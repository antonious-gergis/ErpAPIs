
using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace APIV2.Mark.Business.Services
{
    public class CustomerService :ICustomerService
    {
        private readonly UtilitiyContext _context;
        private string _errors = "";
        public CustomerService(UtilitiyContext context)
        {
            _context = context;
        }

        public ApiResponse<bool> Create(Customer customer)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(customer.NameEn, customer.NameAr)) 
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Name is already exist";
                }
                if (customer != null)
                {
                    customer.Code = GenerateCode();
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                    result.Data = true;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }
                
                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            var result = new ApiResponse<bool>();
            try
            {
                var item = GetItem(id);
                if (item.Data == null)
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Customer is not found";
                    return result;
                }

                item.Data.StatusId = 4;
                _context.Customers.Attach(item.Data);
                _context.Entry(item.Data).State = EntityState.Modified;
                _context.SaveChanges();

                result.Data = true;
                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";

                return result;
                
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<bool> Edit(Customer customer)
        {
            var result = new ApiResponse<bool>();
            try
            {
                if (IsItemExists(customer.NameEn, customer.NameAr,customer.AnotherCode, customer.Id)) 
                {
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "This Customer is already exist";
                    return result;
                } 

                _context.Customers.Attach(customer);
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                result.Data = true;
                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";

                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public string GetErrors()
        {
            return _errors;
        }

        public ApiResponse<Customer> GetItem(int id)
        {
            var result = new ApiResponse<Customer>();
            var item = _context.Customers.Where(u => u.Id == id && u.StatusId == 1).FirstOrDefault();
            if (item != null)
            {
                result.Data = item;
                result.ErrorCode = (int)HttpStatusCode.OK;
                result.Message = "Success";
            }
            else
            {
                result.Data = null;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = "Fail";
            } 
            return result;
        }
        public ApiResponse<List<Customer>> GetItems()
        {
            var result = new ApiResponse<List<Customer>>();
            try
            {
                var items = _context.Customers.Where(u => u.StatusId == 1).ToList();
                if (items != null)
                {
                    result.Data = items;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    result.Data = null;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }


        public ApiResponse<TotalDetailsResponse<List<Customer>>> GetListCustomers(Param param)
        {
            var result = new ApiResponse<TotalDetailsResponse<List<Customer>>>();
            var totalDetails = new TotalDetailsResponse<List<Customer>>();
            List<Customer> items;

            if (param.SearchText != "" && param.SearchText != null)
            {
                items = _context.Customers.Where(n => n.StatusId == 1 && (n.NameEn.Contains(param.SearchText) || n.NameAr.Contains(param.SearchText) || n.Code.Contains(param.SearchText)|| n.AnotherCode.Contains(param.SearchText)) )
                    .ToList();
            }
            else
                items = _context.Customers.Where(c =>c.StatusId == 1).ToList();

            totalDetails.PageNumber = param.page;
            totalDetails.PageSize = param.pageSize;
            totalDetails.Total = items.Count;
            items = items.Skip((param.page - 1) * param.pageSize).Take(param.pageSize).ToList();


            
            totalDetails.Data = items;
            result.Data = totalDetails; 
             
            result.ErrorCode = (int)HttpStatusCode.OK;
            result.Message = "Success";

            return result;
        }

        public bool IsCustomerExists(int srcCustomerId)
        {
            int ct = _context.Customers.Where(n => n.Id == srcCustomerId).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr)
        {
            int ct = _context.Customers.Where(n => n.StatusId == 1 && ( n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower())).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string nameEn, string nameAr,string code, int id)
        {
            int ct = _context.Customers.Where(n => n.StatusId == 1 && (n.NameAr.ToLower() == nameAr.ToLower() || n.NameEn.ToLower() == nameEn.ToLower() || n.AnotherCode == code) && n.Id != id).Count();
            if (ct > 0)
            {
                _errors = " NameEn " + nameEn + " or NameAr" + nameAr + "  Exists Already";
                return true;
            }
            else
                return false;
        }

        public string GenerateCode()
        {
            try
            {
                var latestCode = _context.Customers
                       .OrderByDescending(x => x.Id)
                       .Take(1)
                       .Select(x => x.Code)
                       .ToList()
                       .FirstOrDefault();

                var newCode = "";
                if (latestCode != null)
                {
                    var codeString = latestCode.Substring(1);
                    var code = Convert.ToInt64(codeString);
                    code = code + 1;
                    newCode = "C" + code;
                }
                else
                {
                    newCode = "C" + "10000";
                }

                return newCode;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
