using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.ODS.Models
{
    public class DashboardViewModel
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public List<RecentOrderViewModel> RecentOrders { get; set; } = new();
        public List<LowStockProductViewModel> LowStockProducts { get; set; } = new();
    }
}
