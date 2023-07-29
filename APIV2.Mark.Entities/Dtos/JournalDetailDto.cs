using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.Dtos
{
    public class JournalDetailDto
    {
        public long Id { get; set; }
        public long? JournalId { get; set; }
        public long? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public string? Description { get; set; }
   
    }
}
