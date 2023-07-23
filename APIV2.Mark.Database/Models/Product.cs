using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Description { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public string? ImageUrl { get; set; }
        public long? UnitId { get; set; }
        public long? CategoryId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Barcode { get; set; }
        public string? Sku { get; set; }
        public string? Code { get; set; }
        public long EmpId { get; set; }
    }
}
