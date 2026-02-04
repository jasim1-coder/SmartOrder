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
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<Guid> CreateProductAsync(string name, decimal price, string currency)
        {
            var product = Product.Create(name, new Money(price, currency));

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return product.Id;
        }
    }
}
