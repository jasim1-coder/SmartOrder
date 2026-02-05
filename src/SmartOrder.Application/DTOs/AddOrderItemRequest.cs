using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder.Application.DTOs
{
    public record AddOrderItemRequest(
        Guid ProductId,
        int Quantity,
        Guid CustomerId
    );

}
