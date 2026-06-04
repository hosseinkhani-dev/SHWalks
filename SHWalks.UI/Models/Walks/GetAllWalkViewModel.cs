namespace SHWalks.UI.Models.Walks
{
    public class GetAllWalkViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Length { get; set; }
        public string? ImageUrl { get; set; }
        public string Difficulty { get; set; }
        public Guid AreaId { get; set; }
    }
}
