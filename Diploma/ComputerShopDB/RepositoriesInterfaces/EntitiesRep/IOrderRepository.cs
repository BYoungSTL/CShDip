using ComputerShopDB.Entities;
using ComputerShopDB.RepositoriesInterfaces.BaseRep;

namespace ComputerShopDB.RepositoriesInterfaces.EntitiesRep;

public interface IOrderRepository : IRepository<Order, int>
{
    public void CalculatePrice(int id);
}