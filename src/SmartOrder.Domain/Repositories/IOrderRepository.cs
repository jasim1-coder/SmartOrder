using SmartOrder.Domain.Aggregates;


namespace SmartOrder.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);

        Task SaveChangesAsync();
    }
}
