namespace SHWalks.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Lenght { get; set; }
        public string? ImageUrl { get; set; }
        public Difficulty Difficulty { get; set; }
        public Guid AreaId { get; set; }

        //Navigation property
        public Area? Area { get; set; }
    }
}
