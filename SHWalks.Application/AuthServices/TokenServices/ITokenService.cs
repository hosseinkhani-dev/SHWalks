namespace SHWalks.Application.AuthServices.TokenServices
{
    public interface ITokenService
    {
        string CreateToken(string userName, string role);
    }
}
