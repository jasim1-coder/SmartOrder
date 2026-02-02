using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder.Application.Services
{
    public class OrderServices
    {
        public Order CreateOrder()
        {
            return new Order();
        }
    }
}
