using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InfoTrackSEO.API.Controllers;
using InfoTrackSEO.API.DTOs;

namespace InfoTrackSEO.API.Validation
{
    public static class SearchValidation
    {
        public static IEnumerable<ValidationResult> ValidateSearchRequest(SearchRequestDto request)
        {
            if (request == null)
            {
                yield return new ValidationResult("Request cannot be null.");
                yield break;
            }
            if (string.IsNullOrWhiteSpace(request.Keywords))
                yield return new ValidationResult("Keywords are required.");
            if (string.IsNullOrWhiteSpace(request.Url))
                yield return new ValidationResult("Url is required.");
            else if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                yield return new ValidationResult("Url must be a valid HTTP or HTTPS URL.");
            if (!string.IsNullOrWhiteSpace(request.SearchEngine) && request.SearchEngine != "Google")
                yield return new ValidationResult("Currently only 'Google' is supported as a search engine.");
            if (!string.IsNullOrWhiteSpace(request.ScrapingStrategy) && request.ScrapingStrategy != "PowerShell" && request.ScrapingStrategy != "HttpManualParse" && request.ScrapingStrategy != "SeleniumManualParse")
                yield return new ValidationResult("Invalid scraping strategy. Allowed: PowerShell, HttpManualParse, SeleniumManualParse.");
        }        public static IEnumerable<ValidationResult> ValidateHistoryRequest(HistoryRequestDto request)
        {
            if (request == null)
            {
                yield return new ValidationResult("Request cannot be null.");
                yield break;
            }
              // Url validation only if provided
            if (!string.IsNullOrWhiteSpace(request.Url))
            {
                if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uriResult) || 
                    (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    yield return new ValidationResult("Url must be a valid HTTP or HTTPS URL.");
                }
            }
            
            // Date range validation only if both dates are provided
            if (request.StartDate.HasValue && request.EndDate.HasValue && request.EndDate < request.StartDate)
                yield return new ValidationResult("EndDate cannot be earlier than StartDate.");
            
            // Scraping strategy validation only if provided
            if (!string.IsNullOrWhiteSpace(request.ScrapingStrategy) && 
                request.ScrapingStrategy != "PowerShell" && 
                request.ScrapingStrategy != "HttpManualParse" && 
                request.ScrapingStrategy != "SeleniumManualParse")
                yield return new ValidationResult("Invalid scraping strategy. Allowed: PowerShell, HttpManualParse, SeleniumManualParse.");
        }
    }
}
