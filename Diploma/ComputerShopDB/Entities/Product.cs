using ComputerShopDB.Entities.BaseEntity;

namespace ComputerShopDB.Entities;

public class Product : Entity<int>
{
    public const int MaxDescriptionLength = 256;
    public const int MaxNameLength = 64;
    public const int MaxCategoryLength = 64;
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public byte[] Image { get; set; }
    public List<Characteristics> Characteristics { get; set; }
    public List<Order> Orders { get; set; }
    public List<ProductsOrders> ProductsOrders { get; set; }
}