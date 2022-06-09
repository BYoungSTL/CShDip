using System.Data.SqlClient;
using System.Linq.Expressions;
using ComputerShopDB.Entities.BaseEntity;
using ComputerShopDB.Exceptions;
using ComputerShopDB.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDB.Infrastructure.Repositories.BaseRep;

public abstract class Repository<TEntity, TKey> where TEntity : Entity<TKey>
{
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(DbContext context) => DbSet = context.Set<TEntity>();

    public virtual TEntity Find(TKey id) => Find(x => x.Id.Equals(id));

    public TEntity Find(Expression<Func<TEntity, bool>> predicate)
    {
        Check.ArgumentNotNull(predicate, nameof(predicate));

        try
        {
            return InternalFind(predicate);
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

    public IEnumerable<TEntity> All() => Filter(x => true);

    public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
    {
        Check.ArgumentNotNull(predicate, nameof(predicate));

        try
        {
            return InternalFilter(predicate);
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

    public void Add(TEntity entity)
    {
        Check.ArgumentNotNull(entity, nameof(entity));

        DbSet.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        Check.ArgumentNotNull(entity, nameof(entity));

        DbSet.Remove(entity);
    }

    protected virtual TEntity InternalFind(Expression<Func<TEntity, bool>> predicate)
    {
        return MakeInclusions().SingleOrDefault(predicate)!;
    }

    protected virtual IEnumerable<TEntity> InternalFilter(Expression<Func<TEntity, bool>> predicate)
    {
        return MakeInclusions().Where(predicate).ToList();
    }

    protected virtual IQueryable<TEntity> MakeInclusions() => DbSet;
}