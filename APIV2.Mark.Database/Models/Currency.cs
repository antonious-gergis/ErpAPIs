using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Currency
    {
        [Key]
        public long Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Country { get; set; }
        public double? Price { get; set; }
        public string? Code { get; set; }
        public double? ExchangeRate { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long EmpId { get; set; }
    }
}
