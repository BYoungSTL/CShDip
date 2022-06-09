using ComputerShopDB.Common.Helpers;
using ComputerShopDB.Entities.BaseEntity;

namespace ComputerShopDB.Entities;

public class User : Entity<int>
{
    public const int MaxNameLength = 64;
    public const int MaxEmailLength = 128;
    public const int MaxPhoneNumberLength = 12;
    public const int MaxAddressLength = 128;
    public const int MaxPasswordLength = 64;
    public const int MaxPasswordHashLength = 64;
    public const int MaxPasswordSaltLength = 64;

    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string Address { get; set; }
    public List<Order> Orders { get; set; }
    
    public bool VerifyPassword(string password)
    {
        return !string.IsNullOrEmpty(password) &&
               PasswordHelper.ComputeHash(password, PasswordSalt) == PasswordHash;
    }

    public void ChangePassword(string password)
    {
        PasswordSalt = PasswordHelper.GenerateSalt(MaxPasswordSaltLength);
        PasswordHash = PasswordHelper.ComputeHash(password, PasswordSalt);
    }
}