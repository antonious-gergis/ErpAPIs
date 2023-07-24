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
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("get/vendors")]
        public async Task<ActionResult> GetVendors()
        {
            try
            {
                var response = _vendorService.GetItems();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("get/vendor/list")]
        public async Task<ActionResult> GetVendorsList(Param param)
        {
            try
            {
                var response = _vendorService.GetListVendors(param);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create/vendor")]
        public async Task<ActionResult> Create(Vendor vendor)
        {
            try
            {
                var result = await _vendorService.Create(vendor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/vendor")]
        public async Task<ActionResult> UpdateVendor(Vendor vendor)
        {
            try
            {
                var result = _vendorService.Edit(vendor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/vendor")]
        public async Task<ActionResult> GetCustomer(long id)
        {
            var result = _vendorService.GetItem(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("delete/vendor")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = _vendorService.Delete(id);
            return Ok(result);
        }

    }
}
