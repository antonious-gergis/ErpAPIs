namespace APIV2.Mark.Entities.Dtos
{
    public class BankDto
    {
        public int Id { get; set; }
        public string? NameEn { get; set; }  
        public string? NameAr { get; set; } 
        public string? Address { get; set; }
        public string? EbanNumber { get; set; }
        public string? AccountNumber { get; set; }
        public double? Balance { get; set; }
        public string? Code { get; set; }
        public int? CurrencyId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
