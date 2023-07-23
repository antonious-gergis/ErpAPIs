using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APIV2.Mark.Business.Services
{
    public class ChartOfAccountService : IChartOfAccountService
    {
        private readonly UtilitiyContext _context; 
        public ChartOfAccountService(UtilitiyContext context)
        {
            _context = context; 
        }
        public ApiResponse<string> Create(ChartOfAccountDto accountDto)
        {
            var result = new ApiResponse<string>();
            ChartOfAccount account = new ChartOfAccount();
            try
            {
                if (IsItemExists(accountDto.NameEn, accountDto.NameAr, accountDto.Code, accountDto.OfficialCode))
                {
                    result.Data = "";
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Account is already exist";
                }
                if (accountDto != null) {

                    //create a level for new account that depend on father level
                    var fatherLevel = _context.ChartOfAccount.Find(accountDto.ParentId).AccountLevel;
                    account.AccountLevel = fatherLevel + 1;

                    //all accounts that have the same father
                    var accountFather = _context.ChartOfAccount.Where(a => a.ParentId == accountDto.ParentId);
                    var o = accountFather.FirstOrDefault();

                    if (accountDto.AccountLevel < 4)
                    {//if there is no account has the same father
                        if (o != null)
                        {
                            var lastAccount = accountFather.ToList().Last();
                            //transform last account code to long
                            long lastAccountlong = long.Parse(lastAccount.Code);
                            //new code in long
                            var newCodelong = lastAccountlong + 1;

                            account.Code = "0" + newCodelong.ToString();

                            //father code
                        }
                        else
                        {//get the father account
                            var father = _context.ChartOfAccount.Find(accountDto.ParentId);
                            var fatherAccount = father.Code;
                            account.Code = fatherAccount + "01";
                        }
                    }
                    else
                    {//if there is no account has the same father
                        if (o != null)
                        {
                            var lastAccount = accountFather.ToList().Last();
                            //transform last account code to long
                            long lastAccountLong = long.Parse(lastAccount.Code);
                            //new code in long
                            var newCodeLong = lastAccountLong + 1;

                            account.Code = "0" + newCodeLong.ToString();

                            //father code
                        }
                        else
                        {//get the father account
                            var father = _context.ChartOfAccount.Find(accountDto.ParentId);
                            var fatherAccount = father.Code;
                            account.Code = fatherAccount + "001";
                        }
                    }

                    account.ParentId = accountDto.ParentId;
                    account.AccountNature = accountDto.AccountNature;
                    account.CurrencyId = accountDto.CurrencyId;
                    account.StatusId =1;
                    account.AccountType = accountDto.AccountType;
                    account.CreationDate = DateTime.Now;
                    account.Description = accountDto.Description;
                    account.Balance = accountDto.Balance;
                    account.OfficialCode = accountDto.OfficialCode;
                    account.NameAr = accountDto.NameAr;
                    account.NameEn = accountDto.NameEn;
                    _context.ChartOfAccount.Add(account);
                    _context.SaveChanges();

                    result.Data = account.Code;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    result.Data = "";
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Data = "";
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<bool> Edit(ChartOfAccount account)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<ChartOfAccount> GetItem(long id)
        {
            var result = new ApiResponse<ChartOfAccount>();
            try
            {
                var item = _context.ChartOfAccount.Where(u => u.Id == id && u.StatusId == 1).FirstOrDefault();
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

        public ApiResponse<TotalDetailsResponse<List<ChartOfAccount>>> GetListChartOfAccounts(Param param)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExists(string nameEn, string nameAr, string code, string OfficialCode)
        {
            int ct = _context.ChartOfAccount.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() || n.Code == code || n.OfficialCode == OfficialCode)).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr, string code, string OfficialCode, int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
