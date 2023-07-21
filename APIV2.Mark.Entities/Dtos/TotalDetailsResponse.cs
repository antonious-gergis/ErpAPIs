namespace APIV2.Mark.Entities.Dtos
{
    public class TotalDetailsResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public T Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
