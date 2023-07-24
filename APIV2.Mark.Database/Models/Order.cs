using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string? Uuid { get; set; }
        public int? OrderState { get; set; }
        public string? Notes { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public double? SubTotal { get; set; } = 0;
        public double? GrandTotal { get; set; } = 0;
        public double? PaidAmpunt { get; set; } = 0;
        public double? Discount { get; set; }
        public int? PaymentMethod { get; set; }
        public string? InvoiceNumber { get; set; }
        public long CustomerId { get; set; }
        public long? WarehouseId { get; set; }
        public double? TotalVat { get; set; } = 0;
        public long? AccountId { get; set; }
        public long EmpId { get; set; }
        public string? CustomerNameEn { get; set; }
        public string? CustomerNameAr { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
