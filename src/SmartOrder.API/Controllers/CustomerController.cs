using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOrder.Application.Services;

namespace SmartOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(string name, string email)
        {
            var id = await _customerService.CreateCustomerAsync(name, email);
            return CreatedAtAction(nameof(Create), new { id }, id);
        }

    }
}
