namespace SHWalks.Application.Areas.DTOs
{
    public class GetAllAreaWalksDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Lenght { get; set; }
        public string? ImageUrl { get; set; }
        public string Difficulty { get; set; }
    }
}
