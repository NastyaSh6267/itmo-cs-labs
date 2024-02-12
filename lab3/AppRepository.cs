using Newtonsoft.Json;

namespace Slab3;

public class AppRepository
{
    private readonly string _jsonFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "tracks.json");

    private readonly AppContext _context = new ();
    
    public AppRepository()
    {
        _context.Database.EnsureCreated();
    }
    public void DbSave(IEnumerable<MusicTrack> tracks)
    {
        _context.Tracks?.RemoveRange(_context.Tracks);
        _context.SaveChanges();
        _context.Tracks?.AddRange(tracks);
        _context.SaveChanges();
    }

    public List<MusicTrack> DbLoad()
    {
        return _context.Tracks!.ToList();
    }

    public void JsonSave(IEnumerable<MusicTrack> tracks)
    {
        var jsonData = JsonConvert.SerializeObject(tracks, Formatting.Indented);
        File.WriteAllText(_jsonFilePath, jsonData);
    }

    public List<MusicTrack> JsonLoad()
    {
        File.Create(_jsonFilePath).Close();
        var jsonData = File.ReadAllText(_jsonFilePath);
        var data = JsonConvert.DeserializeObject<List<MusicTrack>>(jsonData) ?? [];
        return data.Select(track => new MusicTrack { Author = track.Author, Name = track.Name }).ToList();
    }
}