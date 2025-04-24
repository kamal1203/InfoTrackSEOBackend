using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using InfoTrackSEO.Core.Interfaces;

namespace InfoTrackSEO.Core.Scraping;

public class GoogleScraper : ISearchEngineScraper
{
    private readonly IScrapingStrategyExecutor _executor;
    private readonly IHtmlParser _parser;
    private const string GoogleBaseUrl = "https://www.google.co.uk/search";
    private const int ResultsPerPage = 100;

    public GoogleScraper(IScrapingStrategyExecutor executor, IHtmlParser parser)
    {
        _executor = executor;
        _parser = parser;
    }

    public async Task<string> ScrapeRankPositionsAsync(string keywords, string targetUrl)
    {
        var searchUrl = BuildGoogleSearchUrl(keywords);
        var htmlContent = await _executor.ExecuteAsync(searchUrl);
        var ranks = _parser.FindUrlRanks(htmlContent, targetUrl);
        if (ranks == null || !ranks.Any())
            return "0";
        return string.Join(",", ranks.OrderBy(r => r));
    }

    private string BuildGoogleSearchUrl(string keywords)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["num"] = ResultsPerPage.ToString();
        queryString["q"] = keywords;
        return $"{GoogleBaseUrl}?{queryString}";
    }
}