using ComputerShopDB.Common.Paging;
using ComputerShopDB.Entities;

namespace DTO.Catalog;

public class CatalogPageViewModel
{
    public PagedList<Product> Products { get; set; }
    public PagedListModel PagedList { get; set; }
    public string? Category { get; set; }
    public string? UserName { get; set; }
}