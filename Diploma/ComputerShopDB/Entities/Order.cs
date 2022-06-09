using ComputerShopDB.Entities.BaseEntity;

namespace ComputerShopDB.Entities;

public class Order : Entity<int>
{
    public const int MaxStatusLength = 64;
    
    public string Status { get; set; }
    public decimal Price { get; set; }
    public List<Product> Products { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public List<ProductsOrders> ProductsOrders { get; set; }
}