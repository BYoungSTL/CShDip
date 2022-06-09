using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ComputerShopDB.Common.Cryptography;
using ComputerShopDB.Entities;
using ComputerShopDB.Infrastructure;
using ComputerShopUI.Serivces;
using DTO;
using DTO.Home;
using DTO.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ComputerShopUI.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private static UnitOfWork _unitOfWork = new(new UnitOfWorkOptions
    {
        CommandTimeout = Config.CommandTimeout,
        ConnectionString = Config.ConnectionString
    });

    [HttpPost("/token")]
    public IActionResult Token(string username, string password)
    {
        var identity = GetIdentity(username, password);
        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid username or password." });
        }
 
        var now = DateTime.UtcNow;
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };
 
        return Json(response);
    }
 
    private ClaimsIdentity GetIdentity(string username, string password)
    {
        var person = _unitOfWork.UserRepository.Find(x => x.UserName == username);
        if (person != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, person.UserName)
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
 
        return null;
    }
    
    [Route("login")]
    public IActionResult Login()
    {
        return View(new LoginEditModel
        {
            ReturnUrl = "homepage"
        });
    }

    [HttpPost("account/login")]
    public IActionResult PostLogin(LoginEditModel loginEditModel)
    {
        return View("~/Views/Home/HomePage.cshtml", new HomePageViewModel()
        {
            UserName = loginEditModel.UserName
        });
    }
    
    [Route("Registration")]
    public IActionResult Registration()
    {
        return View(new RegistrationEditModel()
        {
            ReturnUrl = "homepage"
        });
    }

    public IActionResult PostRegistration(RegistrationEditModel registrationEditModel)
    {
        User newUser = new User
        {
            Address = registrationEditModel.Address,
            Email = registrationEditModel.Email,
            PasswordHash = registrationEditModel.Password,
            PhoneNumber = registrationEditModel.PhoneNumber,
            UserName = registrationEditModel.UserName,
            PasswordSalt = new Sha512Hash().CalculateHash(registrationEditModel.Password)
        };
        
        _unitOfWork.UserRepository.Add(newUser);
        _unitOfWork.Commit();
        return RedirectToAction("HomePage", "Home");
    }
}