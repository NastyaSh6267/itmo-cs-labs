namespace Slab4;

public interface IAppRepository
{
    Task<List<MusicTrack>> Search(string keyword, int mode);
    Task Delete(string keyword);
    Task Add(MusicTrack musicTrack);
    Task<List<MusicTrack>> List();
}