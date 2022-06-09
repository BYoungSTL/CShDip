using ComputerShopDB.Entities.BaseEntity;

namespace ComputerShopDB.Entities;

public class Characteristics : Entity<int>
{
    public const int MaxNameLength = 32;
    public const int MaxValueLength = 128;
    
    public string Name { get; set; }
    public string Value { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
}