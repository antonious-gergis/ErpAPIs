using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.Dtos
{
    public class TreasuryDto
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public double Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public long EmpId { get; set; }
    }
}
