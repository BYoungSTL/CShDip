using System.Linq.Expressions;
using ComputerShopDB.Entities.BaseEntity;

namespace ComputerShopDB.RepositoriesInterfaces.BaseRep;

public interface IRepository<TEntity, in TKey> where TEntity : Entity<TKey>
{
    TEntity Find(Expression<Func<TEntity, bool>> predicate);
    TEntity Find(TKey id);
    IEnumerable<TEntity> All();
    IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);
    void Remove(TEntity entity);
}