using SmartOrder.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SmartOrder.Application.Services;
using SmartOrder.Domain.Aggregates;

namespace SmartOrder.Application.Tests.Orders
{
    public class OrderServicesTests
    {
        private readonly Mock<IOrderRepository> _orderRepo = new();
        private readonly Mock<IProductRepository> _productRepo = new();
        private readonly Mock<ICustomerRepository> _customerRepo = new();

        private OrderServices CreateService()
        {
            var productService = new ProductService(_productRepo.Object);
            var customerService = new CustomerService(_customerRepo.Object);

            return new OrderServices(_orderRepo.Object, productService, customerService);
        }
        [Fact]
        public async Task CreatOrder_Should_Fail_When_Customer_Is_Blocked()
        {
            var customerId = Guid.NewGuid();
            var customer = Customer.Create("Test", "test@gmail.com");
            customer.Block();

            _customerRepo.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);

            var service = CreateService();

            await Assert.ThrowsAsync<InvalidOperationException>(() => 
            service.CreateOrderAsync(customerId));
        }

    }
}
~