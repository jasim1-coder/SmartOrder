namespace SmartOrder.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money() { }

    public Money (decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        Amount = amount;
        Currency = currency;
    }
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Currency mismatch");

        return new Money(Amount +  other.Amount, Currency);
    }
}