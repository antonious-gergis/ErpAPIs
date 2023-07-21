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
    public class TreasuryService : ITreasuryService
    {
        private readonly UtilitiyContext _context;
        private readonly IChartOfAccountService _account;
        public TreasuryService(UtilitiyContext context, IChartOfAccountService account)
        {
            _context = context;
            _account = account;
        }
        public ApiResponse<bool> Create(TreasuryDto treasuryDto)
        {
            var treasury = new Treasury();
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(treasuryDto.NameEn, treasuryDto.NameAr))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This treasury is already exist";
                }

                if (treasuryDto != null)
                {
                    treasury = new Treasury
                    {
                        NameEn = treasuryDto.NameEn,
                        NameAr = treasuryDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        Description = treasuryDto.Description,
                        Balance = treasuryDto.Balance,

                    };


                    ChartOfAccountDto accountDto = new ChartOfAccountDto();
                    accountDto = new ChartOfAccountDto
                    {
                        NameEn = treasuryDto.NameEn,
                        NameAr = treasuryDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        AccountType = "Balance sheet",
                        AccountNature = "Debit",
                        Balance = treasuryDto.Balance,
                        ParentId = 18,
                        CurrencyId = 1,
                    };

                    var accountResponse = _account.Create(accountDto);
                    if (accountResponse.Data != "")
                    {
                        treasury.Code = accountResponse.Data;
                    }

                    _context.Treasury.Add(treasury);
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
                    result.Message = "This treasury is not found";
                    return result;
                }

                item.Data.StatusId = 4;
                _context.Treasury.Attach(item.Data);
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

        public ApiResponse<bool> Edit(Treasury warehouse)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<Treasury> GetItem(int id)
        {
            var result = new ApiResponse<Treasury>();
            try
            {
                var item = _context.Treasury.Where(u => u.Id == id && u.StatusId == 1).FirstOrDefault();
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

        public ApiResponse<List<Treasury>> GetItems()
        {
            var result = new ApiResponse<List<Treasury>>();
            try
            {
                var items = _context.Treasury.Where(u => u.StatusId == 1).ToList();
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

        public ApiResponse<TotalDetailsResponse<List<Treasury>>> GetListTreauries(Param param)
        {
            var result = new ApiResponse<TotalDetailsResponse<List<Treasury>>>();
            try
            {

                var totalDetails = new TotalDetailsResponse<List<Treasury>>();
                List<Treasury> items;

                if (param.SearchText != "" && param.SearchText != null)
                {
                    items = _context.Treasury.Where(n => n.StatusId == 1 && (n.NameEn.Contains(param.SearchText) || n.NameAr.Contains(param.SearchText) || n.Code.Contains(param.SearchText) || n.Description.Contains(param.SearchText)))
                        .ToList();
                }
                else
                    items = _context.Treasury.Where(c => c.StatusId == 1).ToList();

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
            int ct = _context.Treasury.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower())).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr, int id)
        {
            int ct = _context.Treasury.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower()) && n.Id != id).Count();
            if (ct > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
