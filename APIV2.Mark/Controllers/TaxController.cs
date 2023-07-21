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
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxkService;
        public TaxController(ITaxService taxService)
        {
            _taxkService = taxService;
        }

        [HttpPost]
        [Route("create/tax")]
        public async Task<ActionResult> Create(TaxDto tax)
        {
            try
            {
                var result = _taxkService.Create(tax);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/tax/list")]
        public async Task<ActionResult> GetTaxList(Param param)
        {
            try
            {
                var response = _taxkService.GetListTaxes(param);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
