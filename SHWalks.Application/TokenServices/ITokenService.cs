namespace SHWalks.Application.TokenServices
{
    public interface ITokenService
    {
        string CreateToken(string userName, string role);
    }
}
