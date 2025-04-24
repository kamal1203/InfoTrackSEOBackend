using InfoTrackSEO.Core.Interfaces;
using InfoTrackSEO.Core.Scraping.Parsers;
using InfoTrackSEO.Core.Scraping.Strategies;
using InfoTrackSEO.Core.Services;
using InfoTrackSEO.Data.Data;
using InfoTrackSEO.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SeoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISearchResultRepository, SearchResultRepository>();
builder.Services.AddScoped<ISearchEngineScraperFactory, SearchEngineScraperFactory>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IHtmlParser, ManualRegexParser>();
builder.Services.AddScoped<IScrapingStrategyExecutorFactory, ScrapingStrategyExecutorFactory>();
builder.Services.AddScoped<PowerShellManualParseStrategy>();
builder.Services.AddScoped<SeleniumManualParseStrategy>();
builder.Services.AddScoped<HttpManualParseStrategy>();
builder.Services.AddHttpClient("ScrapingClient");


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "InfoTrackSEO API", Version = "v1" });
});

// Add CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
             policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();         
        });
});


// --- Build the Application ---
var app = builder.Build();

// --- Configure the HTTP request pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InfoTrackSEO API v1"));
    app.UseDeveloperExceptionPage();
}

app.UseRouting(); 
app.UseCors("AllowReactApp"); 

app.UseAuthorization();

app.MapControllers();

app.Run();