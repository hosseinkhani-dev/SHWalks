using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SHWalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("4ef52928-b2fb-441c-a56d-f059215ebcc6"), "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSlkQoPCwn6UWhVoGekUJuih5kO9kYSTQHdA&s", "Sadra" },
                    { new Guid("8888ed03-4317-4467-be4e-c386b8384312"), "https://learn.zoner.com/wp-content/uploads/2025/04/zoner-ai-image-creator.jpg", "Golestan" },
                    { new Guid("95e336d7-9003-41da-aaeb-0a23fdff0fb8"), "https://www.digikala.com/mag/wp-content/uploads/2025/02/AI-ART-main-min.webp", "Maaliabad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("4ef52928-b2fb-441c-a56d-f059215ebcc6"));

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("8888ed03-4317-4467-be4e-c386b8384312"));

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("95e336d7-9003-41da-aaeb-0a23fdff0fb8"));
        }
    }
}
