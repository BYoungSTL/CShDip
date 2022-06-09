using System.ComponentModel.DataAnnotations;
using ComputerShopDB.Entities;

namespace DTO.Login;

public class LoginEditModel
{
    [Display(Name = "Username")]
    [Required(ErrorMessage = "Username is required")]
    [StringLength(User.MaxNameLength, ErrorMessage = "Max length is 64 letters")]
    public string UserName { get; set; }
    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required")]
    [StringLength(User.MaxPasswordLength, ErrorMessage = "Max length is 64 letters")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "Remember Me")]
    [Required(ErrorMessage = "Remember me is required")]
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }
}