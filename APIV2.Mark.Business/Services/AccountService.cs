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
    public class AccountService : IAccountService
    {
        private readonly UtilitiyContext _context;
        public AccountService(UtilitiyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<bool>> Create(Account account)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (account != null)
                {
                    await _context.Accounts.AddAsync(account);
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

        public ApiResponse<bool> Edit(Account account)
        {
            var result = new ApiResponse<bool>();
            try
            {
                 
                _context.Accounts.Attach(account);
                _context.Entry(account).State = EntityState.Modified;
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

        public async Task<ApiResponse<Account>> GetItem(string code)
        {
            var result = new ApiResponse<Account>();
            try
            {
                var item = await _context.Accounts.Where(u => u.Code == code).FirstOrDefaultAsync();
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
    }
}
