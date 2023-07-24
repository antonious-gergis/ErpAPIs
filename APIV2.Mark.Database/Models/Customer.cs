using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Address { get; set; }
        public string Phone { get; set; } = null!;
        public string? Mail { get; set; }
        public string? Notes { get; set; }
        public string Code { get; set; } = null!;
        public string? AnotherCode { get; set; }
        public int StatusId { get; set; } = 1;
        public double Balance { get; set; } = 0;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long EmpId { get; set; }
    }
}
