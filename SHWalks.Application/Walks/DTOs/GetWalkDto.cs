using SHWalks.Application.Areas.DTOs;
using SHWalks.Domain;

namespace SHWalks.Application.Walks.DTOs
{
    public class GetWalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Length { get; set; }
        public string? ImageUrl { get; set; }
        public string Difficulty { get; set; }
        public GetAreaDto AreaDto { get; set; } = new ();
    }
}
