using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHWalks.Application.ImageStorageServices;
using SHWalks.Application.ImageStorageServices.DTOs;

namespace SHWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageStorageService _imageStorageService;

        public ImagesController(IImageStorageService imageStorageService)
        {
            _imageStorageService = imageStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto dto)
        {
            // Validate file extensions
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            var extension = Path.GetExtension(dto.File.FileName);

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file format. Please upload JPG, JPEG, or PNG.");
            }

            if(dto.File.Length > 6 * 1024 * 1024)
            {
                return BadRequest("File size exceeds the 6MB limit.");
            }

            // Upload image and get the URL string
            string completeImageUrl = await _imageStorageService.UploadAsync(dto.File);

            return Ok(new { imageUrl = completeImageUrl });
        }
    }
}
