using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double? Vat { get; set; }
        public double? Cost { get; set; }
        public double? Total { get; set; }
        public double? TotalBeforeVatAndDiscount { get; set; }
        public double? Discount { get; set; }
        public int OrderId { get; set; }
        public string? Notes { get; set; }

        public Order Order { get; set; } = null!;
    }
}
