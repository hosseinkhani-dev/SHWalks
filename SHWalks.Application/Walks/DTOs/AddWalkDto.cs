using SHWalks.Domain;
using System.ComponentModel.DataAnnotations;

namespace SHWalks.Application.Walks.DTOs
{
    public class AddWalkDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Description must not less than 2 character")]
        [MaxLength(500, ErrorMessage = "Description must not exceed 500 character")]
        public string Description { get; set; }
        public double Length { get; set; }
        public string? ImageUrl { get; set; }
        public Difficulty Difficulty { get; set; }
        public Guid AreaId { get; set; }
    }
}
