using System;
using System.Net.Http;
using System.Threading.Tasks;
using InfoTrackSEO.Core.Interfaces;

namespace InfoTrackSEO.Core.Scraping.Strategies;

public class HttpManualParseStrategy : IScrapingStrategyExecutor
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";

    public HttpManualParseStrategy(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<string> ExecuteAsync(string searchUrl)
    {
        var client = _httpClientFactory.CreateClient("ScrapingClient");
        client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.AcceptLanguage.Clear();

        try
        {
            HttpResponseMessage response = await client.GetAsync(searchUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"HTTP request failed for URL '{searchUrl}'. Status: {ex.StatusCode}. Message: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred while fetching URL '{searchUrl}'. Message: {ex.Message}", ex);
        }
    }
}