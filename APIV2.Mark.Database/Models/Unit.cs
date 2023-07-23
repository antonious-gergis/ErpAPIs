using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Unit
    {
        [Key]
        public long Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Code { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public long EmpId { get; set; }
    }
}
