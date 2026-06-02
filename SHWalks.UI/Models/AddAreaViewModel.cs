using System.ComponentModel.DataAnnotations;

namespace SHWalks.UI.Models
{
    public class AddAreaViewModel
    {
        [Required(ErrorMessage = "Area name is required")]
        public string Name { get; set; }
        [Display(Name = "Upload Area Image")]
        public IFormFile? File { get; set; }
    }
}
