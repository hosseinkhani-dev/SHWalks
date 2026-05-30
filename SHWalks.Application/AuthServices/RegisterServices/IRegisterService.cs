using SHWalks.Application.AuthServices.RegisterServices.DTOs;

namespace SHWalks.Application.AuthServices.RegisterServices
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
    }
}
