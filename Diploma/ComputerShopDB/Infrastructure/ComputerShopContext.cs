using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDB.Infrastructure;

public class ComputerShopContext : DbContext
{
    public ComputerShopContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}