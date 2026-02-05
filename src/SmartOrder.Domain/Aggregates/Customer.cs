using SmartOrder.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder.Domain.Aggregates
{
    public class Customer : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool IsBlocked { get; private set; }


        protected Customer() { }

        private Customer(string name, string email)
        {
            Name = name;
            Email = email;
            IsBlocked = false;
        }

        public static Customer Create(string name, string email)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Customer Name is required");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Cutomer Email is required");

            return new Customer(name, email);
        }

        public void Block()
        {
            IsBlocked = true;
        }

        public void UnBlock()
        {
            IsBlocked = false;
        }
    }
}
