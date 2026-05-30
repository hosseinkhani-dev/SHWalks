using SHWalks.Application.Areas.DTOs;
using SHWalks.Domain;

namespace SHWalks.Application.Walks.DTOs
{
    public class GetWalkDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Lenght { get; set; }
        public string? ImageUrl { get; set; }
        public Difficulty Difficulty { get; set; }
        public GetAreaDto AreaDto { get; set; } = new ();
    }
}
