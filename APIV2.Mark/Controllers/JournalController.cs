using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Business.Services;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Mark.Controllers
{
    [Route("api")]
    [ApiController, Authorize]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;
        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost]
        [Route("create/journal")]
        public async Task<ActionResult> Create(JournalDto journalDto)
        {
            try
            {
                var result = await _journalService.Create(journalDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("post/journal")]
        public async Task<ActionResult> PostJournal(long id,int transactionId)
        {
            try
            {
                var result = await _journalService.PostJournal(id, transactionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
