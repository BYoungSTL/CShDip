using ComputerShopDB.Entities;

namespace DTO.ShoppingCart;

public class ShoppingCartModel
{
    public IEnumerable<Product> Products { get; set; }
    public string Status { get; set; }
    public string UserName { get; set; }
    public decimal Price { get; set; }
}