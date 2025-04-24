using InfoTrackSEO.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackSEO.Data.Data
{
    public class SeoDbContext : DbContext
    {
        public SeoDbContext(DbContextOptions<SeoDbContext> options) : base(options) { }
        public DbSet<SearchResult> SearchResults { get; set; }
        // ScrapingStrategy property will be mapped automatically by EF Core conventions.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}