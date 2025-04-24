using System;
using System.ComponentModel.DataAnnotations;

namespace InfoTrackSEO.API.DTOs;

/// <summary>
/// Represents a search request for keyword ranking.
/// </summary>
public record SearchRequestDto(
    [Required] string Keywords,
    [Required, Url] string Url,
    string SearchEngine = "Google",
    string ScrapingStrategy = "PowerShell"
);

/// <summary>
/// Represents a request for search history.
/// </summary>
public record HistoryRequestDto(
    string? Keywords = null,
    [Url] string? Url = null,
    DateTime? StartDate = null,
    DateTime? EndDate = null,
    string? ScrapingStrategy = null,
    string? SearchEngine = null
);
