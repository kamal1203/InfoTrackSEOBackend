using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfoTrackSEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleSearchResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SearchResults",
                columns: new[] { "Id", "Keywords", "RankPositions", "ScrapingStrategy", "SearchEngine", "Timestamp", "Url" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "seo tools", "1,3,7", "PowerShell", "Google", new DateTime(2025, 4, 23, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4073), "example.com" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "best laptops", "2,5", "PowerShell", "Google", new DateTime(2025, 4, 22, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4420), "laptopworld.com" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), "c# tutorials", "1", "PowerShell", "Google", new DateTime(2025, 4, 21, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4424), "dotnetdocs.com" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), "web scraping", "4,8", "PowerShell", "Google", new DateTime(2025, 4, 20, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4426), "scrapemaster.com" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), "ai news", "1,2", "PowerShell", "Google", new DateTime(2025, 4, 19, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4427), "ainews.com" },
                    { new Guid("11111111-1111-1111-1111-111111111116"), "football scores", "10", "PowerShell", "Google", new DateTime(2025, 4, 18, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4429), "sportszone.com" },
                    { new Guid("11111111-1111-1111-1111-111111111117"), "weather today", "1", "PowerShell", "Google", new DateTime(2025, 4, 17, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4430), "weather.com" },
                    { new Guid("11111111-1111-1111-1111-111111111118"), "stock market", "3,6", "PowerShell", "Google", new DateTime(2025, 4, 16, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4432), "financehub.com" },
                    { new Guid("11111111-1111-1111-1111-111111111119"), "dotnet core", "1,2,5", "PowerShell", "Google", new DateTime(2025, 4, 15, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4433), "microsoft.com" },
                    { new Guid("11111111-1111-1111-1111-111111111120"), "travel deals", "7,8", "PowerShell", "Google", new DateTime(2025, 4, 14, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4434), "travelnow.com" },
                    { new Guid("11111111-1111-1111-1111-111111111121"), "healthy recipes", "2,4", "PowerShell", "Google", new DateTime(2025, 4, 13, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4436), "foodie.com" },
                    { new Guid("11111111-1111-1111-1111-111111111122"), "music charts", "1,3", "PowerShell", "Google", new DateTime(2025, 4, 12, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4437), "musiczone.com" },
                    { new Guid("11111111-1111-1111-1111-111111111123"), "movie reviews", "5,9", "PowerShell", "Google", new DateTime(2025, 4, 11, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4439), "cinemaworld.com" },
                    { new Guid("11111111-1111-1111-1111-111111111124"), "coding bootcamp", "1,2", "PowerShell", "Google", new DateTime(2025, 4, 10, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4442), "codecamp.com" },
                    { new Guid("11111111-1111-1111-1111-111111111125"), "fitness tips", "3,4", "PowerShell", "Google", new DateTime(2025, 4, 9, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4443), "fitlife.com" },
                    { new Guid("11111111-1111-1111-1111-111111111126"), "gardening guide", "1,5", "PowerShell", "Google", new DateTime(2025, 4, 8, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4445), "gardeners.com" },
                    { new Guid("11111111-1111-1111-1111-111111111127"), "startup news", "2,6", "PowerShell", "Google", new DateTime(2025, 4, 7, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4446), "startupdaily.com" },
                    { new Guid("11111111-1111-1111-1111-111111111128"), "python libraries", "1,3,8", "PowerShell", "Google", new DateTime(2025, 4, 6, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4448), "pythontools.com" },
                    { new Guid("11111111-1111-1111-1111-111111111129"), "car reviews", "4,7", "PowerShell", "Google", new DateTime(2025, 4, 5, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4449), "carzone.com" },
                    { new Guid("11111111-1111-1111-1111-111111111130"), "fashion trends", "2,5", "PowerShell", "Google", new DateTime(2025, 4, 4, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4451), "fashionista.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111120"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111121"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111122"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111123"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111124"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111125"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111126"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111127"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111128"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111129"));

            migrationBuilder.DeleteData(
                table: "SearchResults",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111130"));
        }
    }
}
