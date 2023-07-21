
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
    //[ApiExplorerSettings.]
    public class BanksController : ControllerBase
    {
        private readonly IBankService _bankService;
        public BanksController(IBankService bankService)
        {
            _bankService = bankService; 
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("get/bank/list")]
        public async Task<ActionResult> GetListBanks(Param param)
        {
            try
            {
                var response = _bankService.GetListBanks(param);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create/bank")]
        public async Task<ActionResult> Create(BankDto bank)
        {
            try
            {
                var result = _bankService.Create(bank);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/bank")]
        public async Task<ActionResult> Edit(Bank bank)
        {
            try
            {
                var result = _bankService.Edit(bank);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/bank")]
        public async Task<ActionResult> Getwarehouse(BankDto bank)
        {
            var result = _bankService.GetItem(bank.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("delete/bank")]
        public async Task<ActionResult> DeleteBank(BankDto bank)
        {
            var result = _bankService.Delete(bank.Id);
            return Ok(result);
        }


        [HttpPost]
        [Route("upload/banks/excel")]
        public async Task<ActionResult> UploadExcelBanks(List<BankDto> bankDtos)
        {
            try
            {
                var response = _bankService.UploadBanks(bankDtos);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
