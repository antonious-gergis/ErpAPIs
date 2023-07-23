using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Database.Models
{
    public class Receipt
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public int OrderId { get; set; }
        public string Code { get; set; } = null!;
        public float Amount { get; set; }
        public long CustomerId { get; set; }
        public long EmpId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Notes { get; set; }
    }
}
