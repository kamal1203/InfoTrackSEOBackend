using System;
using System.Threading.Tasks;
using InfoTrackSEO.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace InfoTrackSEO.Core.Scraping.Strategies;

public class SeleniumManualParseStrategy : IScrapingStrategyExecutor
{
    private readonly string _userAgent;
    private readonly string _chromeUserDataDir;

    public SeleniumManualParseStrategy(IConfiguration configuration)
    {
        _userAgent = configuration["Scraping:UserAgent"] ?? "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
        _chromeUserDataDir = configuration["Scraping:ChromeUserDataDir"] ?? @"C:\Users\Default\AppData\Local\Google\Chrome\User Data";
    }

    public Task<string> ExecuteAsync(string searchUrl)
    {
        return Task.Run(() =>
        {

            IWebDriver? driver = null;
            try
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--disable-gpu");
                chromeOptions.AddArgument("--log-level=3");
                chromeOptions.AddArgument("--no-sandbox");
                chromeOptions.AddArgument("--disable-dev-shm-usage");
                chromeOptions.AddArgument($"--user-agent={_userAgent}");
                chromeOptions.AddExcludedArgument("enable-automation");
                chromeOptions.AddAdditionalOption("useAutomationExtension", false);
                chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                chromeOptions.AddArgument($"--user-data-dir={_chromeUserDataDir}");
                driver = new ChromeDriver(chromeOptions);
                driver.Navigate().GoToUrl(searchUrl);
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.TagName("body")));
                return driver.PageSource;
            }
            catch (Exception ex)
            {
                throw new Exception($"Selenium WebDriver execution failed for URL '{searchUrl}'. Message: {ex.Message}", ex);
            }
            finally
            {
                driver?.Quit();
                driver?.Dispose();
            }
        });
    }
}