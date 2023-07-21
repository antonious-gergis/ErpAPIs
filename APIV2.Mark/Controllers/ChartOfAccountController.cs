using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Business.Services;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Mark.Controllers
{
    [Route("api")]
    [ApiController, Authorize]
    public class ChartOfAccountController : ControllerBase
    {
        private readonly IChartOfAccountService _chartOfAccount;
        public ChartOfAccountController(IChartOfAccountService chartOfAccount)
        {
            _chartOfAccount = chartOfAccount;
        }
        [HttpPost]
        [Route("create/account")]
        public async Task<ActionResult> Create(ChartOfAccountDto account)
        {
            try
            {
                var result = _chartOfAccount.Create(account);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
