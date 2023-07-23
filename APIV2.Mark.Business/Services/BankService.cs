
using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APIV2.Mark.Business.Services
{
    public class BankService : IBankService
    {
        private readonly UtilitiyContext _context;
        private readonly IChartOfAccountService _account;
        public BankService(UtilitiyContext context, IChartOfAccountService account)
        {
            _context = context;
            _account = account;
        }
        public ApiResponse<bool> Create(BankDto bankDto)
        {
            var bank = new Bank();
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(bankDto.NameEn, bankDto.NameAr, bankDto.EbanNumber))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Bank is already exist";
                }

                if (bankDto != null)
                {
                    bank = new Bank
                    {
                        NameEn = bankDto.NameEn,
                        NameAr = bankDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        Address = bankDto.Address,
                        Balance = bankDto.Balance,
                        Code = GenerateCode(),
                        CurrencyId = 1,
                        EbanNumber = bankDto.EbanNumber,
                        
                     };


                    ChartOfAccountDto accountDto = new ChartOfAccountDto();
                    accountDto = new ChartOfAccountDto
                    {
                        NameEn = bankDto.NameEn,
                        NameAr = bankDto.NameAr,
                        StatusId = 1,
                        CreationDate = DateTime.Now,
                        AccountType = "Balance sheet",
                        AccountNature = "Debit",
                        Balance = bankDto.Balance,
                        ParentId = 19,
                        CurrencyId = 1,
                    };

                    var accountResponse = _account.Create(accountDto);
                    if (accountResponse.Data != "")
                    {
                       bank.AccountNumber = accountResponse.Data;
                    }

                    _context.Banks.Add(bank);
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
                    result.Message = "This Bank is not found";
                    return result;
                }

                item.Data.StatusId = 4;
                _context.Banks.Attach(item.Data);
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

        public ApiResponse<bool> Edit(Bank bank)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(bank.NameEn, bank.NameAr,bank.EbanNumber, bank.Id))
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Bank is already exist";
                    return result;
                }

                _context.Banks.Attach(bank);
                _context.Entry(bank).State = EntityState.Modified;
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

        public ApiResponse<Bank> GetItem(long id)
        {
            var result = new ApiResponse<Bank>();
            try
            {
                var item = _context.Banks.Where(u => u.Id == id && u.StatusId == 1).FirstOrDefault();
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

        public ApiResponse<TotalDetailsResponse<List<Bank>>> GetListBanks(Param param)
        {
            var result = new ApiResponse<TotalDetailsResponse<List<Bank>>>();
            try
            {

                var totalDetails = new TotalDetailsResponse<List<Bank>>();
                List<Bank> items;

                if (param.SearchText != "" && param.SearchText != null)
                {
                    items = _context.Banks.Where(n => n.StatusId == 1 && (n.NameEn.Contains(param.SearchText) || n.NameAr.Contains(param.SearchText) || n.Code.Contains(param.SearchText) || n.EbanNumber.Contains(param.SearchText) || n.AccountNumber.Contains(param.SearchText)))
                        .ToList();
                }
                else
                    items = _context.Banks.Where(c => c.StatusId == 1).ToList();

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

        public bool IsItemExists(string nameEn, string nameAr, string ebanNumber)
        {
            int ct = _context.Banks.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() ||  n.EbanNumber == ebanNumber)).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr, string ebanNumber, long id)
        {
            int ct = _context.Banks.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() || n.EbanNumber == ebanNumber) && n.Id != id).Count();
            if (ct > 0)
            {
                return true;
            }
            else
                return false;
        }

        public ApiResponse<bool> UploadBanks(List<BankDto> warehouses)
        {
            throw new NotImplementedException();
        }

        private string GenerateCode()
        {
            try
            {
                var latestCode = _context.Banks
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
                    newCode = "B" + code;
                }
                else
                {
                    newCode = "B" + "10000";
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
