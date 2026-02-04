using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOrder.Application.DTOs;
using SmartOrder.Application.Services;
using System.Threading.Tasks;

namespace SmartOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderServices _ordersService;

        public OrdersController(OrderServices ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var orderId = await _ordersService.CreateOrderAsync();
            return Ok(orderId);
        }

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItem(Guid orderId,[FromBody] AddOrderItemRequest request)
        {
            await _ordersService.AddItemToOrderAsync(orderId,request.ProductId,request.Quantity );
            return NoContent();
        }

        [HttpPost("{orderId}/pay")]
        public async Task<IActionResult> Pay(Guid orderId)
        {
            await _ordersService.PayOrderAsync(orderId);
            return NoContent();
        }
        [HttpPost("{orderId}/cancel")]
        public async Task<IActionResult> Cancel(Guid orderId, [FromBody] CancelOrderRequest request)
        {
            await _ordersService.CancelOrderAsync(orderId, request.Reason);
            return NoContent();
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            var order = await _ordersService.GetOrderByIdAsync(orderId);

            if (order is null)
                return NotFound();

            return Ok(order);
        }


    }
}
