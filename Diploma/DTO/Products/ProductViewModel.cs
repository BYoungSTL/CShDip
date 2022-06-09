using ComputerShopDB.Entities;

namespace DTO.Products;

public class ProductViewModel
{
    public string ProductName { get; set; }
    public byte[] Image { get; set; }
    public IEnumerable<Characteristics> Characteristics { get; set; }
    public decimal Price { get; set; }
}