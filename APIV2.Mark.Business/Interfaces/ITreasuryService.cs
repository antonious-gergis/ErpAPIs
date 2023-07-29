using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface ITreasuryService
    {
        ApiResponse<Treasury> GetItem(long id);

        ApiResponse<bool> Create(TreasuryDto treasury);

        ApiResponse<bool> Edit(Treasury treasury);

        ApiResponse<bool> Delete(long id);

        public bool IsItemExists(string nameEn, string nameAr);
        public bool IsItemExists(string nameEn, string nameAr, int id);
        ApiResponse<TotalDetailsResponse<List<Treasury>>> GetListTreauries(Param param);
        ApiResponse<List<Treasury>> GetItems();
    }
}
