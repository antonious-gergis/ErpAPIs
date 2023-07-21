using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Address { get; set; }
        public string? Code { get; set; }
        public string? Phone { get; set; }
        public string? Mail { get; set; }
        public string? AnotherCode { get; set; }
        public int StatusId { get; set; }
        public double Balance { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
