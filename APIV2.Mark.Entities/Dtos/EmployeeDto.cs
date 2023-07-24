using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Code { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; } = null!;
        public string? Mail { get; set; }
        public string? Notes { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long EmpId { get; set; }
    }
}
