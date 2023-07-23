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
    public class TransactionOperationsService : ITransactionOperationsService
    {
        private readonly UtilitiyContext _context;
        public TransactionOperationsService(UtilitiyContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<bool>> Create(TransactionOperations transaction)
        {
            var result = new ApiResponse<bool>();
            
            try
            {
                if (transaction != null)
                {                 
                    await _context.TransactionOperations.AddAsync(transaction);
                    await _context.SaveChangesAsync();
                     
                    result.Data = true;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
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
    }
}
