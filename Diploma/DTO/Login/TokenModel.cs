namespace DTO.Login
{
    public class TokenModel
    {
        public string Token { get; set; }
        public IEnumerable<ClaimModel> Claims { get; set; }
    }
}
