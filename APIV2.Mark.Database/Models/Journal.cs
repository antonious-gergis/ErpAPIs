using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Journal
    {
        [Key]
        public int Id { get; set; }
        public int? CurrencyId { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? StatusId { get; set; }
        public double? Amount { get; set; }

        public ICollection<JournalDetail> JournalDetails { get; set; }
    }
}
