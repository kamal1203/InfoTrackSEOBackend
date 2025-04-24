using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Scraping.Strategies;

namespace InfoTrackSEO.Core.Services
{
    public interface IScrapingStrategyExecutorFactory
    {
        IScrapingStrategyExecutor GetExecutor(string strategyName);
    }

    public class ScrapingStrategyExecutorFactory : IScrapingStrategyExecutorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _strategyMap;

        public ScrapingStrategyExecutorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _strategyMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
            {
                { "PowerShell", typeof(PowerShellManualParseStrategy) },
                { "Selenium", typeof(SeleniumManualParseStrategy) },
                { "Http", typeof(HttpManualParseStrategy) }
            };
        }

        public IScrapingStrategyExecutor GetExecutor(string strategyName)
        {
            if (!_strategyMap.TryGetValue(strategyName, out var type))
                throw new NotSupportedException($"Scraping strategy '{strategyName}' is not supported.");
            var executor = _serviceProvider.GetService(type) as IScrapingStrategyExecutor;
            if (executor == null)
                throw new InvalidOperationException($"Failed to resolve scraping strategy executor for '{strategyName}'.");
            return executor;
        }
    }
}
