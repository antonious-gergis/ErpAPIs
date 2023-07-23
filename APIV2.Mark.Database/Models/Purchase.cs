using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Database.Models
{
    public class Purchase
    {
        [Key]
        public long Id { get; set; }
        public string PurchaseNumber { get; set; } = null!;
        public string? Uuid { get; set; }
        public int? PurchaseState { get; set; }
        public string? Notes { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public double? SubTotal { get; set; }
        public double? GrandTotal { get; set; }
        public double? Discount { get; set; }
        public int? PaymentMethod { get; set; }
        public string? InvoiceNumber { get; set; }
        public long VendorId { get; set; }
        public long? WarehouseId { get; set; }
        public double? TotalVat { get; set; }
        public long? AccountId { get; set; }
        public int StatusId { get; set; }
        public ICollection<PurchaseItems> PurchaseItems { get; set; } = new List<PurchaseItems>();
        public long EmpId { get; set; }
    }
}
