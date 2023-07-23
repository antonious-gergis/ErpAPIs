using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities
{
    public class Enums
    {
        public enum TransactionState : int
        {
            Pending = 1,
            Post = 2,
            Void = 3
        }
    }
}
