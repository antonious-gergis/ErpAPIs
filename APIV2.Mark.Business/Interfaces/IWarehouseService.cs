using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IWarehouseService
    {
        ApiResponse<Warehouse> GetItem(long id);

        ApiResponse<bool> Create(WarehouseDto warehouse);

        ApiResponse<bool> Edit(Warehouse warehouse);

        ApiResponse<bool> Delete(long id);

        public bool IsItemExists(string nameEn, string nameAr);
        public bool IsItemExists(string nameEn, string nameAr, long id);
        ApiResponse<TotalDetailsResponse<List<Warehouse>>> GetListWarehouses(Param param);
        ApiResponse<List<Warehouse>> GetItems();
        ApiResponse<bool> UploadWarehouses(List<WarehouseDto> warehouses);
    }
}
