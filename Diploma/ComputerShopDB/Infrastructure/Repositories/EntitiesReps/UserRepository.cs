using ComputerShopDB.Entities;
using ComputerShopDB.Infrastructure.Repositories.BaseRep;
using ComputerShopDB.RepositoriesInterfaces.EntitiesRep;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDB.Infrastructure.Repositories.EntitiesReps;

public class UserRepository : Repository<User, int>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}