using System.ComponentModel.DataAnnotations;

namespace SHWalks.UI.Models
{
    public class AddAreaViewModel
    {
        [Required(ErrorMessage = "Area name is required")]
        public string Name { get; set; }
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
    }
}
