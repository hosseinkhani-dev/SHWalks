using SHWalks.Application.AuthServices.TokenServices.DTOs;

namespace SHWalks.Application.AuthServices.TokenServices
{
    public interface ITokenService
    {
        Task<string?> CreateTokenAsync(LoginDto dto);
    }
}
