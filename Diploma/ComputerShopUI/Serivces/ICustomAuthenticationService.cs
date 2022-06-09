using DTO;
using DTO.Login;

namespace ComputerShopUI.Serivces
{
    public interface ICustomAuthenticationService
    {
        Task<TokenModel> GetToken(LoginEditModel loginModel);
    }
}
