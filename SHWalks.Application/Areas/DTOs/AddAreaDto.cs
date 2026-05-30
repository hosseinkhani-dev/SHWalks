using System.ComponentModel.DataAnnotations;

namespace SHWalks.Application.Areas.DTOs
{
    public class AddAreaDto
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
