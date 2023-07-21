using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IEmployeeService
    {

        ApiResponse<Employee> GetItem(int id);

        ApiResponse<bool> Create(Employee employee);

        ApiResponse<bool> Edit(Employee employee);

        ApiResponse<bool> Delete(int id);

        public bool IsItemExists(string nameEn, string nameAr, string mail);
        public bool IsItemExists(string nameEn, string nameAr, string mail, int id);
        ApiResponse<TotalDetailsResponse<List<Employee>>> GetListEmployees(Param param);
        ApiResponse<bool> UploadBanks(List<EmployeeDto> warehouses);
    }
}
