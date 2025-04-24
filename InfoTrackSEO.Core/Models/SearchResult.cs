using System;

namespace InfoTrackSEO.Core.Models;

/// <summary>
/// Represents a single search result record capturing the ranking of a URL for specific keywords.
/// </summary>
public class SearchResult
{
    /// <summary>
    /// Unique identifier for the search result record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The keywords used for the search.
    /// </summary>
    public required string Keywords { get; set; }

    /// <summary>
    /// The target URL that was searched for.
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// The search engine used (e.g., "Google", "Bing").
    /// </summary>
    public required string SearchEngine { get; set; }

    /// <summary>
    /// A comma-separated string of the rank positions where the URL was found within the top 100 results.
    /// Contains "0" if the URL was not found in the top 100.
    /// Example: "1, 15, 88" or "0".
    /// </summary>
    public required string RankPositions { get; set; }

    /// <summary>
    /// The timestamp when the search was performed.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// The scraping strategy used (e.g., "PowerShell", "HttpManualParse", "SeleniumManualParse").
    /// </summary>
    public required string ScrapingStrategy { get; set; }
}