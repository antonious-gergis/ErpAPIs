
namespace APIV2.Mark.Entities.Dtos
{
    public class OrderDto
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string? Uuid { get; set; }
        public int? OrderState { get; set; }
        public string? Notes { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public double? SubTotal { get; set; }
        public double? GrandTotal { get; set; }
        public double? PaidAmpunt { get; set; }
        public double? Discount { get; set; }
        public int? PaymentMethod { get; set; }
        public string? InvoiceNumber { get; set; }
        public long CustomerId { get; set; }
        public long WarehouseId { get; set; }
        public double? TotalVat { get; set; }
        public long? AccountId { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
