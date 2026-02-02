using SmartOrder.Domain.Common;
using SmartOrder.Domain.ValueObjects;

namespace SmartOrder.Domain.Aggregates;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    protected OrderItem() { }

    public OrderItem(Guid productId, Money unitPrice, int quantity)
    {
        Id = Guid.NewGuid();

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public Money Total =>
        new(UnitPrice.Amount * Quantity, UnitPrice.Currency);
}
