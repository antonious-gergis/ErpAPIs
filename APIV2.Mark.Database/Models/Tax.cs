using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Database.Models
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Type { get; set; }
        public string? Code { get; set; }
        public float Percentage { get; set; }
        public int StatusId { get; set; }
        public double Balance { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
