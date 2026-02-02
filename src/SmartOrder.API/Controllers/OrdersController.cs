using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOrder.Application.Services;

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
        public IActionResult Create()
        {
            var order = _ordersService.CreateOrder();
            return Ok(order.Id);
        }
    }
}
