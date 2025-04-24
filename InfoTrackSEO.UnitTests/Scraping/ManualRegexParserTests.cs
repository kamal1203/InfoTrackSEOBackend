using System.Collections.Generic;
using FluentAssertions;
using InfoTrackSEO.Core.Scraping.Parsers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InfoTrackSEO.UnitTests.Scraping
{
    public class ManualRegexParserTests
    {
        private readonly Mock<ILogger<ManualRegexParser>> _loggerMock = new();
        private readonly ManualRegexParser _parser;

        public ManualRegexParserTests()
        {
            _parser = new ManualRegexParser(_loggerMock.Object);
        }

        [Fact]
        public void FindUrlRanks_ReturnsCorrectRanks_ForTargetUrl()
        {
            string html = @"<div class=""yuRUbf""><a href=""https://target.com""><h3>Result</h3></a></div>";
            var ranks = _parser.FindUrlRanks(html, "target.com");
            ranks.Should().ContainSingle().Which.Should().Be(1);
        }

        [Fact]
        public void FindUrlRanks_ReturnsEmptyList_WhenUrlNotFound()
        {
            string html = @"<div class=""yuRUbf""><a href=""https://other.com""><h3>Result</h3></a></div>";
            var ranks = _parser.FindUrlRanks(html, "target.com");
            ranks.Should().BeEmpty();
        }

        [Fact]
        public void FindUrlRanks_ReturnsMultipleRanks_WhenUrlAppearsMultipleTimes()
        {
            string html = @"<div class=""yuRUbf""><a href=""https://target.com""><h3>Result</h3></a></div>"
                + @"<div class=""yuRUbf""><a href=""https://target.com/page""><h3>Result2</h3></a></div>";
            var ranks = _parser.FindUrlRanks(html, "target.com");
            ranks.Should().BeEquivalentTo(new List<int> { 1, 2 });
        }
    }
}
