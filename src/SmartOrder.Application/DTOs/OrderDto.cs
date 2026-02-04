using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder.Application.DTOs
{

    public record OrderItemDto(
        Guid ProductId,
        int Quantity,
        decimal UnitPrice,
        string Currency,
        decimal Total
        );

    public record OrderDto(
        Guid Id,
        bool IsPaid,
        bool IsCancelled,
        decimal TotalAmount,
        string Currency,
        IReadOnlyList<OrderItemDto> Items);
}
