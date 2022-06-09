using ComputerShopDB.Entities;
using ComputerShopDB.Infrastructure.Repositories.BaseRep;
using ComputerShopDB.RepositoriesInterfaces.EntitiesRep;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDB.Infrastructure.Repositories.EntitiesReps;

public class ProductsOrdersRepository : Repository<ProductsOrders, int>, IProductsOrdersRepository
{
    public ProductsOrdersRepository(DbContext context) : base(context)
    {
    }
}