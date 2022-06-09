using ComputerShopDB.Entities.BaseEntity;

namespace ComputerShopDB.Entities;

public class ProductsOrders : Entity<int>
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}