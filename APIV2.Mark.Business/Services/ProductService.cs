
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Metrics;
using System.Net;
using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
namespace APIV2.Mark.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly UtilitiyContext _context;
        public ProductService(UtilitiyContext context)
        {
            _context = context;
        }
        public ApiResponse<bool> Create(Product product)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(product.NameEn, product.NameAr,product.Barcode,product.Sku))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Product is already exist";
                }
                if (product != null)
                {
                    product.Code = GenerateCode();
                    _context.Products.Add(product);
                    _context.SaveChanges();

                    result.Data = true;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            var result = new ApiResponse<bool>();
            try
            {
                var item = GetItem(id);
                if (item.Data == null)
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Product is not found";
                    return result;
                }

                item.Data.StatusId = 4;
                _context.Products.Attach(item.Data);
                _context.Entry(item.Data).State = EntityState.Modified;
                _context.SaveChanges();

                result.Data = true;
                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";

                return result;

            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<bool> Edit(Product product)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(product.NameEn, product.NameAr, product.Barcode,product.Sku, product.Id))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Product is already exist";
                    return result;
                }

                _context.Products.Attach(product);
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                result.Data = true;
                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";

                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<Product> GetItem(int id)
        {
            var result = new ApiResponse<Product>();
            try
            {                
                var item = _context.Products.Where(u => u.Id == id && u.StatusId == 1).FirstOrDefault();
                if (item != null)
                {
                    result.Data = item;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    result.Data = null;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }
        public ApiResponse<List<Product>> GetItems()
        {
            var result = new ApiResponse<List<Product>>();
            try
            {
                var items = _context.Products.Where(u => u.StatusId == 1).ToList();
                if (items != null)
                {
                    result.Data = items;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    result.Data = null;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<TotalDetailsResponse<List<Product>>> GetListProducts(Param param)
        {
            var result = new ApiResponse<TotalDetailsResponse<List<Product>>>();
            try
            {
               
                var totalDetails = new TotalDetailsResponse<List<Product>>();
                List<Product> items;

                if (param.SearchText != "" && param.SearchText != null)
                {
                    items = _context.Products.Where(n => n.StatusId == 1 && (n.NameEn.Contains(param.SearchText) || n.NameAr.Contains(param.SearchText) || n.Code.Contains(param.SearchText) || n.Barcode.Contains(param.SearchText) || n.Sku.Contains(param.SearchText)))
                        .ToList();
                }
                else
                    items = _context.Products.Where(c => c.StatusId == 1).ToList();

                totalDetails.PageNumber = param.page;
                totalDetails.PageSize = param.pageSize;
                totalDetails.Total = items.Count;
                items = items.Skip((param.page - 1) * param.pageSize).Take(param.pageSize).ToList();

                totalDetails.Data = items;
                result.Data = totalDetails;

                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";

                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public bool IsItemExists(string nameEn, string nameAr,string barcode,string sku)
        {
            int ct = _context.Products.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() || n.Barcode.ToLower() == barcode || n.Sku.ToLower() == sku)).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr, string barcode,string sku, int id)
        {
            int ct = _context.Products.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() || n.Barcode == barcode || n.Sku == sku) && n.Id != id).Count();
            if (ct > 0)
            {
                return true;
            }
            else
                return false;
        }
        public string GenerateCode()
        {
            try
            {
                var latestCode = _context.Products
                       .OrderByDescending(x => x.Id)
                       .Take(1)
                       .Select(x => x.Code)
                       .ToList()
                       .FirstOrDefault();

                var newCode = "";
                if (latestCode != null)
                {
                    var codeString = latestCode.Substring(1);
                    var code = Convert.ToInt64(codeString);
                    code = code + 1;
                    newCode = "P" + code;
                }
                else
                {
                    newCode = "P" + "10000";
                }

                return newCode;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ApiResponse<List<Unit>> GetListUnits()
        {
            var result = new ApiResponse<List<Unit>>();
            try
            {
                 
                List<Unit> items;
                items = _context.Units.Where(c => c.StatusId == 1).ToList();
                  
                result.Data = items;

                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";

                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }
        public ApiResponse<List<Category>> GetListCategories()
        {
            var result = new ApiResponse<List<Category>>();
            try
            {
                 
                List<Category> items;
                items = _context.Categories.Where(c => c.StatusId == 1).ToList();
                  
                result.Data = items;

                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";

                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<bool> UploadProducts(List<ProductDto> products)
        {
            using var transaction = _context.Database.BeginTransaction();
            var result = new ApiResponse<bool>();
            List<string> errorList = new List<string>();
            int counter = 2;
            try
            {
                foreach (var item in products)
                {
                    var prodcut = new Product();
                    var cate = _context.Categories.Where(c => c.NameEn == item.Category.Trim() || c.NameAr == item.Category.Trim()).FirstOrDefault();
                    if (cate != null)
                    {
                        prodcut.CategoryId = cate.Id;
                    }
                    else
                    {
                        errorList.Add("You have an invalid data in row  " + counter + " " + "in column E with value " + "  " + item.Category);
                    }

                    var unit = _context.Units.Where(c => c.NameEn == item.Unit.Trim() || c.NameAr == item.Unit.Trim()).FirstOrDefault();
                    if (unit != null)
                    {
                        prodcut.UnitId = unit.Id;
                    }
                    else
                    {
                        errorList.Add("You have an invalid data in row  " + counter + " " + "in column F with value " + "  " + item.Unit);
                    }

                    var random = new Random();
                    prodcut.Sku = item.Sku;
                    prodcut.Barcode = item.Barcode;
                    prodcut.Code = GenerateCode();//Math.Floor(100000 + random.NextDouble() * 900000).ToString();
                    prodcut.Description = item.Description;
                    prodcut.Price = item.Price;
                    prodcut.Cost = item.Cost;
                    prodcut.NameAr = item.NameAr;
                    prodcut.NameEn = item.NameEn;
                    prodcut.StatusId = 1;
                    counter++;

                    var re = Create(prodcut);
                }

                if (errorList.Count() > 0)
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = string.Join("\n", errorList);
                    throw new Exception();
                }
                else
                {
                   
                    result.Data = true;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                    transaction.Commit();
                }
                

                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                if (result.Message == null)
                {
                    result.Message = ex.Message;
                }
                return result;
            }
        }
    }
}
