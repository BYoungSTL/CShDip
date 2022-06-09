using System.Data.SqlClient;
using System.Linq.Expressions;
using ComputerShopDB.Common.Paging;
using ComputerShopDB.Entities;
using ComputerShopDB.Exceptions;
using ComputerShopDB.Helpers;
using ComputerShopDB.Infrastructure.Repositories.BaseRep;
using ComputerShopDB.RepositoriesInterfaces.EntitiesRep;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDB.Infrastructure.Repositories.EntitiesReps;

public class ProductRepository : Repository<Product, int>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }
    
    public PagedList<Product> GetPagedList(PageInfo pageInfo, Expression<Func<Product, bool>> predicate = null)
    {
        Check.ArgumentNotNull(pageInfo, nameof(pageInfo));

        try
        {
            var query = MakeInclusions();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var items = query.SelectPage(pageInfo).ToList();
            var total = query.Count();

            return new PagedList<Product>(items, total, pageInfo);
        }
        catch (SqlException ex)
        {
            throw ex.ToRepositoryException();
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message, ex);
        }
    }
}