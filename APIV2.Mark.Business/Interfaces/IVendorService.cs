using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IVendorService
    {
        ApiResponse<List<Vendor>> GetItems();
        ApiResponse<Vendor> GetItem(long id);

        Task<ApiResponse<bool>> Create(Vendor vendor);

        Task<ApiResponse<bool>> Edit(Vendor vendor);

        ApiResponse<bool> Delete(int id);

        public bool IsItemExists(string nameEn, string nameAr);
        public bool IsItemExists(string nameEn, string nameAr, string code, long id);

        public bool IsVendorExists(int srcVendorId);
        public string GetErrors();

        ApiResponse<TotalDetailsResponse<List<Vendor>>> GetListVendors(Param param);
    }
}
