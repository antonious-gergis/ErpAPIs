
using APIV2.Mark.Business.Interfaces;
using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using APIV2.Mark.Entities.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace APIV2.Mark.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly UtilitiyContext _context;
        public OrderService(UtilitiyContext context)
        {
            _context = context;
        } 
        public async Task<ApiResponse<bool>> Create(OrderDto request)
        {
            var result = new ApiResponse<bool>();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var order = new Order();
                if (request != null)
                {


                    var random = new Random();
                    order.OrderNumber = Math.Floor(100000 + random.NextDouble() * 900000).ToString();
                    order.InvoiceNumber = GenerateCode();
                    order.CreationDate = DateTime.Now;
                    order.DueDate = request.DueDate;
                    order.CustomerId = request.CustomerId;
                    order.PaymentMethod = request.PaymentMethod;
                    order.WarehouseId = request.WarehouseId;
                    order.OrderState = (int)OrderStatus.Pending;
                    order.Discount = 0; 
                    order.AccountId = 0; 

                    foreach (var item in request.OrderItems)
                    {
                        var orderItem = new OrderItem();
                        var product = await _context.Products.Where(p => p.Id == item.ProductId).FirstOrDefaultAsync();
                        orderItem.ProductId = item.ProductId;
                        orderItem.Quantity = item.Quantity;
                        orderItem.Price = item.Price;

                        var TotalBeforeDiscount = item.Price * item.Quantity;
                        var TotalAfterVat = (TotalBeforeDiscount + orderItem.Vat);
                        var discount = TotalBeforeDiscount * (item.Discount / 100);
                        var TotalAfterDiscount = TotalBeforeDiscount - discount;

                        var tax = 0 /*Convert.ToDecimal(db.Taxes.Find(details.Product_Tax).Tax_Percentage)*/;
                        orderItem.Vat = TotalAfterDiscount * (tax / 100);
                        order.TotalVat += orderItem.Vat;
                        orderItem.Total = TotalAfterVat - discount;
                        orderItem.TotalBeforeVatAndDiscount = TotalBeforeDiscount;
                       
                        orderItem.Notes = "";
                        orderItem.Cost = product?.Cost;
                        order.GrandTotal += orderItem.Total;
                        order.SubTotal += TotalAfterDiscount;
                        order.OrderItems.Add(orderItem);
                    }

                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();                 

                    transaction.Commit();
                    result.Data = true;
                    result.ErrorCode = (int)HttpStatusCode.OK;
                    result.Message = "Success";
                }
                else
                {
                    transaction.Rollback();
                    result.Data = false;
                    result.ErrorCode = (int)HttpStatusCode.BadRequest;
                    result.Message = "Fail";
                }

                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result.Data = false;
                result.ErrorCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return result;
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<bool> Edit(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<Order> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<TotalDetailsResponse<List<OrderDto>>> GetListOrders(Param param)
        {
            throw new NotImplementedException();
        }
        private string GenerateCode()
        {
            try
            {
                var latestCode = _context.Orders
                          .OrderByDescending(x => x.Id)
                          .Take(1)
                          .Select(x => x.InvoiceNumber)
                          .ToList()
                          .FirstOrDefault();
                var newCode = "";
                if (latestCode != null)
                {
                    var codeString = latestCode.Substring(1);
                    var code = Convert.ToInt64(codeString);
                    code = code + 1;
                    newCode = "N" + code;
                }
                else
                {
                    newCode = "N10000";
                }

                return newCode;
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}
