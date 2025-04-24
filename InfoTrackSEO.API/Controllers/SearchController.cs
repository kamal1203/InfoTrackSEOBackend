using InfoTrackSEO.API.DTOs;
using InfoTrackSEO.API.Validation;
using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackSEO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ISearchResultRepository _resultRepository;
        private readonly ILogger<SearchController> _logger;

        public SearchController(
            ISearchService searchService,
            ISearchResultRepository resultRepository,
            ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _resultRepository = resultRepository;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PerformSearch([FromBody] SearchRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var errors = SearchValidation.ValidateSearchRequest(request).Select(v => v.ErrorMessage).ToList();
            if (errors.Any())
                return BadRequest(new { message = "Validation failed", errors });

            // Remove protocol (http/https) from URL for consistent storage and comparison
            var urlWithoutProtocol = StripProtocol(request.Url);

            try
            {
                var searchResult = await _searchService.PerformSearchAsync(
                    request.Keywords,
                    urlWithoutProtocol,
                    request.SearchEngine ?? "Google",
                    request.ScrapingStrategy ?? "PowerShell");
                _logger.LogInformation("Search completed successfully for Keywords: {Keywords}, URL: {Url}. Ranks: {Ranks}", request.Keywords, urlWithoutProtocol, searchResult.RankPositions);
                return Ok(searchResult.RankPositions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during search for {@Request}", request);
                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    message = ex.Message,
                    stackTrace = ex.StackTrace,
                    innerException = ex.InnerException?.ToString()
                });
            }
        }

        [HttpGet("history")]
        [ProducesResponseType(typeof(IEnumerable<SearchResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHistory([FromQuery] HistoryRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var errors = SearchValidation.ValidateHistoryRequest(request).Select(v => v.ErrorMessage).ToList();
            if (errors.Any())
                return BadRequest(new { message = "Validation failed", errors });

            try
            {
                var history = await _resultRepository.GetHistoryAsync(request.Keywords, request.Url, request.StartDate, request.EndDate, request.ScrapingStrategy);
                _logger.LogInformation("Retrieved {Count} history records for Keywords: {Keywords}, URL: {Url}, ScrapingStrategy: {ScrapingStrategy}", history.Count(), request.Keywords, request.Url, request.ScrapingStrategy);
                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during history retrieval for {@Request}", request);
                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    message = ex.Message,
                    stackTrace = ex.StackTrace,
                    innerException = ex.InnerException?.ToString()
                });
            }
        }

        /// <summary>
        /// Deletes a search result by its unique ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSearch(Guid id)
        {
            try
            {
                var deleted = await _resultRepository.DeleteAsync(id);
                if (!deleted)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting search result with ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // Helper to strip protocol from URL
        private static string StripProtocol(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                return url.Substring(7);
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return url.Substring(8);
            return url;
        }
    }
}