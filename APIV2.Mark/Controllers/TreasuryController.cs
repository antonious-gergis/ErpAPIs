using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Mark.Controllers
{
    [Route("api")]
    [ApiController, Authorize] 
    public class TreasuryController : ControllerBase
    {
        private readonly ITreasuryService _treasuryService;
        public TreasuryController(ITreasuryService treasuryService)
        {
            _treasuryService = treasuryService;
        }

        [HttpPost]
        [Route("get/treasury/list")]
        public async Task<ActionResult> GetListTreasury(Param param)
        {
            try
            {
                var response = _treasuryService.GetListTreauries(param);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create/treasury")]
        public async Task<ActionResult> Create(TreasuryDto warehouse)
        {
            try
            {
                var result = _treasuryService.Create(warehouse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/treasury")]
        public async Task<ActionResult> Edit(Treasury treasury)
        {
            try
            {
                var result = _treasuryService.Edit(treasury);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/treasury")]
        public async Task<ActionResult> Getwarehouse(TreasuryDto TreasuryDto)
        {
            var result = _treasuryService.GetItem(TreasuryDto.Id);
            return Ok(result);
        }
        [HttpPost]
        [Route("get/treasury")]
        public async Task<ActionResult> GetItems()
        {
            var result = _treasuryService.GetItems();
            return Ok(result);
        }

        [HttpPost]
        [Route("delete/treasury")]
        public async Task<ActionResult> DeleteTreasury(TreasuryDto Treasury)
        {
            var result = _treasuryService.Delete(Treasury.Id);
            return Ok(result);
        }


    }
}
