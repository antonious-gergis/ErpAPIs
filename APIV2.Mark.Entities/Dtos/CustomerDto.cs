namespace APIV2.Mark.Entities.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? NameEn { get; set; } 
        public string? NameAr { get; set; }  
        public string? Address { get; set; }
        public string? Phone { get; set; } 
        public string? Mail { get; set; }
        public string? Notes { get; set; }
        public string? Code { get; set; } 
        public string? AnotherCode { get; set; }
        public int StatusId { get; set; }
    }
}
