using Microsoft.EntityFrameworkCore;

namespace Slab4;

public class AppRepository(AppContextDb contextDb):IAppRepository
{
    public Task Add(MusicTrack musicTrack)
    {
        if (!contextDb.Tracks!.Any(t => t.Name == musicTrack.Name && t.Author == musicTrack.Author))
        {
            contextDb.Tracks?.Add(musicTrack);
            return contextDb.SaveChangesAsync();
        }
        throw new Exception("Трек уже существует.");
    }

    public Task<List<MusicTrack>> Search(string keyword, int mode)
    {
        return mode switch
        {
            1 => contextDb.Tracks!.Where(t => t.Name.ToLower() == keyword.ToLower()).ToListAsync(),
            2 => contextDb.Tracks!.Where(t => t.Author.ToLower() == keyword.ToLower()).ToListAsync(),
            3 => contextDb.Tracks!.Where(t => t.Name.ToLower() == keyword.ToLower() || t.Author.ToLower() ==
                keyword.ToLower()).ToListAsync(),
            _ => throw new Exception("Неизвестный режим, либо трек не найден.")
        };
    }
    
    public Task<List<MusicTrack>> List()
    {
        return contextDb.Tracks!.ToListAsync();
    }

    public Task Delete(string keyword)
    {
        return contextDb.Tracks!.Where(t => t.Author.ToLower() + t.Name.ToLower() == keyword.Replace(" ",
            "").ToLower()).ExecuteDeleteAsync();
    }
}
