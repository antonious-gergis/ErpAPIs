using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponse<Account>> GetItem(string code);
        Task<ApiResponse<bool>> Create(Account account);
        ApiResponse<bool> Edit(Account account);
    }
}
