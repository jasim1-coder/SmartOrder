using Microsoft.EntityFrameworkCore;
using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.Repositories;
using SmartOrder.Infrastructure.Data;


namespace SmartOrder.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SmartOrderDbContext _context ;

        public OrderRepository(SmartOrderDbContext context) 
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include (o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders .AddAsync(order);
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
