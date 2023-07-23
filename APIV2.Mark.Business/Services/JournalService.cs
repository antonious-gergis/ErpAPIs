
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
using static APIV2.Mark.Entities.Enums;

namespace APIV2.Mark.Business.Services
{
    public class JournalService : IJournalService
    {
        private readonly UtilitiyContext _context;
        private readonly IChartOfAccountService _account;
        private readonly ITransactionOperationsService _transaction;
        public JournalService(UtilitiyContext context, IChartOfAccountService account, ITransactionOperationsService transaction)
        {
            _account = account;
            _context = context;
            _transaction = transaction;
        }

        public async Task<ApiResponse<bool>> Create(JournalDto journalDto)
        {
            var journal = new Journal();
            var result = new ApiResponse<bool>();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (journalDto != null)
                {
                    var sumDebit = journalDto.JournalDetails.Sum(s => s.Debit).GetValueOrDefault();
                    var sumCredit = journalDto.JournalDetails.Sum(s => s.Credit).GetValueOrDefault();
                    if (sumDebit != sumCredit)
                    {
                        throw new Exception("The total debit not equal total credit");
                    }
                    journal = new Journal
                    {
                        CreationDate = DateTime.Now,
                        CurrencyId = journalDto.CurrencyId,
                        Description = journalDto.Description,
                        StatusId = 1,
                        TransactionState = 1,
                        Code = GenerateCode(),
                        Amount = 0
                    };

                    foreach (var item in journalDto.JournalDetails)
                    {
                        var detail = new JournalDetail
                        {
                            AccountId = item.AccountId,
                            SubAccountId = item.SubAccountId,
                            Debit = item.Debit,
                            Credit = item.Credit,
                            Description = item.Description,
                        };

                        journal.Amount += item.Debit;
                        journal.JournalDetails.Add(detail);
                    }

                    await _context.Journals.AddAsync(journal);
                    await _context.SaveChangesAsync();

                    var operation = new TransactionOperations
                    {
                        Description = journalDto.Description,
                        EmpId = journalDto.EmpId,
                        OperationCode = journal.Code,
                        OperationId = journal.Id,
                        OperationName = journalDto.OperationName == null ? "New Journal" : journalDto.OperationName,

                    };
                    await _transaction.Create(operation);

                    transaction.Commit();
                    result.Data = true;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    transaction.Rollback();
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }

                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }
        private string GenerateCode()
        {
            try
            {
                var latestCode = _context.Customers
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
                    newCode = "J" + code;
                }
                else
                {
                    newCode = "J" + "10000";
                }

                return newCode;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse<Journal>> GetItem(long id)
        {
            var result = new ApiResponse<Journal>();
            try
            {
                var item = await _context.Journals.Where(u => u.Id == id && u.StatusId == 1)
                                    .Include(j => j.JournalDetails).FirstOrDefaultAsync();
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
        private async Task<ApiResponse<bool>> UpdateTheAccountBalance(long id)
        {
            var result = new ApiResponse<bool>();

            try
            {
                var journal = await GetItem(id);
                if (journal.Data != null)
                {
                    if (journal.Data.TransactionState == 1)
                    {
                        foreach (var detail in journal.Data.JournalDetails)
                        {
                            var minusCredit = detail.Credit * -1;
                            var balance = minusCredit + detail.Debit;
                            var account = _account.GetItem(Convert.ToInt32(detail.AccountId.GetValueOrDefault()));
                            if (account.Data != null)
                            {
                                account.Data.Balance = account.Data.Balance + balance;
                                _context.Entry(account.Data).State = EntityState.Modified;

                                var parent = account.Data.ParentId;
                                while (parent != null)
                                {
                                    var parentDetail = _account.GetItem(parent.GetValueOrDefault());
                                    parentDetail.Data.Balance = parentDetail.Data.Balance + balance;
                                    _context.Entry(parentDetail.Data).State = EntityState.Modified;

                                    parent = parentDetail.Data.ParentId;
                                } 
                            }
                        }

                        journal.Data.TransactionState = 2;
                        journal.Data.PostedDate = DateTime.Now;
                        _context.Entry(journal.Data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        result.Data = true;
                        result.ErrorCode = (int)HttpStatusCode.OK;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.Data = false;
                        result.ErrorCode = (int)HttpStatusCode.BadRequest;
                        result.Message = "This journal is already posted!";
                        return result;
                    }
                   
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
        public ApiResponse<bool> UpdateTheSubAccountBalance(Journal journal)
        {
            var result = new ApiResponse<bool>();

            try
            {
                foreach (var detail in journal.JournalDetails)
                {

                    var minusCredit = detail.Credit * -1;
                    var balance = minusCredit + detail.Debit;
                    var account = _account.GetItem(Convert.ToInt32(detail.AccountId.GetValueOrDefault()));
                    if (account.Data != null)
                    {
                        account.Data.Balance = account.Data.Balance + balance;
                        _context.Entry(account).State = EntityState.Modified;


                        var parent = account.Data.ParentId;
                        while (parent != null)
                        {
                            var parentDetail = _account.GetItem(parent.GetValueOrDefault());
                            parentDetail.Data.Balance = parentDetail.Data.Balance + balance;
                            _context.Entry(parentDetail).State = EntityState.Modified;

                            parent = parentDetail.Data.ParentId;
                        }
                        _context.SaveChanges();
                        result.Data = true;
                        result.ErrorCode = (int)HttpStatusCode.OK;
                        result.Message = "Success";
                    }

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
        public async Task<ApiResponse<bool>> PostJournal(long id)
        {
            var result = new ApiResponse<bool>();
            try
            {
                result = await UpdateTheAccountBalance(id);
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


    }
}
