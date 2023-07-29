using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    [ApiController, Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("create/order")]
        public async Task<ActionResult> Create(OrderDto order)
        {
            try
            {
                var result = await _orderService.Create(order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("recalculate/order")]
        public async Task<ActionResult> CalculateInvoice(long id)
        {
            try
            {
                var result = await _orderService.CalculateInvoice(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
