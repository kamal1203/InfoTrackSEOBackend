namespace InfoTrackSEO.Core.Interfaces;

/// <summary>
/// Defines the contract for a strategy that executes the process
/// of retrieving web content (e.g., HTML) from a specified URL.
/// </summary>
public interface IScrapingStrategyExecutor
{
    Task<string> ExecuteAsync(string searchUrl);
}