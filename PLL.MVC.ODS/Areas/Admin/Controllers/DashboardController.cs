using DAL.ODS.Interfaces;
using DAL.ODS.Models.Order;
using DAL.ODS.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLL.ODS.Interfaces;
using SLL.ODS.Models;

namespace PLL.MVC.ODS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {

        private readonly IGenericRepo<OrderClass> _orderRepo;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public DashboardController(
            IGenericRepo<OrderClass> orderRepo,
            IProductService productService,
            ICategoryService categoryService)
        {
            _orderRepo = orderRepo;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var allOrders = await _orderRepo.GetAllAsync();
                var products = await _productService.GetAllProductsAsync();
                var categories = await _categoryService.GetAllCategoriesAsync();

                var totalOrders = allOrders.Count();
                var totalRevenue = allOrders.Sum(o => o.TotalAmount);

                var recentOrders = allOrders
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .Select(o => new RecentOrderViewModel
                    {
                        Id = o.Id,
                        CustomerName = GetCustomerName(o),
                        TotalAmount = o.TotalAmount,
                        Status = GetStatusText(o.Status),
                        OrderDate = o.OrderDate
                    })
                    .ToList();

                var dashboardData = new DashboardViewModel
                {
                    TotalOrders = totalOrders,
                    TotalRevenue = totalRevenue,
                    TotalProducts = products.Count(),
                    TotalCategories = categories.Count(),
                    RecentOrders = recentOrders,
                    LowStockProducts = GetLowStockProducts(products)
                };

                return View(dashboardData);
            }
            catch (Exception)
            {
                return View(new DashboardViewModel());
            }
        }

        private List<LowStockProductViewModel> GetLowStockProducts(IEnumerable<ProductClass> products)
        {
            return products
                .Where(p => p.StockQuantity > 0 && p.StockQuantity <= 5)
                .Take(5)
                .Select(p => new LowStockProductViewModel
                {
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .ToList();
        }

        private string GetCustomerName(OrderClass order)
        {
            if (order.AppUser != null)
            {
                return $"{order.AppUser.FirstName} {order.AppUser.LastName}";
            }
            return "Customer";
        }

        private string GetStatusText(OrderStatusEnums status)
        {
            return status.ToString();
        }
    }
}

