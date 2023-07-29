
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace APIV2.Mark.Business.Interfaces
{
    public interface ICustomerService
    {
        ApiResponse<List<Customer>> GetItems();
        ApiResponse<Customer> GetItem(long id);

        Task<ApiResponse<bool>> Create(Customer customer);

        Task<ApiResponse<bool>> Edit(Customer customer);

        ApiResponse<bool> Delete(long id);

        public bool IsItemExists(string nameEn, string nameAr);
        public bool IsItemExists(string nameEn, string nameAr,string code, long id);

        public bool IsCustomerExists(int srcCustomerId);
        public string GetErrors();

        ApiResponse<TotalDetailsResponse<List<Customer>>> GetListCustomers(Param param);
    }
}
