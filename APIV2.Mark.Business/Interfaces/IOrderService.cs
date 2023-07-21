
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IOrderService
    {
        ApiResponse<Order> GetItem(int id);

        Task<ApiResponse<bool>> Create(OrderDto request);

        ApiResponse<bool> Edit(OrderDto order);

        ApiResponse<bool> Delete(int id);
         
        ApiResponse<TotalDetailsResponse<List<OrderDto>>> GetListOrders(Param param);
    }
}
