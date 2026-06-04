using SHWalks.UI.Models.Areas;
using System.ComponentModel.DataAnnotations;

namespace SHWalks.UI.Models.Walks
{
    public class WalkDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Lenght { get; set; }
        public string? ImageUrl { get; set; }
        public string Difficulty { get; set; }
        public AreaViewModel AreaDto { get; set; } = new();
    }
}
