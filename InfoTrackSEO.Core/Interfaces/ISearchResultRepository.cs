using InfoTrackSEO.Core.Models;

namespace InfoTrackSEO.Core.Interfaces;

public interface ISearchResultRepository
{
    Task AddAsync(SearchResult result);
    Task<IEnumerable<SearchResult>> GetHistoryAsync(string? keywords = null, string? url = null, DateTime? startDate = null, DateTime? endDate = null, string? scrapingStrategy = null);
    Task<bool> DeleteAsync(Guid id);
}