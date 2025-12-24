using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public enum OrderStatusEnums
    {
        Pending = 0,
        Shipped = 1,
        Delivered = 2,
        Cancelled = 3
    }
}
