using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class Journal
    {
        [Key]
        public long Id { get; set; }
        public long? CurrencyId { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public int? StatusId { get; set; }
        public double? Amount { get; set; }
        public int TransactionState { get; set; } 
        public long EmpId { get; set; } 

        public ICollection<JournalDetail> JournalDetails { get; set; } = new List<JournalDetail>();
    }
}
