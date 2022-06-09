using ComputerShopDB.RepositoriesInterfaces.EntitiesRep;

namespace ComputerShopDB.RepositoriesInterfaces;

public interface IUnitOfWork : IDisposable
{
    public IOrderRepository OrderRepository { get; }
    public IUserRepository UserRepository { get; }
    public IProductRepository ProductRepository { get; }
    public ICharacteristicsRepository CharacteristicsRepository { get; }

    void Commit();
}