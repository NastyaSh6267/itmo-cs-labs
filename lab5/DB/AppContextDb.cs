using Microsoft.EntityFrameworkCore;
using Slab5.Models;

namespace Slab5.DB;

public class AppContextDb:DbContext
{
    public DbSet<MusicTrack>? Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Tracks.db");
    }
}
