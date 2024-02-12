using Microsoft.EntityFrameworkCore;

namespace Slab4;

public sealed class AppContextDb:DbContext
{
    public DbSet<MusicTrack>? Tracks { get; init; }
    
    public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
    {
        Database.EnsureCreated();
    }
}