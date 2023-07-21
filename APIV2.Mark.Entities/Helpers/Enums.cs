using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.Helpers
{
    public enum JournalStatus
    {
        Pending=1,
        Post=2,
        Void=3
    }

    public enum OrderStatus
    {
        Pending = 1,
        Accept = 2,
        Done = 3
    }
    public enum PaymentMethod
    {
        Cash = 1,
        Credit = 2,
        Cheque = 3
    }
    public enum Status
    {
        Active = 1,
        Suspend = 2,
        Deleted = 3,
    }
}
