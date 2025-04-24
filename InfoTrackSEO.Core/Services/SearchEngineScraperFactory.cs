using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Scraping;

namespace InfoTrackSEO.Core.Services;

public class SearchEngineScraperFactory : ISearchEngineScraperFactory
{
    private readonly IScrapingStrategyExecutorFactory _executorFactory;
    private readonly IHtmlParser _parser;

    public SearchEngineScraperFactory(IScrapingStrategyExecutorFactory executorFactory, IHtmlParser parser)
    {
        _executorFactory = executorFactory;
        _parser = parser;
    }

    public ISearchEngineScraper GetScraper(string engineName, string scrapingStrategy)
    {
        var executor = _executorFactory.GetExecutor(scrapingStrategy);
        
        if (engineName.Equals("Google", StringComparison.OrdinalIgnoreCase))
            return new GoogleScraper(executor, _parser);

        throw new NotSupportedException($"Search engine '{engineName}' is not supported.");
    }
}