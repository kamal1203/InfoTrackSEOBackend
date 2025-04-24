using InfoTrackSEO.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackSEO.Data.Data
{
    public class SeoDbContext : DbContext
    {
        public SeoDbContext(DbContextOptions<SeoDbContext> options) : base(options) { }
        public DbSet<SearchResult> SearchResults { get; set; }
        // ScrapingStrategy property will be mapped automatically by EF Core conventions.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SearchResult>().HasData(
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Keywords = "seo tools", Url = "example.com", SearchEngine = "Google", RankPositions = "1,3,7", Timestamp = new DateTime(2025, 4, 23, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Keywords = "best laptops", Url = "laptopworld.com", SearchEngine = "Google", RankPositions = "2,5", Timestamp = new DateTime(2025, 4, 22, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), Keywords = "c# tutorials", Url = "dotnetdocs.com", SearchEngine = "Google", RankPositions = "1", Timestamp = new DateTime(2025, 4, 21, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), Keywords = "web scraping", Url = "scrapemaster.com", SearchEngine = "Google", RankPositions = "4,8", Timestamp = new DateTime(2025, 4, 20, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), Keywords = "ai news", Url = "ainews.com", SearchEngine = "Google", RankPositions = "1,2", Timestamp = new DateTime(2025, 4, 19, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111116"), Keywords = "football scores", Url = "sportszone.com", SearchEngine = "Google", RankPositions = "10", Timestamp = new DateTime(2025, 4, 18, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111117"), Keywords = "weather today", Url = "weather.com", SearchEngine = "Google", RankPositions = "1", Timestamp = new DateTime(2025, 4, 17, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111118"), Keywords = "stock market", Url = "financehub.com", SearchEngine = "Google", RankPositions = "3,6", Timestamp = new DateTime(2025, 4, 16, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111119"), Keywords = "dotnet core", Url = "microsoft.com", SearchEngine = "Google", RankPositions = "1,2,5", Timestamp = new DateTime(2025, 4, 15, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111120"), Keywords = "travel deals", Url = "travelnow.com", SearchEngine = "Google", RankPositions = "7,8", Timestamp = new DateTime(2025, 4, 14, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111121"), Keywords = "healthy recipes", Url = "foodie.com", SearchEngine = "Google", RankPositions = "2,4", Timestamp = new DateTime(2025, 4, 13, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111122"), Keywords = "music charts", Url = "musiczone.com", SearchEngine = "Google", RankPositions = "1,3", Timestamp = new DateTime(2025, 4, 12, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111123"), Keywords = "movie reviews", Url = "cinemaworld.com", SearchEngine = "Google", RankPositions = "5,9", Timestamp = new DateTime(2025, 4, 11, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111124"), Keywords = "coding bootcamp", Url = "codecamp.com", SearchEngine = "Google", RankPositions = "1,2", Timestamp = new DateTime(2025, 4, 10, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111125"), Keywords = "fitness tips", Url = "fitlife.com", SearchEngine = "Google", RankPositions = "3,4", Timestamp = new DateTime(2025, 4, 9, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111126"), Keywords = "gardening guide", Url = "gardeners.com", SearchEngine = "Google", RankPositions = "1,5", Timestamp = new DateTime(2025, 4, 8, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111127"), Keywords = "startup news", Url = "startupdaily.com", SearchEngine = "Google", RankPositions = "2,6", Timestamp = new DateTime(2025, 4, 7, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111128"), Keywords = "python libraries", Url = "pythontools.com", SearchEngine = "Google", RankPositions = "1,3,8", Timestamp = new DateTime(2025, 4, 6, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111129"), Keywords = "car reviews", Url = "carzone.com", SearchEngine = "Google", RankPositions = "4,7", Timestamp = new DateTime(2025, 4, 5, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" },
                new SearchResult { Id = Guid.Parse("11111111-1111-1111-1111-111111111130"), Keywords = "fashion trends", Url = "fashionista.com", SearchEngine = "Google", RankPositions = "2,5", Timestamp = new DateTime(2025, 4, 4, 12, 0, 0, DateTimeKind.Utc), ScrapingStrategy = "PowerShell" }
            );
        }
    }
}