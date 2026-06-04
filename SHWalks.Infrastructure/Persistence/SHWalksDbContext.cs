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

            WalksSeedData(modelBuilder);
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
                    ImageUrl = "/images/MaaliAbad.jpg",
                },
                new Area
                {
                    Id = Guid.Parse("4ef52928-b2fb-441c-a56d-f059215ebcc6"),
                    Name = "Sadra",
                    ImageUrl = "/images/Sadra.jpeg",
                },
                new Area
                {
                    Id = Guid.Parse("8888ed03-4317-4467-be4e-c386b8384312"),
                    Name = "Golestan",
                    ImageUrl = "/images/Golestan.jpg",
                },
                new Area
                {
                    Id = Guid.Parse("112038ee-7894-4efe-bcbc-68eecb6e09f6"),
                    Name = "Afif Abad",
                    ImageUrl = "/images/Afif Abad.jpg",
                },
                 new Area
                {
                    Id = Guid.Parse("15eeface-f864-4caa-969d-9e9979e0447b"),
                    Name = "Afif Abad",
                    ImageUrl = "/images/Zand Vakil.jpeg",
                },
            };

            modelBuilder.Entity<Area>().HasData(areas);
        }

        private static void WalksSeedData(ModelBuilder modelBuilder)
        {
            var walks = new List<Walk>()
        {
        new Walk
        {
            Id = Guid.Parse("11111111-2222-3333-4444-555555555555"),
            Name = "Green Way Trail",
            Description = "A beautiful, easy walk through the heart of Maaliabad parks, perfect for families.",
            Length = 3.5,
            ImageUrl = "/images/Siraz1.jpg",
            Difficulty = Difficulty.Easy, 
            AreaId = Guid.Parse("95e336d7-9003-41da-aaeb-0a23fdff0fb8")
        },

        new Walk
        {
            Id = Guid.Parse("66666666-7777-8888-9999-000000000000"),
            Name = "Sadra Valley Trek",
            Description = "A challenging trail highlighting the scenic rocky valleys and hills surrounding Sadra.",
            Length = 7.2,
            ImageUrl = "/images/Shiraz2.jpg",
            Difficulty = Difficulty.Hard, 
            AreaId = Guid.Parse("4ef52928-b2fb-441c-a56d-f059215ebcc6")
        }
    };

            modelBuilder.Entity<Walk>().HasData(walks);
        }
    }
}
