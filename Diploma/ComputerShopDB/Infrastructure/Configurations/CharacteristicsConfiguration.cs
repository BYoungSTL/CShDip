using ComputerShopDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerShopDB.Infrastructure.Configurations;

public class CharacteristicsConfiguration : IEntityTypeConfiguration<Characteristics>
{
    public void Configure(EntityTypeBuilder<Characteristics> builder)
    {
        builder.ToTable(nameof(Characteristics));

        builder.Property(x => x.Name).IsRequired().HasMaxLength(Characteristics.MaxNameLength);
        builder.Property(x => x.Value).IsRequired().HasMaxLength(Characteristics.MaxValueLength);
        builder.Property(x => x.ProductId).IsRequired();
        
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Characteristics).HasForeignKey(x=>x.ProductId);
    }
}