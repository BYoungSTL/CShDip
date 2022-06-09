using System.Linq.Expressions;
using ComputerShopDB.Common.Paging;
using ComputerShopDB.Entities;
using ComputerShopDB.RepositoriesInterfaces.BaseRep;

namespace ComputerShopDB.RepositoriesInterfaces.EntitiesRep;

public interface IProductRepository : IRepository<Product, int>
{
    public PagedList<Product> GetPagedList(PageInfo pageInfo, Expression<Func<Product, bool>> predicate);
}