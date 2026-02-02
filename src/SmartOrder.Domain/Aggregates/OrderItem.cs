using SmartOrder.Domain.Common;
using SmartOrder.Domain.ValueObjects;

namespace SmartOrder.Domain.Aggregates;

public class OrderItem : Entity
{

    protected OrderItem() { }
    public Guid ProductId { get; }
    public int Quantity { get; }
    public Money UnitPrice { get; }

    public Money Total => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    internal OrderItem(Guid productId, Money unitPrice, int quantity)
    {
        if(quantity < 0)
            throw new ArgumentException("Quantity must be greater than zero");

        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;

    }
}