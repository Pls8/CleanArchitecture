using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Order
{
    public enum OrderStatusEnums
    {
        Pending = 0,
        Confirmed = 1,
        Shipping = 2,
        Delivered = 3,
        Cancelled = 4
    }
}
