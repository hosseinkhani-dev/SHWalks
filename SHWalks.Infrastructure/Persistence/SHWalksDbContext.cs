using Microsoft.EntityFrameworkCore;
using SHWalks.Domain;

namespace SHWalks.Infrastructure.Persistence
{
    public class SHWalksDbContext : DbContext
    {
        public SHWalksDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Area> Areas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AreasSeedData(modelBuilder);
        }

        private static void AreasSeedData(ModelBuilder modelBuilder)
        {
            var areas = new List<Area>()
            {
                new Area
                {
                    Id = Guid.Parse("95e336d7-9003-41da-aaeb-0a23fdff0fb8"),
                    Name = "Maaliabad",
                    ImageUrl = "https://www.digikala.com/mag/wp-content/uploads/2025/02/AI-ART-main-min.webp",
                },
                new Area
                {
                    Id = Guid.Parse("4ef52928-b2fb-441c-a56d-f059215ebcc6"),
                    Name = "Sadra",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSlkQoPCwn6UWhVoGekUJuih5kO9kYSTQHdA&s",
                },
                new Area
                {
                    Id = Guid.Parse("8888ed03-4317-4467-be4e-c386b8384312"),
                    Name = "Golestan",
                    ImageUrl = "https://learn.zoner.com/wp-content/uploads/2025/04/zoner-ai-image-creator.jpg",
                },
            };

            modelBuilder.Entity<Area>().HasData(areas);
        }
    }
}
