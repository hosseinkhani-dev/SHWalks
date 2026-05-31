using Microsoft.AspNetCore.Http;

namespace SHWalks.Application.ImageStorageServices.DTOs
{
    public class ImageUploadDto
    {
        public required IFormFile File { get; set; }
    }
}
