using DAL.ODS.Models.Order;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderClass>>> GetAll()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderClass>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
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

        [HttpPost]
        public async Task<ActionResult<OrderClass>> Create(OrderClass order)
        {
            var created = await _orderService.CreateOrderAsync(order);
            return Ok(created);
        }

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
