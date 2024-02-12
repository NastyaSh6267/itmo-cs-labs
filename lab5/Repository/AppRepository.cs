using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Slab5.DB;
using Slab5.Models;

namespace Slab5.Repository;

public class AppRepository:IAppRepository
{
    private readonly AppContextDb _context;
    
    public AppRepository(AppContextDb context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    public void Save(ObservableCollection<MusicTrack> tracks)
    {
        _context.Tracks?.RemoveRange(_context.Tracks);
        _context.SaveChanges();

        _context.Tracks?.AddRange(tracks);
        _context.SaveChanges();
    }

    public List<MusicTrack> Load()
    {
        return _context.Tracks!.ToList();
    }
}
