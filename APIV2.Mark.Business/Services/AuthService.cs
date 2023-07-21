
using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Mark.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UtilitiyContext _context;
        public AuthService(UtilitiyContext context)
        {
            _context = context;
        }

        public async Task<UserAccount> Login(string email, string password)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null!;

            if (!VerfiyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null!;

            return user;
        }

        public async Task<UserAccount> Register(UserAccount user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.UserAccounts.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerfiyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public async Task<bool> UserExist(string email)
        {
            if (await _context.UserAccounts.AnyAsync(x => x.Email == email))
                return true;

            return false;
        }
    }
}
