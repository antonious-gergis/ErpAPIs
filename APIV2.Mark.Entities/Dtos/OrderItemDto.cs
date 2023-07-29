namespace APIV2.Mark.Entities.Dtos
{
    public class OrderItemDto
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double? Vat { get; set; }
        public double? Cost { get; set; }
        public double? Total { get; set; }
        public double? Discount { get; set; }
        public double? TotalBeforeVatAndDiscount { get; set; }
        public long OrderId { get; set; }
        public string? Notes { get; set; }
    }
}
