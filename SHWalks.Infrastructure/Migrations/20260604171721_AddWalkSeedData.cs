using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SHWalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWalkSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "AreaId", "Description", "Difficulty", "ImageUrl", "Length", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555555"), new Guid("95e336d7-9003-41da-aaeb-0a23fdff0fb8"), "A beautiful, easy walk through the heart of Maaliabad parks, perfect for families.", (byte)1, "/images/Siraz1.jpg", 3.5, "Green Way Trail" },
                    { new Guid("66666666-7777-8888-9999-000000000000"), new Guid("4ef52928-b2fb-441c-a56d-f059215ebcc6"), "A challenging trail highlighting the scenic rocky valleys and hills surrounding Sadra.", (byte)3, "/images/Shiraz2.jpg", 7.2000000000000002, "Sadra Valley Trek" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: new Guid("66666666-7777-8888-9999-000000000000"));
        }
    }
}
