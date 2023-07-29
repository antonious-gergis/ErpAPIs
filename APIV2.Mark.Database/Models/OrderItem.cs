using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double? Vat { get; set; } = 0;
        public double? Cost { get; set; }
        public double? Total { get; set; } = 0;
        public double? TotalBeforeVatAndDiscount { get; set; } = 0;
        public double? Discount { get; set; } = 0;
        public long OrderId { get; set; }
        public string? Notes { get; set; }

        public Order Order { get; set; } = null!;
    }
}
