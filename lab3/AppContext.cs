using Microsoft.EntityFrameworkCore;

namespace Slab3;

public class AppContext:DbContext
{
    public DbSet<MusicTrack>? Tracks { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Tracks.db");
    }
}
