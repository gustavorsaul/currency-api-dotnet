
using Microsoft.EntityFrameworkCore;
using currency_api.models;

namespace currency_api.data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<ConversionLog> Conversions { get; set; }
}
