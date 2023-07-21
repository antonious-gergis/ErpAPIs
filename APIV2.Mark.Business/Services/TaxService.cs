using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Services
{
    public class TaxService : ITaxService
    {
        private readonly UtilitiyContext _context;
        private readonly IChartOfAccountService _account;
        public TaxService(UtilitiyContext context, IChartOfAccountService account)
        {
            _context = context;
            _account = account;
        }
        public ApiResponse<bool> Create(TaxDto taxDto)
        {
            var tax = new Tax();
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(taxDto.NameEn, taxDto.NameAr))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Tax is already exist";
                }
                if (taxDto != null)
                {
                    tax = new Tax
                    {
                        NameEn = taxDto.NameEn,
                        NameAr = taxDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        Percentage = taxDto.Percentage,
                        Type = taxDto.Type                        
                    };  

                    ChartOfAccountDto accountDto = new ChartOfAccountDto();
                    accountDto = new ChartOfAccountDto
                    {
                        NameEn = taxDto.NameEn,
                        NameAr = taxDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        AccountType = "Balance sheet",
                        AccountNature = taxDto.SelectAccountTaxId == 12 ? "Debit":"Crdit",
                        Balance = 0,
                        ParentId = taxDto.SelectAccountTaxId,
                        CurrencyId = 1,                        
                    };

                    var accountResponse = _account.Create(accountDto);
                    if (accountResponse.Data != "")
                    {
                        tax.Code = accountResponse.Data;
                    }
                     
                    _context.Taxes.Add(tax);
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
            throw new NotImplementedException();
        }

        public ApiResponse<bool> Edit(TaxDto tax)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<Tax> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<TotalDetailsResponse<List<Tax>>> GetListTaxes(Param param)
        {
            var result = new ApiResponse<TotalDetailsResponse<List<Tax>>>();
            try
            {

                var totalDetails = new TotalDetailsResponse<List<Tax>>();
                List<Tax> items;

                if (param.SearchText != "" && param.SearchText != null)
                {
                    items = _context.Taxes.Where(n => n.StatusId == 1 && (n.NameEn.Contains(param.SearchText) || n.NameAr.Contains(param.SearchText) || n.Code.Contains(param.SearchText)))
                        .ToList();
                }
                else
                    items = _context.Taxes.Where(c => c.StatusId == 1).ToList();

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
            int ct = _context.Taxes.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower())).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        
        public bool IsItemExists(string nameEn, string nameAr, string code, int id)
        {
            throw new NotImplementedException();
        }

    }
}
