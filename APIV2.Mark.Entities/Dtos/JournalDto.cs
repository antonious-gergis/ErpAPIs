using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.Dtos
{
    public class JournalDto
    {
        public int Id { get; set; }
        public int? CurrencyId { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? StatusId { get; set; }
        public double? Amount { get; set; }
        public DateTime? PostedDate { get; set; }
        public long EmpId { get; set; }
        public string? OperationName { get; set; }
        public List<JournalDetailDto> JournalDetails { get; set; } = new List<JournalDetailDto>();
    }
}
