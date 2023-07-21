
using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIV2.Mark.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            userForRegister.UserName = userForRegister.UserName?.ToLower();

            if (await _authService.UserExist(userForRegister.UserName))
            {
                return BadRequest("Username already exist");
            }

            var userToCreate = new UserAccount
            {
                UserName = userForRegister.UserName,
                Email = userForRegister.Email,
            };

            var userCreate = await _authService.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForRegisterDto userForLogin)
        {
            var userLogin = await _authService.Login(userForLogin.Email.ToLower(), userForLogin.Password);

            if (userLogin == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, userLogin.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(100),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return Ok(new
            //{
            //    token = tokenHandler.WriteToken(token),
            //});
            return new ObjectResult(new
            {
                access_token = tokenHandler.WriteToken(token),
                user_Id = userLogin.Id,
                user_name = userLogin.UserName,
                expires_in = token.ValidTo,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
            });
        }

    }
}
