namespace InfoTrackSEO.Core.Interfaces;

/// <summary>
/// Defines the contract for a factory that creates instances of ISearchEngineScraper.
/// </summary>
public interface ISearchEngineScraperFactory
{
      ISearchEngineScraper GetScraper(string engineName, string scrapingStrategy);
}