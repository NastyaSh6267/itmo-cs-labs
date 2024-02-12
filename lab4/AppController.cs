using Microsoft.AspNetCore.Mvc;

namespace Slab4;

public class AppController
{
    private readonly IAppRepository _appRepository;

    public AppController(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    [HttpPost]
    [Route("/add")]
    public Task Add([FromBody] string q, string name, string author)
    {
        var musicTrack = new MusicTrack{Name = name, Author = author};
        if (musicTrack.Name=="" || musicTrack.Author=="")
            throw new Exception("Поля не должны быть пустыми.");
        
        return _appRepository.Add(musicTrack);
    }
    
    [HttpGet]
    [Route("/list")]
    public Task<List<MusicTrack>> List()
    {
        return _appRepository.List();
    }

    [HttpGet]
    [Route("/search")]
    public Task<List<MusicTrack>> Search(string keyword, int mode)
    {
        return _appRepository.Search(keyword, mode);
    }

    [HttpGet]
    [Route("/delete")]
    public Task Delete(string keyword)
    {
        return _appRepository.Delete(keyword);
    }
}