namespace APIV2.Mark.Entities.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public double? Tax { get; set; }
        public string? ImageUrl { get; set; }
        public string? Unit { get; set; }
        public string? Category { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Barcode { get; set; }
        public string? Sku { get; set; }
        public string? Code { get; set; }
        public long EmpId { get; set; }
    }
}
