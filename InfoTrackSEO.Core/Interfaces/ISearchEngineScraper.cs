namespace InfoTrackSEO.Core.Interfaces;

/// <summary>
/// Defines the contract for a service that scrapes a specific search engine
/// to find the rank positions of a target URL for given keywords.
/// </summary>
public interface ISearchEngineScraper
{
    Task<string> ScrapeRankPositionsAsync(string keywords, string targetUrl);
}