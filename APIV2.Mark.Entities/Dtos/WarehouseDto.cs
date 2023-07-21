namespace APIV2.Mark.Entities.Dtos
{
    public class WarehouseDto
    {
        public int Id { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; } 
        public string? Code { get; set; } 
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
