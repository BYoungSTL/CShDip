using System.ComponentModel.DataAnnotations;
using ComputerShopDB.Entities;

namespace DTO.Login;

public class RegistrationEditModel
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
    [Display(Name = "PhoneNumber")]
    [Required(ErrorMessage = "PhoneNumber is required")]
    [StringLength(User.MaxPhoneNumberLength, ErrorMessage = "Max length is 64 letters")]
    public string PhoneNumber { get; set; }
    [Display(Name = "Address")]
    [Required(ErrorMessage = "Address is required")]
    [StringLength(User.MaxAddressLength, ErrorMessage = "Max length is 64 letters")]
    public string Address { get; set; }
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required")]
    [StringLength(User.MaxEmailLength, ErrorMessage = "Max length is 64 letters")]
    public string Email { get; set; }

    public string ReturnUrl { get; set; }
}