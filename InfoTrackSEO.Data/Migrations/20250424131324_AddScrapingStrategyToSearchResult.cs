using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoTrackSEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddScrapingStrategyToSearchResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScrapingStrategy",
                table: "SearchResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScrapingStrategy",
                table: "SearchResults");
        }
    }
}
