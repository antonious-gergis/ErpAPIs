using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Route("api")]
    [ApiController,Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService; 
        }
         
        [HttpPost]
        [Route("get/product/list")]
        public async Task<ActionResult> GetProductsList(Param param)
        {
            try
            {
                var response =  _productService.GetListProducts(param);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create/product")]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                var result =  _productService.Create(product); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/product")]
        public async Task<ActionResult> Edit(Product product)
        {
            try
            {
               var result = _productService.Edit(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/product")]
        public async Task<ActionResult> GetProduct(ProductDto product)
        {
            var result = _productService.GetItem(product.Id);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("delete/product")]
        public async Task<ActionResult> DeleteProduct(ProductDto product)
        {
            var result = _productService.Delete(product.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("get/unit/list")]
        public async Task<ActionResult> GetListUnits()
        {
            try
            {
                var response = _productService.GetListUnits();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("get/products")]
        public async Task<ActionResult> GetItems()
        {
            try
            {
                var response = _productService.GetItems();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("get/category/list")]
        public async Task<ActionResult> GetListCategories()
        {
            try
            {
                var response = _productService.GetListCategories();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("upload/products/excel")]
        public async Task<ActionResult> UploadExcelProducts(List<ProductDto> productDtos)
        {
            try
            {
                var response = _productService.UploadProducts(productDtos);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload/image")]
        public async Task<IActionResult> Upload()
        {
            var result = new ApiResponse<string>();
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                //var file = Request.Form.Files[0];
                var folderName = Path.Combine("UploadFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    result.Data = dbPath;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                    return Ok(result);
                }
                else
                {
                    result.Data = null;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
