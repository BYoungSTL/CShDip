using ComputerShopDB.Entities;
using ComputerShopDB.Exceptions;
using ComputerShopDB.Infrastructure.Repositories.EntitiesReps;
using ComputerShopDB.RepositoriesInterfaces;
using ComputerShopDB.RepositoriesInterfaces.EntitiesRep;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ComputerShopDB.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContextOptions _options;
    private ComputerShopContext _context;
    
    private bool _isDisposed;
    private ComputerShopContext Context => _context ??= new ComputerShopContext(_options);

    private CharacteristicsRepository _characteristicsRepository;
    private OrderRepository _orderRepository;
    private UserRepository _userRepository;
    private ProductRepository _productRepository;
    private ProductsOrdersRepository _productsOrdersRepository;

    public ICharacteristicsRepository CharacteristicsRepository => _characteristicsRepository ??= new CharacteristicsRepository(Context);
    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(Context);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(Context);
    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(Context);

    public IProductsOrdersRepository ProductsOrdersRepository =>
        _productsOrdersRepository ??= new ProductsOrdersRepository(Context);

    public UnitOfWork(IOptions<UnitOfWorkOptions> accessor) : this(accessor.Value) { }

    public UnitOfWork(UnitOfWorkOptions options)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseSqlServer(options.ConnectionString, x => x.CommandTimeout(options.CommandTimeout));
        _options = optionsBuilder.Options;
    }

    public void Commit()
    {
        if (_context == null)
        {
            return;
        }

        if (_isDisposed)
        {
            throw new ObjectDisposedException("UnitOfWork");
        }

        try
        {
            Context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString()));
        }
        catch (DbUpdateException ex)
        {
            throw new UpdateException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Commit error.", ex);
        }
    }

    public void Dispose()
    {
        if (_context == null)
        {
            return;
        }

        if (!_isDisposed)
        {
            _context.Dispose();
        }

        _isDisposed = true;
    }
}