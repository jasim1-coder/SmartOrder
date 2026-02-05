using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOrder.Domain.Aggregates;

namespace SmartOrder.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
               .ValueGeneratedNever();

        builder.Property(o => o.CustomerId)
       .IsRequired();


        builder.Property(o => o.IsPaid).IsRequired();
        builder.Property(o => o.IsCancelled).IsRequired();


        //  IMPORTANT: Ignore read-only navigation
        builder.Ignore(o => o.Items);

        // Configure owned collection via backing field
        builder.OwnsMany<OrderItem>("_items", b =>
        {
            b.ToTable("OrderItems");

            b.WithOwner()
             .HasForeignKey("OrderId");

            // 1. Define the Primary Key
            b.HasKey(i => i.Id);

            // 2. CRITICAL FIX: Tell EF Core NOT to expect the DB to generate this.
            // Since you call Guid.NewGuid() in your constructor, this prevents
            // EF Core from thinking the entity is already existing (Update vs Insert).
            b.Property(i => i.Id)
             .ValueGeneratedNever();

            b.Property(i => i.ProductId).IsRequired();
            b.Property(i => i.Quantity).IsRequired();

            b.OwnsOne(i => i.UnitPrice, money =>
            {
                money.Property(m => m.Amount)
                     .HasPrecision(18, 2)
                     .IsRequired();

                money.Property(m => m.Currency)
                     .HasMaxLength(3)
                     .IsRequired();
            });
        });

        builder.Navigation("_items")
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }

}



