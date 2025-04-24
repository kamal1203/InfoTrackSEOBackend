namespace InfoTrackSEO.Core.Interfaces;

/// <summary>
/// Defines the contract for parsing HTML content to find the rank positions
/// of a target URL within search results.
/// </summary>
public interface IHtmlParser
{
    List<int> FindUrlRanks(string htmlContent, string targetUrl);
}