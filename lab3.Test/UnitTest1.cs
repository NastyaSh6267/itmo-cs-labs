using NSubstitute;

namespace Slab3.Test;

public class UnitTest1
{
    [Fact]
    public void AddTrackTest()
    {
        var tracks = new List<MusicTrack>();
        var repository = Substitute.For<AppRepository>();
        repository.DbLoad().Returns([]);
        repository.JsonLoad().Returns([]);
        var appController = new AppController(tracks, new AppView(), repository);
        appController.Add(new MusicTrack { Author = "test", Name = "test" });
        Assert.NotEmpty(tracks);

    }
    
    [Theory]
    [InlineData("dora", "doradura", "kid cudi", "enter galactic")]
    public void SearchByNameTest(string author1, string name1, string author2, string name2)
    {
        var tracks = new List<MusicTrack>();
        var repository = Substitute.For<AppRepository>();
        repository.DbLoad().Returns([]);
        repository.JsonLoad().Returns([]);
        var appController = new AppController(tracks, new AppView(), repository);
        appController.Add(new MusicTrack { Author = author1, Name = name1 });
        appController.Add(new MusicTrack { Author = author2, Name = name2 });
        Assert.Equal(appController.Search(author1, 2)[0], tracks[0]);
    }
}
