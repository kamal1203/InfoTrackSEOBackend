using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;
using InfoTrackSEO.Core.Scraping.Strategies;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InfoTrackSEO.UnitTests.Scraping
{
    public class PowerShellManualParseStrategyTests
    {

        [Fact]
        public void BuildPowerShellScript_ContainsUrlAndClipboardCopy()
        {
            var logger = new Mock<ILogger<PowerShellManualParseStrategy>>();
            var strategy = new PowerShellManualParseStrategy(logger.Object);
            var method = typeof(PowerShellManualParseStrategy).GetMethod("BuildPowerShellScript", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Should().NotBeNull();
            var script = (string?)method?.Invoke(strategy, new object[] { "http://example.com" });
            script.Should().NotBeNull();
            script!.Should().Contain("http://example.com");
            script.Should().Contain("Clipboard");
            script.Should().Contain("SendKeys");
        }
    }
}
