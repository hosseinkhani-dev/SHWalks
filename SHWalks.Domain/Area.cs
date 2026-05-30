namespace SHWalks.Domain
{
    public class Area
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
