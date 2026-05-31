using Microsoft.AspNetCore.Http;

namespace SHWalks.Application.ImageStorageServices
{
    public interface IImageStorageService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
