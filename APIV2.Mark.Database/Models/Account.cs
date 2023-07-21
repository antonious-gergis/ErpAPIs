using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Database.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Code { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? AccountType { get; set; }
        public long? AccountId { get; set; }
        public double? Balance { get; set; }
        public string? Description { get; set; }
    }
}
