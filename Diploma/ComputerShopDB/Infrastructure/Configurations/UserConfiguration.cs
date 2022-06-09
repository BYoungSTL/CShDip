using ComputerShopDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerShopDB.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasAlternateKey(x => x.UserName);
        builder.HasAlternateKey(x => x.Email);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(User.MaxEmailLength);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(User.MaxAddressLength);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(User.MaxNameLength);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(User.MaxPhoneNumberLength);
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.PasswordSalt).IsRequired();
    }
}