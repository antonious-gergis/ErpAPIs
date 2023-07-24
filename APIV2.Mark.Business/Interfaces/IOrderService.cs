
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<Order>> GetItem(long id);

        Task<ApiResponse<bool>> Create(OrderDto request);

        ApiResponse<bool> Edit(OrderDto order);

        ApiResponse<bool> Delete(int id);
         
        ApiResponse<TotalDetailsResponse<List<OrderDto>>> GetListOrders(Param param);
        Task<ApiResponse<bool>> CalculateInvoice(long id);
    }
}
