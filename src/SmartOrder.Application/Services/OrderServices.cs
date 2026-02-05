using SmartOrder.Application.DTOs;
using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.Repositories;


namespace SmartOrder.Application.Services
{
    public class OrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;

        public OrderServices(IOrderRepository orderRepository, ProductService productService, CustomerService customerService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task<Guid> CreateOrderAsync(Guid customerId)
        {

            await _customerService.GetEligibleCustomerAsync(customerId);

            var order = Order.Create(customerId);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task AddItemToOrderAsync(Guid orderId, Guid productId , int quantity, Guid custoemrId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            
            if (order is null)
                throw new InvalidOperationException("Order not found");


            EnsureOwnership(order, custoemrId);


               //CROSS-AGGREGATE VALIDATION

               var product = await _productService.GetActiveProductAsync(productId);

            order.AddItem(
                product.Id,
                product.Price,
                quantity
            );

            await _orderRepository.SaveChangesAsync();
        }

        public async Task PayOrderAsync(Guid orderId , Guid customerId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new InvalidOperationException("Order not found");

            //  OWNERSHIP CHECK
            EnsureOwnership(order, customerId);

            order.MarkAsPaid();
            await _orderRepository.SaveChangesAsync();
        }

        public async Task CancelOrderAsync(Guid orderId, string reason, Guid customerId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new InvalidOperationException("Order not found");

            // OWNERSHIP CHECK
            EnsureOwnership(order, customerId);

            order.Cancel(reason);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                return null;

            var items = order.Items.Select(i => new OrderItemDto(
                
                i.ProductId,
                i.Quantity,
                i.UnitPrice.Amount,
                i.UnitPrice.Currency,
                i.Total.Amount
            )).ToList();

            return new OrderDto(
                order.Id,
                order.CustomerId,
                order.IsPaid,
                order.IsCancelled,
                order.TotalAmount.Amount,
                order.TotalAmount.Currency,
                items
            );
        }

        private static void EnsureOwnership(Order order, Guid customerId)
        {
            if (order.CustomerId != customerId)
                throw new InvalidOperationException("You are not allowed to modify this order");
        }

    }
}