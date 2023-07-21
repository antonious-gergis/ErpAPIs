using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Database.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Address { get; set; }
        public string? EbanNumber { get; set; }
        public string AccountNumber { get; set; } = null!;
        public double? Balance { get; set; }
        public string? Code { get; set; }
        public int? CurrencyId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
