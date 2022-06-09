using ComputerShopDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerShopDB.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));

        builder.Property(x => x.Category).IsRequired().HasMaxLength(Product.MaxCategoryLength);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(Product.MaxDescriptionLength);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Product.MaxNameLength);
        builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(6, 2).IsRequired();
    }
}