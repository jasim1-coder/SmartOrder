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

        public OrderServices(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
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

        public async Task AddItemToOrderAsync(Guid orderId,Guid productId,decimal price,string currency,int quantity)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                // If this hits, the ID you are passing in the URL doesn't exist in the DB
                throw new Exception($"Order {orderId} not found in Database!");
            }
            if (order is null)
                throw new InvalidOperationException("Order not found");

            order.AddItem(
                productId,
                new Money(price, currency),
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

    }
}
