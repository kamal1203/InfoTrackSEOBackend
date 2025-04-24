using System;
using System.Threading.Tasks;
using FluentAssertions;
using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Models;
using InfoTrackSEO.Core.Services;
using Moq;
using Xunit;

namespace InfoTrackSEO.UnitTests.Services
{
    public class SearchServiceTests
    {
        private readonly Mock<ISearchEngineScraperFactory> _scraperFactoryMock = new();
        private readonly Mock<ISearchResultRepository> _resultRepositoryMock = new();
        private readonly Mock<ISearchEngineScraper> _scraperMock = new();
        private readonly SearchService _service;

        public SearchServiceTests()
        {
            _service = new SearchService(_scraperFactoryMock.Object, _resultRepositoryMock.Object);
        }

        [Fact]
        public async Task PerformSearchAsync_CallsFactoryAndScraperAndRepository()
        {
            var keywords = "test";
            var url = "example.com";
            var engine = "Google";
            var strategy = "PowerShell";
            var rankPositions = "1,2";
            _scraperFactoryMock.Setup(f => f.GetScraper(engine, strategy)).Returns(_scraperMock.Object);
            _scraperMock.Setup(s => s.ScrapeRankPositionsAsync(keywords, url)).ReturnsAsync(rankPositions);
            SearchResult? addedResult = null;
            _resultRepositoryMock.Setup(r => r.AddAsync(It.IsAny<SearchResult>())).Callback<SearchResult>(r => addedResult = r).Returns(Task.CompletedTask);

            var result = await _service.PerformSearchAsync(keywords, url, engine, strategy);

            _scraperFactoryMock.Verify(f => f.GetScraper(engine, strategy), Times.Once);
            _scraperMock.Verify(s => s.ScrapeRankPositionsAsync(keywords, url), Times.Once);
            _resultRepositoryMock.Verify(r => r.AddAsync(It.IsAny<SearchResult>()), Times.Once);
            result.Should().NotBeNull();
            result.RankPositions.Should().Be(rankPositions);
            result.Keywords.Should().Be(keywords);
            result.Url.Should().Be(url);
            result.SearchEngine.Should().Be(engine);
            result.ScrapingStrategy.Should().Be(strategy);
            result.Id.Should().NotBeEmpty();
            result.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            addedResult.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenDependenciesNull()
        {
            Action act1 = () => new SearchService(null!, _resultRepositoryMock.Object);
            Action act2 = () => new SearchService(_scraperFactoryMock.Object, null!);
            act1.Should().Throw<ArgumentNullException>();
            act2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task PerformSearchAsync_Throws_WhenFactoryThrows()
        {
            _scraperFactoryMock.Setup(f => f.GetScraper(It.IsAny<string>(), It.IsAny<string>())).Throws(new InvalidOperationException());
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.PerformSearchAsync("k", "u", "e", "s"));
        }

        [Fact]
        public async Task PerformSearchAsync_Throws_WhenScraperThrows()
        {
            _scraperFactoryMock.Setup(f => f.GetScraper(It.IsAny<string>(), It.IsAny<string>())).Returns(_scraperMock.Object);
            _scraperMock.Setup(s => s.ScrapeRankPositionsAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            await Assert.ThrowsAsync<Exception>(() => _service.PerformSearchAsync("k", "u", "e", "s"));
        }

        [Fact]
        public async Task PerformSearchAsync_Throws_WhenRepositoryThrows()
        {
            _scraperFactoryMock.Setup(f => f.GetScraper(It.IsAny<string>(), It.IsAny<string>())).Returns(_scraperMock.Object);
            _scraperMock.Setup(s => s.ScrapeRankPositionsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("1");
            _resultRepositoryMock.Setup(r => r.AddAsync(It.IsAny<SearchResult>())).ThrowsAsync(new Exception());
            await Assert.ThrowsAsync<Exception>(() => _service.PerformSearchAsync("k", "u", "e", "s"));
        }
    }
}
