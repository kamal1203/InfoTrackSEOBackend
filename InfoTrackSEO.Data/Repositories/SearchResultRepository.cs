using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Models;
using InfoTrackSEO.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackSEO.Data.Repositories
{
    public class SearchResultRepository : ISearchResultRepository
    {
        private readonly SeoDbContext _context;

        public SearchResultRepository(SeoDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(SearchResult searchResult)
        {
            await _context.SearchResults.AddAsync(searchResult);
            await _context.SaveChangesAsync();
        }        public async Task<IEnumerable<SearchResult>> GetHistoryAsync(string? keywords = null, string? url = null, DateTime? startDate = null, DateTime? endDate = null, string? scrapingStrategy = null)
        {
            // Set default date range to last 7 days if not specified
            var effectiveStartDate = startDate ?? DateTime.Now.AddDays(-7);
            var effectiveEndDate = endDate ?? DateTime.Now;
            
            var query = _context.SearchResults.AsQueryable();
            
            // Apply filters only if they are provided
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                query = query.Where(sr => sr.Keywords == keywords);
            }
              if (!string.IsNullOrWhiteSpace(url))
            {
                // Use case-insensitive comparison and check if URL contains the search term
                // This helps match URLs regardless of exact formatting, casing, or protocol differences
                query = query.Where(sr => sr.Url.ToLower().Contains(url.ToLower()) || url.ToLower().Contains(sr.Url.ToLower()));
            }
            
            // Date range filter is always applied (with default or specified dates)
            query = query.Where(sr => sr.Timestamp >= effectiveStartDate && sr.Timestamp <= effectiveEndDate);
            
            if (!string.IsNullOrWhiteSpace(scrapingStrategy))
            {
                query = query.Where(sr => sr.ScrapingStrategy == scrapingStrategy);
            }
            
            query = query.OrderByDescending(sr => sr.Timestamp);
            return await query.ToListAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.SearchResults.FindAsync(id);
            if (entity == null)
                return false;
            _context.SearchResults.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}