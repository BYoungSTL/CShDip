using ComputerShopDB.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ComputerShopDB;

public class SampleContextFactory : IDesignTimeDbContextFactory<ComputerShopContext>
{
    public ComputerShopContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ComputerShopContext>();

        optionsBuilder.UseSqlServer("Server=localhost;Database=ComputerShopDatabase;Trusted_Connection=True;");

        return new ComputerShopContext(optionsBuilder.Options);
    }
}