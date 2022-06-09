namespace ComputerShopDB.Common.Cryptography
{
    public interface IHashAlgorithm
    {
        string CalculateHash(string text);
    }
}
