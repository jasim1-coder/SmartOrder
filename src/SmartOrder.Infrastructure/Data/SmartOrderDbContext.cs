

using Microsoft.EntityFrameworkCore;
using SmartOrder.Domain.Aggregates;

namespace SmartOrder.Infrastructure.Data
{
    public class SmartOrderDbContext : DbContext
    {
        public SmartOrderDbContext(DbContextOptions<SmartOrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartOrderDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
