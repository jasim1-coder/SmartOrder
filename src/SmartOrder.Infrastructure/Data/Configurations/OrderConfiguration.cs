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

        builder.Property(o => o.IsPaid)
               .IsRequired();

        builder.Property(o => o.IsCancelled)
               .IsRequired();

        // Configure aggregate relationship
        builder.HasMany(o => o.Items)
               .WithOne()
               .HasForeignKey("OrderId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
