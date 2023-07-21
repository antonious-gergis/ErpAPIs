using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Database.Models
{
    public class PurchaseItems
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double? Vat { get; set; }
        public double? Cost { get; set; }
        public double? Total { get; set; }
        public int PurchaseId { get; set; }
        public string? Notes { get; set; }

        public Purchase Purchase { get; set; } = null!;
    }
}
