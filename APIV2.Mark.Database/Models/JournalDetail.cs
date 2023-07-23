using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class JournalDetail
    {
        [Key]
        public long Id { get; set; }
        public long? JournalId { get; set; }
        public long? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public string? Description { get; set; }

        public virtual Journal? Journal { get; set; }
    }
}
