using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using InfoTrackSEO.Core.Models;
using InfoTrackSEO.Data.Data;
using InfoTrackSEO.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InfoTrackSEO.UnitTests.Repositories
{
    public class SearchResultRepositoryTests
    {
        private SeoDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<SeoDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new SeoDbContext(options);
        }

        [Fact]
        public async Task AddAsync_AddsSearchResult()
        {
            var context = CreateContext(nameof(AddAsync_AddsSearchResult));
            var repo = new SearchResultRepository(context);
            var result = new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = DateTime.UtcNow };

            await repo.AddAsync(result);
            context.SearchResults.Should().ContainEquivalentOf(result);
        }

        [Fact]
        public async Task GetHistoryAsync_ReturnsAll_WhenNoFilters()
        {
            var context = CreateContext(nameof(GetHistoryAsync_ReturnsAll_WhenNoFilters));
            var repo = new SearchResultRepository(context);
            var now = DateTime.Now;
            var results = new[]
            {
                new SearchResult { Id = Guid.NewGuid(), Keywords = "a", Url = "u1", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = now.AddDays(-1) },
                new SearchResult { Id = Guid.NewGuid(), Keywords = "b", Url = "u2", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "2", Timestamp = now }
            };
            context.SearchResults.AddRange(results);
            context.SaveChanges();

            var history = await repo.GetHistoryAsync();
            history.Should().BeEquivalentTo(results.OrderByDescending(r => r.Timestamp));
        }

        [Fact]
        public async Task GetHistoryAsync_FiltersByKeywords()
        {
            var context = CreateContext(nameof(GetHistoryAsync_FiltersByKeywords));
            var repo = new SearchResultRepository(context);
            context.SearchResults.Add(new SearchResult { Id = Guid.NewGuid(), Keywords = "foo", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = DateTime.Now });
            context.SearchResults.Add(new SearchResult { Id = Guid.NewGuid(), Keywords = "bar", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "2", Timestamp = DateTime.Now });
            context.SaveChanges();

            var history = await repo.GetHistoryAsync("foo");
            history.Should().OnlyContain(r => r.Keywords == "foo");
        }

        [Fact]
        public async Task GetHistoryAsync_FiltersByUrl_CaseInsensitivePartial()
        {
            var context = CreateContext(nameof(GetHistoryAsync_FiltersByUrl_CaseInsensitivePartial));
            var repo = new SearchResultRepository(context);
            context.SearchResults.Add(new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "Example.com/page", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = DateTime.Now });
            context.SearchResults.Add(new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "Other.com", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "2", Timestamp = DateTime.Now });
            context.SaveChanges();

            var history = await repo.GetHistoryAsync(null, "example.com");
            history.Should().OnlyContain(r => r.Url.ToLower().Contains("example.com"));
        }

        [Fact]
        public async Task GetHistoryAsync_FiltersByDateRange()
        {
            var context = CreateContext(nameof(GetHistoryAsync_FiltersByDateRange));
            var repo = new SearchResultRepository(context);
            var now = DateTime.Now;
            var r1 = new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = now.AddDays(-10) };
            var r2 = new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "2", Timestamp = now };
            context.SearchResults.AddRange(r1, r2);
            context.SaveChanges();

            var history = await repo.GetHistoryAsync(null, null, now.AddDays(-2), now.AddDays(1));
            history.Should().OnlyContain(r => r.Timestamp >= now.AddDays(-2) && r.Timestamp <= now.AddDays(1));
        }

        [Fact]
        public async Task GetHistoryAsync_FiltersByScrapingStrategy()
        {
            var context = CreateContext(nameof(GetHistoryAsync_FiltersByScrapingStrategy));
            var repo = new SearchResultRepository(context);
            context.SearchResults.Add(new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = DateTime.Now });
            context.SearchResults.Add(new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "HttpManualParse", RankPositions = "2", Timestamp = DateTime.Now });
            context.SaveChanges();

            var history = await repo.GetHistoryAsync(null, null, null, null, "PowerShell");
            history.Should().OnlyContain(r => r.ScrapingStrategy == "PowerShell");
        }

        [Fact]
        public async Task GetHistoryAsync_OrdersByTimestampDescending()
        {
            var context = CreateContext(nameof(GetHistoryAsync_OrdersByTimestampDescending));
            var repo = new SearchResultRepository(context);
            var now = DateTime.Now;
            var r1 = new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = now.AddMinutes(-10) };
            var r2 = new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "2", Timestamp = now };
            context.SearchResults.AddRange(r1, r2);
            context.SaveChanges();

            var history = await repo.GetHistoryAsync();
            history.First().Should().BeEquivalentTo(r2);
            history.Last().Should().BeEquivalentTo(r1);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntityAndReturnsTrue()
        {
            var context = CreateContext(nameof(DeleteAsync_RemovesEntityAndReturnsTrue));
            var repo = new SearchResultRepository(context);
            var result = new SearchResult { Id = Guid.NewGuid(), Keywords = "k", Url = "u", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = DateTime.Now };
            context.SearchResults.Add(result);
            context.SaveChanges();

            var deleted = await repo.DeleteAsync(result.Id);
            deleted.Should().BeTrue();
            context.SearchResults.Should().NotContain(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
        {
            var context = CreateContext(nameof(DeleteAsync_ReturnsFalse_WhenNotFound));
            var repo = new SearchResultRepository(context);
            var deleted = await repo.DeleteAsync(Guid.NewGuid());
            deleted.Should().BeFalse();
        }
    }
}
