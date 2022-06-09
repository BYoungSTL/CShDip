using ComputerShopDB.Entities;
using ComputerShopDB.Exceptions;
using ComputerShopDB.Helpers;
using ComputerShopDB.Infrastructure.Repositories.BaseRep;
using ComputerShopDB.RepositoriesInterfaces.EntitiesRep;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDB.Infrastructure.Repositories.EntitiesReps;

public class OrderRepository : Repository<Order, int>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context)
    {
    }

    public void CalculatePrice(int id)
    {
        try
        {
            var currentOrder = DbSet.Find(id);
            DbSet.Remove(currentOrder);
            currentOrder.Price = 0;
            var products = currentOrder.Products;
            foreach (var product in products)
            {
                currentOrder.Price += product.Price;
            }

            DbSet.Add(currentOrder);
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