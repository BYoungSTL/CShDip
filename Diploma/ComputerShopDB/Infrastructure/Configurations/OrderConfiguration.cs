using ComputerShopDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerShopDB.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));

        builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(6, 2).IsRequired();
        builder.Property(x => x.Status).IsRequired().HasMaxLength(Order.MaxStatusLength);
        builder.Property(x => x.UserId).IsRequired();
        
        builder.HasMany(x => x.Products)
            .WithMany(x => x.Orders)
            .UsingEntity<ProductsOrders>(
                j=>j
                    .HasOne(x=>x.Product)
                    .WithMany(x=>x.ProductsOrders)
                    .HasForeignKey(x=>x.ProductId),
                j=> j
                    .HasOne(x=>x.Order)
                    .WithMany(x=>x.ProductsOrders)
                    .HasForeignKey(x=>x.OrderId),
                j =>
                {
                    j.ToTable(nameof(ProductsOrders));
                    j.Property(x => x.OrderId).IsRequired();
                    j.Property(x => x.ProductId).IsRequired();
                });
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x=>x.UserId);
    }   
}