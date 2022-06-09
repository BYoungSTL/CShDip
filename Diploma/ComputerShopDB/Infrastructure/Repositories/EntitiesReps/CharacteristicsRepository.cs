using ComputerShopDB.Entities;
using ComputerShopDB.Infrastructure.Repositories.BaseRep;
using ComputerShopDB.RepositoriesInterfaces.EntitiesRep;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDB.Infrastructure.Repositories.EntitiesReps;

public class CharacteristicsRepository : Repository<Characteristics, int>, ICharacteristicsRepository
{
    public CharacteristicsRepository(DbContext context) : base(context)
    {
    }
}