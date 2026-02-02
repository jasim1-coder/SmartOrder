using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOrder.Domain.Aggregates;

namespace SmartOrder.Infrastructure.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.ProductId)
               .IsRequired();

        builder.Property(i => i.Quantity)
               .IsRequired();

        // Money value object mapping
        builder.OwnsOne(i => i.UnitPrice, money =>
        {
            money.Property(m => m.Amount)
                 .HasColumnName("UnitPriceAmount")
                 .HasPrecision(18, 2)
                 .IsRequired();

            money.Property(m => m.Currency)
                 .HasColumnName("UnitPriceCurrency")
                 .HasMaxLength(3)
                 .IsRequired();
        });
    }
}
