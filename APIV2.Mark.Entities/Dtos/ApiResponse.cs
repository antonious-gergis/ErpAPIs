namespace APIV2.Mark.Entities.Dtos
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public T Data { get; set; }   
    }
}
