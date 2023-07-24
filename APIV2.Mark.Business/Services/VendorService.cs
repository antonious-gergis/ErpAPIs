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
    public class VendorService : IVendorService
    {
        private readonly UtilitiyContext _context;
        private readonly IAccountService _account;
        private string _errors = "";
        public VendorService(UtilitiyContext context, IAccountService account)
        {
            _context = context;
            _account = account;
        }
        public async Task<ApiResponse<bool>> Create(Vendor vendor)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(vendor.NameEn, vendor.NameAr))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Name is already exist";
                }
                if (vendor != null)
                {
                    vendor.Code = GenerateCode();
                    await _context.Vendors.AddAsync(vendor);

                    var account = new Account
                    {
                        AccountId = 14,
                        AccountType = "Accounts Payable",
                        Balance = vendor.Balance,
                        EmpId = vendor.EmpId,
                        Code = vendor.Code,
                        CreationDate = DateTime.Now,
                        Description = "Liabilities",
                        NameAr = vendor.NameEn,
                        NameEn = vendor.NameEn,
                    };
                    await _account.Create(account);
                    await _context.SaveChangesAsync();

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
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<bool>> Edit(Vendor vendor)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(vendor.NameEn, vendor.NameAr, vendor.AnotherCode, vendor.Id))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Vendor is already exist";
                    return result;
                }

                _context.Vendors.Attach(vendor);
                _context.Entry(vendor).State = EntityState.Modified;

                var account = await _account.GetItem(vendor.Code);
                if (account.Data != null)
                {
                    _account.Edit(account.Data);
                }

                await _context.SaveChangesAsync();
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

        public string GetErrors()
        {
            return _errors;
        }

        public ApiResponse<Vendor> GetItem(long id)
        {
            var result = new ApiResponse<Vendor>();
            var item = _context.Vendors.Where(u => u.Id == id && u.StatusId == 1).FirstOrDefault();
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

        public ApiResponse<List<Vendor>> GetItems()
        {
            var result = new ApiResponse<List<Vendor>>();
            try
            {
                var items = _context.Vendors.Where(u => u.StatusId == 1).ToList();
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

        public ApiResponse<TotalDetailsResponse<List<Vendor>>> GetListVendors(Param param)
        {

            var result = new ApiResponse<TotalDetailsResponse<List<Vendor>>>();
            var totalDetails = new TotalDetailsResponse<List<Vendor>>();
            List<Vendor> items;

            if (param.SearchText != "" && param.SearchText != null)
            {
                items = _context.Vendors.Where(n => n.StatusId == 1 && (n.NameEn.Contains(param.SearchText) || n.NameAr.Contains(param.SearchText) || n.Code.Contains(param.SearchText) || n.AnotherCode.Contains(param.SearchText)))
                    .ToList();
            }
            else
                items = _context.Vendors.Where(c => c.StatusId == 1).ToList();

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
         

        public bool IsItemExists(string nameEn, string nameAr)
        {
            int ct = _context.Vendors.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower())).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr, string code, long id)
        {
            int ct = _context.Vendors.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() || n.AnotherCode == code) && n.Id != id).Count();
            if (ct > 0)
            {
                _errors = " NameEn " + nameEn + " or NameAr" + nameAr + "  Exists Already";
                return true;
            }
            else
                return false;
        }


        public bool IsVendorExists(int srcVendorId)
        {
            int ct = _context.Customers.Where(n => n.Id == srcVendorId).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public string GenerateCode()
        {
            try
            {
                var latestCode = _context.Vendors
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
                    newCode = "V" + code;
                }
                else
                {
                    newCode = "V" + "10000";
                }

                return newCode;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
