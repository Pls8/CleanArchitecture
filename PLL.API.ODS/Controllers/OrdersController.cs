using DAL.ODS.Models.Order;
using Microsoft.AspNetCore.Mvc;
using SLL.ODS.DTOs;
using SLL.ODS.Interfaces;

namespace PLL.API.ODS.Controllers
{
    public class OrdersController : BaseController
    {

        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OrderClass>>> GetAll()
        //{
        //    return Ok(await _orderService.GetAllOrdersAsync());
        //}
        // GET: api/Orders
        [HttpGet]   //added 1-3-2026
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            var result = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.AppUserId,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.OrderDate
            });

            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderClass>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(new OrderDto
            {
                Id = order.Id,
                UserId = order.AppUserId,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedAt = order.OrderDate
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderClass>>> GetByUser(string userId)
        {
            return Ok(await _orderService.GetOrdersByUserIdAsync(userId));
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderClass>>> GetByStatus(OrderStatusEnums status)
        {
            return Ok(await _orderService.GetOrdersByStatusAsync(status));
        }

        //[HttpPost]
        //public async Task<ActionResult<OrderClass>> Create(OrderClass order)
        //{
        //    var created = await _orderService.CreateOrderAsync(order);
        //    return Ok(created);
        //}
        [HttpPost]  // added 1-3-2026
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
        {
            var order = new OrderClass
            {
                AppUserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatusEnums.Pending,
                OrderItems = dto.Items.Select(i => new OrderItemClass
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            var created = await _orderService.CreateOrderAsync(order);

            return Ok(new OrderDto
            {
                Id = created.Id,
                UserId = created.AppUserId,
                TotalAmount = created.TotalAmount,
                Status = created.Status.ToString(),
                CreatedAt = created.OrderDate
            });
        }
        //// this if chnage IOrderService.CreateOrderAsync to use DTOs
        //[HttpPost]  // added 1-3-2026
        //public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto) {
        //    var created = await _orderService.CreateOrderAsync(dto.UserId, dto.Items);

        //    return Ok(new OrderDto
        //    {
        //        Id = created.Id,
        //        UserId = created.AppUserId,
        //        TotalAmount = created.TotalAmount,
        //        Status = created.Status.ToString(),
        //        CreatedAt = created.OrderDate
        //    });
        //}



        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatusEnums status)
        {
            if (!await _orderService.OrderExistsAsync(id))
                return NotFound();

            await _orderService.UpdateOrderStatusAsync(id, status);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Cancel(int id)
        {
            if (!await _orderService.OrderExistsAsync(id))
                return NotFound();

            await _orderService.CancelOrderAsync(id);
            return NoContent();
        }

    }
}
