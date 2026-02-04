using SmartOrder.Domain.Common;
using SmartOrder.Domain.ValueObjects;

namespace SmartOrder.Domain.Aggregates;

public class Order : Entity
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public bool IsPaid { get; private set; }
    public bool IsCancelled { get; private set; }

    public Money TotalAmount => 
        _items.Aggregate(new Money(0, "USD"), (current , item) => current.Add(item.Total));
    
    public void AddItem(Guid productId,Money price,int quantity)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled order");

        _items.Add(new OrderItem(productId, price, quantity));
    }

    protected Order() { }


    public void MarkAsPaid()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cancelled order cannot be paid");

        if (IsPaid)
            throw new InvalidOperationException("Order is already paid");

        IsPaid = true;
    }


    public void Cancel(string reason)
    {
        if(IsPaid)
            throw new InvalidOperationException("Paid order cannot be cancelled");

        if (IsCancelled)
            throw new InvalidOperationException("Order is already cancelled");

        IsCancelled = true;
    }


    public static Order Create()
    {
        return new Order();
    }
}