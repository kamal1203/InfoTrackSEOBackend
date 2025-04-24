using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using InfoTrackSEO.API.DTOs;
using InfoTrackSEO.API.Validation;
using Xunit;

namespace InfoTrackSEO.UnitTests.Validation
{
    public class SearchValidationTests
    {
        [Fact]
        public void ValidateSearchRequest_ReturnsNoErrors_ForValidRequest()
        {
            var dto = new SearchRequestDto("keyword", "https://example.com", "Google", "PowerShell");
            var results = SearchValidation.ValidateSearchRequest(dto).ToList();
            results.Should().BeEmpty();
        }

        [Fact]
        public void ValidateSearchRequest_ReturnsError_WhenRequestIsNull()
        {
            var results = SearchValidation.ValidateSearchRequest(null).ToList();
            results.Should().ContainSingle(r => r.ErrorMessage == "Request cannot be null.");
        }

        [Fact]
        public void ValidateSearchRequest_ReturnsError_WhenKeywordsMissing()
        {
            var dto = new SearchRequestDto("", "https://example.com");
            var results = SearchValidation.ValidateSearchRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage == "Keywords are required.");
        }

        [Fact]
        public void ValidateSearchRequest_ReturnsError_WhenUrlMissing()
        {
            var dto = new SearchRequestDto("keyword", "");
            var results = SearchValidation.ValidateSearchRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage == "Url is required.");
        }

        [Fact]
        public void ValidateSearchRequest_ReturnsError_WhenUrlInvalid()
        {
            var dto = new SearchRequestDto("keyword", "not-a-url");
            var results = SearchValidation.ValidateSearchRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage == "Url must be a valid HTTP or HTTPS URL.");
        }

        [Fact]
        public void ValidateSearchRequest_ReturnsError_WhenSearchEngineNotGoogle()
        {
            var dto = new SearchRequestDto("keyword", "https://example.com", "Bing", "PowerShell");
            var results = SearchValidation.ValidateSearchRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage!.Contains("only 'Google' is supported"));
        }

        [Fact]
        public void ValidateSearchRequest_ReturnsError_WhenScrapingStrategyInvalid()
        {
            var dto = new SearchRequestDto("keyword", "https://example.com", "Google", "InvalidStrategy");
            var results = SearchValidation.ValidateSearchRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage!.Contains("Invalid scraping strategy"));
        }

        [Fact]
        public void ValidateHistoryRequest_ReturnsNoErrors_ForValidRequest()
        {
            var dto = new HistoryRequestDto("keyword", "https://example.com", DateTime.Now.AddDays(-1), DateTime.Now, "PowerShell", "Google");
            var results = SearchValidation.ValidateHistoryRequest(dto).ToList();
            results.Should().BeEmpty();
        }

        [Fact]
        public void ValidateHistoryRequest_ReturnsError_WhenRequestIsNull()
        {
            var results = SearchValidation.ValidateHistoryRequest(null).ToList();
            results.Should().ContainSingle(r => r.ErrorMessage == "Request cannot be null.");
        }

        [Fact]
        public void ValidateHistoryRequest_ReturnsError_WhenUrlInvalid()
        {
            var dto = new HistoryRequestDto("keyword", "not-a-url");
            var results = SearchValidation.ValidateHistoryRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage == "Url must be a valid HTTP or HTTPS URL.");
        }

        [Fact]
        public void ValidateHistoryRequest_ReturnsError_WhenEndDateBeforeStartDate()
        {
            var dto = new HistoryRequestDto("keyword", "https://example.com", DateTime.Now, DateTime.Now.AddDays(-1));
            var results = SearchValidation.ValidateHistoryRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage == "EndDate cannot be earlier than StartDate.");
        }

        [Fact]
        public void ValidateHistoryRequest_ReturnsError_WhenScrapingStrategyInvalid()
        {
            var dto = new HistoryRequestDto("keyword", "https://example.com", null, null, "InvalidStrategy");
            var results = SearchValidation.ValidateHistoryRequest(dto).ToList();
            results.Should().Contain(r => r.ErrorMessage!.Contains("Invalid scraping strategy"));
        }
    }
}
