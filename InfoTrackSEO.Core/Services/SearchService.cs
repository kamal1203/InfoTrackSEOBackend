using System;
using System.Threading.Tasks;
using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Models;

namespace InfoTrackSEO.Core.Services;

public class SearchService : ISearchService
{
    private readonly ISearchEngineScraperFactory _scraperFactory;
    private readonly ISearchResultRepository _resultRepository;

    public SearchService(ISearchEngineScraperFactory scraperFactory, ISearchResultRepository resultRepository)
    {
        _scraperFactory = scraperFactory ?? throw new ArgumentNullException(nameof(scraperFactory));
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
    }

    public async Task<SearchResult> PerformSearchAsync(string keywords, string url, string searchEngine, string scrapingStrategy)
    {
        var scraper = _scraperFactory.GetScraper(searchEngine, scrapingStrategy);
        var rankPositions = await scraper.ScrapeRankPositionsAsync(keywords, url);
        var searchResult = new SearchResult
        {
            Id = Guid.NewGuid(),
            Keywords = keywords,
            Url = url,
            SearchEngine = searchEngine,
            ScrapingStrategy = scrapingStrategy, // Set the new property
            RankPositions = rankPositions,
            Timestamp = DateTime.UtcNow
        };
        await _resultRepository.AddAsync(searchResult);
        return searchResult;
    }
}