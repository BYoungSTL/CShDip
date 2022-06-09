using System.Security.Claims;
using ComputerShopUI.Serivces;
using DTO;
using DTO.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopUI.Controllers
{
    public class LoginBaseController : Controller
    {
        private readonly ICustomAuthenticationService _authenticationService;
        
        public LoginBaseController(ICustomAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> BaseLogin(LoginEditModel loginModel, string role, string loginView, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var tokenModel = await _authenticationService.GetToken(loginModel);
                
                if(tokenModel != null)
                {
                    if (tokenModel.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == role))
                    {
                        await Authenticate(tokenModel.Claims.Select(x => new Claim(x.Type, x.Value)), loginModel.RememberMe);

                        HttpContext.Response.Cookies.Append("Token", tokenModel.Token, new CookieOptions()
                        {
                            Expires = DateTime.Now.AddDays(1)
                        });

                        return LocalRedirect(returnUrl); ;
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user does not have access to this platform");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and/or password");
                }
            }

            return View(loginView, loginModel);
        }

        public async Task<IActionResult> BaseLogout(string action, string controller)
        {
            HttpContext.Response.Cookies.Delete("Token");

            await HttpContext.SignOutAsync();

            return RedirectToAction(action, controller);
        }

        private async Task Authenticate(IEnumerable<Claim> claims, bool rememberMe)
        {
            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.Now.AddHours(1)
            });
        }
    }
}
