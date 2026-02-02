using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.Repositories;
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

        public async Task<Guid> GetOrderAsync()
        {
            var order = Order.Create();

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }
    }
}
