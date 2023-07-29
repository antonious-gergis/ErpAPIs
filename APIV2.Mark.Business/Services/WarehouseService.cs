using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Services
{
    public class WarehouseService:IWarehouseService
    {
        private readonly UtilitiyContext _context;
        private readonly IChartOfAccountService _account;
        public WarehouseService(UtilitiyContext context, IChartOfAccountService account)
        {
            _context = context;
            _account = account;
        }

        public ApiResponse<bool> Create(WarehouseDto warehouseDto)
        {
            var warehouse = new Warehouse();
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(warehouseDto.NameEn, warehouseDto.NameAr))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Warehouse is already exist";
                }

                if (warehouseDto != null)
                {
                    warehouse = new Warehouse
                    {
                        NameEn = warehouseDto.NameEn,
                        NameAr = warehouseDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        Description = warehouseDto.Description, 
                        
                    };


                    ChartOfAccountDto accountDto = new ChartOfAccountDto();
                    accountDto = new ChartOfAccountDto
                    {
                        NameEn = warehouseDto.NameEn,
                        NameAr = warehouseDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        AccountType = "Balance sheet",
                        AccountNature = "Debit",
                        Balance = 0,
                        ParentId = 16,
                        CurrencyId = 1,
                    };

                    var accountResponse = _account.Create(accountDto);
                    if (accountResponse.Data != "")
                    {
                        warehouse.Code = accountResponse.Data;
                    }

                    _context.Warehouses.Add(warehouse);
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

        public ApiResponse<bool> Delete(long id)
        {
            var result = new ApiResponse<bool>();
            try
            {
                var item = GetItem(id);
                if (item.Data == null)
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This warehouse is not found";
                    return result;
                }

                item.Data.StatusId = 4;
                _context.Warehouses.Attach(item.Data);
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

        public ApiResponse<bool> Edit(Warehouse warehouse)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(warehouse.NameEn, warehouse.NameAr, warehouse.Id))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Warehouse is already exist";
                    return result;
                }

                _context.Warehouses.Attach(warehouse);
                _context.Entry(warehouse).State = EntityState.Modified;
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

        public ApiResponse<Warehouse> GetItem(long id)
        {
            var result = new ApiResponse<Warehouse>();
            try
            {
                var item = _context.Warehouses.Where(u => u.Id == id && u.StatusId == 1).FirstOrDefault();
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

        public ApiResponse<List<Warehouse>> GetItems()
        {
            var result = new ApiResponse<List<Warehouse>>();
            try
            {
                var items = _context.Warehouses.Where(u => u.StatusId == 1).ToList();
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

        public ApiResponse<TotalDetailsResponse<List<Warehouse>>> GetListWarehouses(Param param)
        {
            var result = new ApiResponse<TotalDetailsResponse<List<Warehouse>>>();
            try
            {

                var totalDetails = new TotalDetailsResponse<List<Warehouse>>();
                List<Warehouse> items;

                if (param.SearchText != "" && param.SearchText != null)
                {
                    items = _context.Warehouses.Where(n => n.StatusId == 1 && (n.NameEn.Contains(param.SearchText) || n.NameAr.Contains(param.SearchText) || n.Code.Contains(param.SearchText) || n.Description.Contains(param.SearchText) ))
                        .ToList();
                }
                else
                    items = _context.Warehouses.Where(c => c.StatusId == 1).ToList();

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

        public bool IsItemExists(string nameEn, string nameAr)
        {
            int ct = _context.Warehouses.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() )).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr, long id)
        {
            int ct = _context.Warehouses.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() ) && n.Id != id).Count();
            if (ct > 0)
            {
                return true;
            }
            else
                return false;
        }

        public ApiResponse<bool> UploadWarehouses(List<WarehouseDto> warehouses)
        {
            using var transaction = _context.Database.BeginTransaction();
            var result = new ApiResponse<bool>();
            List<string> errorList = new List<string>();
            int counter = 2;
            try
            {
                foreach (var item in warehouses)
                {
                    var warhouse = new Warehouse();                    

                    var random = new Random();
                    warhouse.Code = Math.Floor(100000 + random.NextDouble() * 900000).ToString();
                    warhouse.Description = item.Description; 
                    warhouse.NameAr = item.NameAr.Trim();
                    warhouse.NameEn = item.NameEn.Trim();
                    warhouse.StatusId = 1;
                    warhouse.CreationDate = DateTime.Now;
                    counter++;

                    var re = Create(item);
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
