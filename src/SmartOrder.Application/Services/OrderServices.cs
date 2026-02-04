using SmartOrder.Application.DTOs;
using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.Repositories;
using SmartOrder.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder.Application.Services
{
    public class OrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ProductService _productService;

        public OrderServices(IOrderRepository orderRepository, ProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public async Task<Guid> CreateOrderAsync()
        {
            var order = Order.Create();

            //order.AddItem(Guid.NewGuid(), new Money(100, "USD"), 2);
            //order.Cancel("Not the expected size");
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task AddItemToOrderAsync(Guid orderId, Guid productId , int quantity)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            
            if (order is null)
                throw new InvalidOperationException("Order not found");

            //CROSS-AGGREGATE VALIDATION

            var product = await _productService.GetActiveProductAsync(productId);

            order.AddItem(
                product.Id,
                product.Price,
                quantity
            );

            await _orderRepository.SaveChangesAsync();
        }

        public async Task PayOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new InvalidOperationException("Order not found");

            order.MarkAsPaid();
            await _orderRepository.SaveChangesAsync();
        }

        public async Task CancelOrderAsync(Guid orderId, string reason)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new InvalidOperationException("Order not found");

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
                order.IsPaid,
                order.IsCancelled,
                order.TotalAmount.Amount,
                order.TotalAmount.Currency,
                items
            );
        }

    }
}