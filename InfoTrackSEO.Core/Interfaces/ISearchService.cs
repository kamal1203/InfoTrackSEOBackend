using InfoTrackSEO.Core.Models;

namespace InfoTrackSEO.Core.Interfaces;

/// <summary>
/// Defines the contract for the service responsible for orchestrating
/// search engine scraping and result storage.
/// </summary>
public interface ISearchService
{
    Task<SearchResult> PerformSearchAsync(string keywords, string url, string searchEngine, string scrapingStrategy);
}