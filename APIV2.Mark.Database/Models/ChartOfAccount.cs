using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIV2.Mark.Database.Models
{
    public class ChartOfAccount
    {
        public int Id { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? OfficialCode { get; set; }
        public int? CurrencyId { get; set; }
        public int? AccountLevel { get; set; }
        public double? Balance { get; set; }
        public string? AccountNature { get; set; }
        public string? AccountType { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int? ParentId { get; set; }
        public ChartOfAccount? AccountParent { get; set; }
    }
}
