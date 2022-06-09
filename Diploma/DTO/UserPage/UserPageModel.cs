using ComputerShopDB.Entities;

namespace DTO.UserPage;

public class UserPageModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public Order Order { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Name { get; set; }
}