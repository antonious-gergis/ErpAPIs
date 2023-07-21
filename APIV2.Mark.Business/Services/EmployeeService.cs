using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UtilitiyContext _context;
        public EmployeeService(UtilitiyContext context)
        {
            _context = context;
        }
        public ApiResponse<bool> Create(Employee employee)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<bool> Edit(Employee employee)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<Employee> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<TotalDetailsResponse<List<Employee>>> GetListEmployees(Param param)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExists(string nameEn, string nameAr, string mail)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExists(string nameEn, string nameAr, string mail, int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<bool> UploadBanks(List<EmployeeDto> warehouses)
        {
            throw new NotImplementedException();
        }
    }
}
