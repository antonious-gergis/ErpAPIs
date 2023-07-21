
using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    [ApiController,Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService; 
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("get/customers")]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                var customers =  _customerService.GetItems();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("get/customer/list")]
        public async Task<ActionResult> GetCustomersList(Param param)
        {
            try
            {
                var customers =  _customerService.GetListCustomers(param);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create/customer")]
        public async Task<ActionResult> CreateCustomer(Customer customer)
        {
            try
            {
                var result =  _customerService.Create(customer); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/customer")]
        public async Task<ActionResult> UpdateCustomer(Customer customer)
        {
            try
            {
               var result = _customerService.Edit(customer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/customer")]
        public async Task<ActionResult> GetCustomer(CustomerDto customer)
        {
            var result =  _customerService.GetItem(customer.Id);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("delete/customer")]
        public async Task<ActionResult> DeleteCustomer(CustomerDto customer)
        {
            var result =  _customerService.Delete(customer.Id);
            return Ok(result);
        }

    }
}
