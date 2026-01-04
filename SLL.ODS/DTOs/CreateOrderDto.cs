using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.ODS.DTOs
{
    public class CreateOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

}
