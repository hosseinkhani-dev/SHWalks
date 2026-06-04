using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SHWalks.UI.Models.Walks
{
    public class AddWalkViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be more than 100 character")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description must not exceed 500 character")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Distance is required")]
        [Range(0.1, 200, ErrorMessage = "Distance must be between 0.1 and 200 km")]
        public double Length { get; set; }
        public IFormFile? File { get; set; }
        [Required(ErrorMessage = "Please select a difficulty level")]
        public string SelectedDifficulty { get; set; }
        [Required(ErrorMessage = "Please select an area location")]
        public Guid SelectedAreaId { get; set; }

        // DropDowns
        public List<SelectListItem> Difficulties { get; set; } = new();
        public List<SelectListItem> Areas { get; set; } = new();
    }
}
