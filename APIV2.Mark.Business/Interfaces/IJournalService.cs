using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IJournalService
    {
        Task<ApiResponse<bool>> Create(JournalDto journalDto);
        Task<ApiResponse<Journal>> GetItem(long id);
        Task<ApiResponse<bool>> PostJournal(long id); 
    }
}
