using SmartOrder.Domain.Common;
using SmartOrder.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder.Domain.Aggregates
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public Money Price { get; private set; }

        public bool IsActive {  get; private set; }

        protected Product() { }

        private Product(string name, Money price)
        {
            Name = name;
            Price = price;
            IsActive = true;
        }

        public static Product Create(string name, Money price)
        {
            if(string.IsNullOrEmpty(name)) 
                throw new InvalidOperationException("Product Name is Required");

            if(price.Amount <= 0)
                throw new InvalidOperationException("Product Price must be positive");

            return new Product(name, price);
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
