using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IBankService
    {
        ApiResponse<Bank> GetItem(int id);

        ApiResponse<bool> Create(BankDto bank);

        ApiResponse<bool> Edit(Bank bank);

        ApiResponse<bool> Delete(int id);

        public bool IsItemExists(string nameEn, string nameAr, string ebanNumber);
        public bool IsItemExists(string nameEn, string nameAr, string ebanNumber, int id);
        ApiResponse<TotalDetailsResponse<List<Bank>>> GetListBanks(Param param);
        ApiResponse<bool> UploadBanks(List<BankDto> warehouses);
    }
}
