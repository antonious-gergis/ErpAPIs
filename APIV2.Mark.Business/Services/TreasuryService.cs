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
                        Balance = 0,
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
            throw new NotImplementedException();
        }

        public ApiResponse<bool> Edit(Treasury warehouse)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<Treasury> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<List<Treasury>> GetItems()
        {
            throw new NotImplementedException();
        }

        public ApiResponse<TotalDetailsResponse<List<Treasury>>> GetListWarehouses(Param param)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExists(string nameEn, string nameAr)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExists(string nameEn, string nameAr, int id)
        {
            throw new NotImplementedException();
        }
    }
}
