using ComputerShopUI.Extensions;
using DTO;
using DTO.Login;
using Microsoft.AspNetCore.Authentication;

namespace ComputerShopUI.Serivces
{
    public class CustomAuthenticationService:ICustomAuthenticationService
    {
        private readonly HttpClient _client;

        public CustomAuthenticationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<TokenModel> GetToken(LoginEditModel loginModel)
        {
            var response = await _client.PostAsJson("api/account/token",loginModel);

            return response.IsSuccessStatusCode ? await response.ReadContentAs<TokenModel>() : null;
        }
    }
}
