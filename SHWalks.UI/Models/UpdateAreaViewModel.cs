using System.ComponentModel.DataAnnotations;

namespace SHWalks.UI.Models
{
    public class UpdateAreaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Area name is required")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Image")]
        public IFormFile? File { get; set; }
        public string? ExistingImageUrl { get; set; }
    }
}
