using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Route("api")]
    [ApiController, Authorize]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        [Route("get/warehouse/list")]
        public async Task<ActionResult> GetListWarehouses(Param param)
        {
            try
            {
                var response = _warehouseService.GetListWarehouses(param);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create/warehouse")]
        public async Task<ActionResult> Create(WarehouseDto warehouse)
        {
            try
            {
                var result = _warehouseService.Create(warehouse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/warehouse")]
        public async Task<ActionResult> Edit(Warehouse warehouse)
        {
            try
            {
                var result = _warehouseService.Edit(warehouse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/warehouse")]
        public async Task<ActionResult> Getwarehouse(WarehouseDto warehouse)
        {
            var result = _warehouseService.GetItem(warehouse.Id);
            return Ok(result);
        }
        [HttpPost]
        [Route("get/warehouses")]
        public async Task<ActionResult> GetItems()
        {
            var result = _warehouseService.GetItems();
            return Ok(result);
        }

        [HttpPost]
        [Route("delete/warehouse")]
        public async Task<ActionResult> Deletewarehouse(WarehouseDto warehouse)
        {
            var result = _warehouseService.Delete(warehouse.Id);
            return Ok(result);
        }

        
        [HttpPost]
        [Route("upload/warehouses/excel")]
        public async Task<ActionResult> UploadExcelwarehouses(List<WarehouseDto> warehouseDtos)
        {
            try
            {
                var response = _warehouseService.UploadWarehouses(warehouseDtos);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         
    }
}
