using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface ITaxService
    {
        ApiResponse<Tax> GetItem(int id);

        ApiResponse<bool> Create(TaxDto tax);

        ApiResponse<bool> Edit(TaxDto tax);

        ApiResponse<bool> Delete(int id);

        public bool IsItemExists(string nameEn, string nameAr);
        public bool IsItemExists(string nameEn, string nameAr, string code, int id);
        ApiResponse<TotalDetailsResponse<List<Tax>>> GetListTaxes(Param param);
    }
}
