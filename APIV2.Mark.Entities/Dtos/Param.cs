
namespace APIV2.Mark.Entities.Dtos
{
    public class Param
    { 
        public string SearchText { get; set; } = "";
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
