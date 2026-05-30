using SHWalks.Domain;

namespace SHWalks.Application.Walks.DTOs
{
    public class UpdateWalkDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Lenght { get; set; }
        public string? ImageUrl { get; set; }
        public Difficulty? Difficulty { get; set; }
        public Guid? AreaId { get; set; }
    }
}
