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
    public class CustomerRespository : ICustomerRepository
    {
        private readonly SmartOrderDbContext _context;

        public CustomerRespository(SmartOrderDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
