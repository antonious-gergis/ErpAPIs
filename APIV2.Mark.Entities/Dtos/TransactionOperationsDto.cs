using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.Dtos
{
    public class TransactionOperationsDto
    {
        public long Id { get; set; }
        public string OperationName { get; set; } = null!;
        public long OperationId { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Description { get; set; }
        public long EmpId { get; set; }
        public string OperationCode { get; set; } = null!;
    }
}
