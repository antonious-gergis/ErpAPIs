
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IChartOfAccountService
    {
        ApiResponse<ChartOfAccount> GetItem(int id);

        ApiResponse<string> Create(ChartOfAccountDto account);

        ApiResponse<bool> Edit(ChartOfAccount account);

        ApiResponse<bool> Delete(int id);

        public bool IsItemExists(string nameEn, string nameAr, string code, string OfficialCode);
        public bool IsItemExists(string nameEn, string nameAr, string code, string OfficialCode, int id);
        ApiResponse<TotalDetailsResponse<List<ChartOfAccount>>> GetListChartOfAccounts(Param param);
    }
}
