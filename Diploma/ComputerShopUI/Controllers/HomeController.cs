using ComputerShopDB.Common.Paging;
using ComputerShopDB.Entities;
using ComputerShopDB.Infrastructure;
using ComputerShopUI.Serivces;
using DTO;
using DTO.Catalog;
using DTO.Home;
using DTO.ShoppingCart;
using DTO.UserPage;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopUI.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private static readonly UnitOfWork _unitOfWork = new(new UnitOfWorkOptions
    {
        CommandTimeout = Config.CommandTimeout,
        ConnectionString = Config.ConnectionString
    });

    [HttpGet("homepage")]
    public IActionResult HomePage(string? username)
    {
        return View(new HomePageViewModel
        {
            UserName = username
        });
    }

    public IActionResult Catalog(string username, string? category = null, int page = 1)
    {
        var pagedList = _unitOfWork.ProductRepository.GetPagedList(new PageInfo(page, 5), x => true);

        if (category != null)
        {
            pagedList = _unitOfWork.ProductRepository.GetPagedList(new PageInfo(page, 5), x => x.Category == category);
        }

        foreach (var prod in pagedList.Items)
        {
            if (prod.Characteristics == null)
            {
                var chars = _unitOfWork.CharacteristicsRepository.All();
                var selectedChars = chars.Where(x => x.ProductId == prod.Id);
                //prod.Characteristics.AddRange(selectedChars);   
            }
        }

        return View(new CatalogPageViewModel
        {
            Products = pagedList,
            PagedList = new PagedListModel
            {
                CanNext = pagedList.CanNext,
                CanPrevious = pagedList.CanPrevious,
                CurrentPage = pagedList.Page,
                TotalPages = pagedList.TotalPages
            },
            UserName = username
        });
    }

    [Route("SearchCatalog")]
    public IActionResult SearchCatalog(string username, string? search = null, int page = 1)
    {
        var pagedList = _unitOfWork.ProductRepository.GetPagedList(new PageInfo(page, 5), x => true);

        if (search != null)
        {
            pagedList = _unitOfWork.ProductRepository.GetPagedList(new PageInfo(page, 5),
                x => x.Category.Contains(search) || x.Name.Contains(search));
        }
        
        foreach (var prod in pagedList.Items)
        {
            if (prod.Characteristics == null)
            {
                var chars = _unitOfWork.CharacteristicsRepository.All();
                var selectedChars = chars.Where(x => x.ProductId == prod.Id);
                //prod.Characteristics.AddRange(selectedChars);   
            }
        }

        return View(new CatalogPageViewModel
        {
            Products = pagedList,
            PagedList = new PagedListModel
            {
                CanNext = pagedList.CanNext,
                CanPrevious = pagedList.CanPrevious,
                CurrentPage = pagedList.Page,
                TotalPages = pagedList.TotalPages
            },
            UserName = username
        });
    }


    [HttpGet("userpage")]
    public IActionResult UserPage(string username)
    {
        // if (User.Identity == null || !User.Identity.IsAuthenticated)
        // {
        //     return RedirectToAction("Login", "Account");
        // }
        var user = _unitOfWork.UserRepository.Find(x => x.UserName == username || x.Email == username);
        var order = _unitOfWork.OrderRepository.Find(x => x.UserId == user.Id);

        var productsOrders = _unitOfWork.ProductsOrdersRepository.All().Where(x => x.OrderId == order.Id);
        List<Product> products = new List<Product>();
        foreach (var prod in productsOrders)
        {
            products.Add(_unitOfWork.ProductRepository.Find(x => x.Id == prod.ProductId));
        }

        foreach (var prod in products)
        {
            if (prod.Characteristics == null)
            {
                var chars = _unitOfWork.CharacteristicsRepository.All();
                var selectedChars = chars.Where(x => x.ProductId == prod.Id);
                //prod.Characteristics.AddRange(selectedChars);   
            }
        }

        order.Products = products;

        return View(new UserPageModel
        {
            Email = user.Email,
            Order = order,
            UserName = username,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Name = user.UserName
        });
    }

    [HttpGet("ShoppingCart")]
    public IActionResult ShoppingCart(int id = 0)
    {
        // if (id <= 0)
        // {
        //     return RedirectToAction("HomePage");
        // }
        id = 1;
        var order = _unitOfWork.OrderRepository.Find(x => x.Id == id);
        //var productsOrders = _unitOfWork.ProductsOrdersRepository.Filter(x => x.OrderId == order.Id);
        var productsOrders = _unitOfWork.ProductsOrdersRepository.All().Where(x => x.OrderId == id);
        List<Product> products = new List<Product>();
        foreach (var prod in productsOrders)
        {
            products.Add(_unitOfWork.ProductRepository.Find(x => x.Id == prod.ProductId));
        }

        foreach (var prod in products)
        {
            var chars = _unitOfWork.CharacteristicsRepository.All();
            var selectedChars = chars.Where(x => x.ProductId == prod.Id);
            prod.Characteristics.AddRange(selectedChars);
        }

        return View(new ShoppingCartModel
        {
            Status = order.Status,
            Products = products,
            Price = order.Price,
            UserName = User.Identity.Name
        });
    }

    [Route("postcatalog")]
    public IActionResult PostCatalog(string productName, string username, string category)
    {
        if (username == null)
        {
            return RedirectToAction("Login", "Account");
        }
        var product = _unitOfWork.ProductRepository.Find(x => x.Name == productName);
        var user = _unitOfWork.UserRepository.Find(x => x.Email == username || x.UserName == username);
        var order = _unitOfWork.OrderRepository.Find(x => x.UserId == user.Id);
        var prodOrders = _unitOfWork.ProductsOrdersRepository.Filter(x => x.OrderId == order.Id);
        // foreach (var prod in prodOrders)
        // {
        //     order.Products.Add(_unitOfWork.ProductRepository.Find(x=>x.Id == prod.ProductId));
        // }
        if (order.Products == null)
        {
            order.Products = new List<Product>();
        }
        order.Products.Add(product);
        _unitOfWork.ProductsOrdersRepository.Add((new ProductsOrders()
        {
            Order = order,
            OrderId = order.Id,
            ProductId = product.Id,
            Product = product
        }));

        _unitOfWork.Commit();
        return RedirectToAction("Catalog", "Home", new{username = username, category = category});
    }
}