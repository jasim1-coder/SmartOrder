using Microsoft.EntityFrameworkCore;
using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.Repositories;
using SmartOrder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SmartOrderDbContext _context;

        public ProductRepository(SmartOrderDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
