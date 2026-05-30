using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SHWalks.Domain;

namespace SHWalks.Infrastructure.Persistence
{
    public class SHWalksDbContext : IdentityDbContext<IdentityUser>
    {
        public SHWalksDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Area> Areas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AreasSeedData(modelBuilder);

            IdentityRolesSeedData(modelBuilder);
        }

        private static void IdentityRolesSeedData(ModelBuilder modelBuilder)
        {
            var readerId = "465a4f5c-552f-4ea8-874f-0847bf598745";
            var writerId = "ca74180f-96c4-43cc-8d09-1d2c7b5ae53f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerId,
                    Name = "Reader",
                    NormalizedName = "READER"
                },
                new IdentityRole
                {
                    Id = writerId,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
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
