using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SHWalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAreaSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lenght",
                table: "Walks",
                newName: "Length");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("4ef52928-b2fb-441c-a56d-f059215ebcc6"),
                column: "ImageUrl",
                value: "/images/Sadra.jpeg");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("8888ed03-4317-4467-be4e-c386b8384312"),
                column: "ImageUrl",
                value: "/images/Golestan.jpg");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("95e336d7-9003-41da-aaeb-0a23fdff0fb8"),
                column: "ImageUrl",
                value: "/images/MaaliAbad.jpg");

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("112038ee-7894-4efe-bcbc-68eecb6e09f6"), "/images/Afif Abad.jpg", "Afif Abad" },
                    { new Guid("15eeface-f864-4caa-969d-9e9979e0447b"), "/images/Zand Vakil.jpeg", "Afif Abad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("112038ee-7894-4efe-bcbc-68eecb6e09f6"));

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("15eeface-f864-4caa-969d-9e9979e0447b"));

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Walks",
                newName: "Lenght");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("4ef52928-b2fb-441c-a56d-f059215ebcc6"),
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSlkQoPCwn6UWhVoGekUJuih5kO9kYSTQHdA&s");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("8888ed03-4317-4467-be4e-c386b8384312"),
                column: "ImageUrl",
                value: "https://learn.zoner.com/wp-content/uploads/2025/04/zoner-ai-image-creator.jpg");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: new Guid("95e336d7-9003-41da-aaeb-0a23fdff0fb8"),
                column: "ImageUrl",
                value: "https://www.digikala.com/mag/wp-content/uploads/2025/02/AI-ART-main-min.webp");
        }
    }
}
