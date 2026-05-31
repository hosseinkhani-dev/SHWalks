using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SHWalks.Application.ImageStorageServices
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageStorageService(
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            //Define folder path and if not exists create one
            string webRootPath = _webHostEnvironment.WebRootPath
                         ?? Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");

            string uploadFolder = Path.Combine(webRootPath, "images");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            //Generate a unique file name
            string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            //Save the file to the local disk
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            //Build the public URL string
            var request = _httpContextAccessor.HttpContext?.Request;

            string publicUrl = $"{request?.Scheme}://{request?.Host}/images/{uniqueFileName}";

            return publicUrl;
        }
    }
}
