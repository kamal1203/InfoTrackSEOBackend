using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using InfoTrackSEO.API.Controllers;
using InfoTrackSEO.API.DTOs;
using InfoTrackSEO.API.Validation;
using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InfoTrackSEO.UnitTests.Controllers
{
    public class SearchControllerTests
    {
        private readonly Mock<ISearchService> _searchServiceMock = new();
        private readonly Mock<ISearchResultRepository> _resultRepositoryMock = new();
        private readonly Mock<ILogger<SearchController>> _loggerMock = new();
        private readonly SearchController _controller;

        public SearchControllerTests()
        {
            _controller = new SearchController(_searchServiceMock.Object, _resultRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task PerformSearch_ReturnsOk_WhenSearchIsSuccessful()
        {
            var request = new SearchRequestDto("keyword", "https://example.com");
            var result = new SearchResult { Id = Guid.NewGuid(), Keywords = "keyword", Url = "example.com", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1,2", Timestamp = DateTime.UtcNow };
            _searchServiceMock.Setup(s => s.PerformSearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result);

            var response = await _controller.PerformSearch(request);

            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be(result.RankPositions);
        }

        [Fact]
        public async Task PerformSearch_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Keywords", "Required");
            var request = new SearchRequestDto("", "https://example.com");

            var response = await _controller.PerformSearch(request);

            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PerformSearch_ReturnsBadRequest_WhenValidationFails()
        {
            var request = new SearchRequestDto("", "invalid-url");

            var response = await _controller.PerformSearch(request);

            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PerformSearch_StripsProtocolFromUrl()
        {
            var request = new SearchRequestDto("keyword", "https://example.com");
            var result = new SearchResult { Id = Guid.NewGuid(), Keywords = "keyword", Url = "example.com", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = DateTime.UtcNow };
            _searchServiceMock.Setup(s => s.PerformSearchAsync(It.IsAny<string>(), "example.com", It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result);

            var response = await _controller.PerformSearch(request);

            _searchServiceMock.Verify(s => s.PerformSearchAsync("keyword", "example.com", "Google", "PowerShell"), Times.Once);
        }

        [Fact]
        public async Task PerformSearch_ReturnsInternalServerError_OnException()
        {
            var request = new SearchRequestDto("keyword", "https://example.com");
            _searchServiceMock.Setup(s => s.PerformSearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("fail"));

            var response = await _controller.PerformSearch(request);

            var result = response as ObjectResult;
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task GetHistory_ReturnsOk_WhenSuccessful()
        {
            var request = new HistoryRequestDto("keyword", "https://example.com");
            var history = new List<SearchResult> { new SearchResult { Id = Guid.NewGuid(), Keywords = "keyword", Url = "example.com", SearchEngine = "Google", ScrapingStrategy = "PowerShell", RankPositions = "1", Timestamp = DateTime.UtcNow } };
            _resultRepositoryMock.Setup(r => r.GetHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>())).ReturnsAsync(history);

            var response = await _controller.GetHistory(request);

            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(history);
        }

        [Fact]
        public async Task GetHistory_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Url", "Invalid");
            var request = new HistoryRequestDto("keyword", "bad-url");

            var response = await _controller.GetHistory(request);

            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetHistory_ReturnsBadRequest_WhenValidationFails()
        {
            var request = new HistoryRequestDto("keyword", "bad-url");

            var response = await _controller.GetHistory(request);

            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetHistory_ReturnsInternalServerError_OnException()
        {
            var request = new HistoryRequestDto("keyword", "https://example.com");
            _resultRepositoryMock.Setup(r => r.GetHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>())).ThrowsAsync(new Exception("fail"));

            var response = await _controller.GetHistory(request);

            var result = response as ObjectResult;
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task DeleteSearch_ReturnsNoContent_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            _resultRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var response = await _controller.DeleteSearch(id);

            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteSearch_ReturnsNotFound_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _resultRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

            var response = await _controller.DeleteSearch(id);

            response.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteSearch_ReturnsInternalServerError_OnException()
        {
            var id = Guid.NewGuid();
            _resultRepositoryMock.Setup(r => r.DeleteAsync(id)).ThrowsAsync(new Exception("fail"));

            var response = await _controller.DeleteSearch(id);

            var result = response as ObjectResult;
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
