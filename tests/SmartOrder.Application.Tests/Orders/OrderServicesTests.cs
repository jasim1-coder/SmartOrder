using SmartOrder.Domain.Repositories;
using Moq;
using SmartOrder.Application.Services;
using SmartOrder.Domain.Aggregates;
using FluentAssertions;

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

        [Fact]
        public async Task CreateOrder_Should_Create_Order_For_Eligible_Customer()
        {
            var customerId = Guid.NewGuid();
            var customer = Customer.Create("Test", "test@mail.com");

            _customerRepo.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);

            _orderRepo.Setup(r => r.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);


            _orderRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var service = CreateService();

            var orderId = await service.CreateOrderAsync(customerId);

            orderId.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task AddItem_Should_Fail_When_Customer_Is_Not_Order_Owner()
        {
            var ownerId = Guid.NewGuid();
            var attackerId = Guid.NewGuid();
            var order = Order.Create(ownerId);

            _orderRepo
                .Setup(r => r.GetByIdAsync(order.Id))
                .ReturnsAsync(order);

            var service = CreateService();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.AddItemToOrderAsync(order.Id,Guid.NewGuid(),1,attackerId)
            );
        }


    }
}
