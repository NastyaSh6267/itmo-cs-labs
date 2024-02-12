namespace Slab3;

public static class Program
{
    public static void Main()
    {
        var catalog = new List<MusicTrack>();
        var appView = new AppView();
        var appRepository = new AppRepository();
        var appController = new AppController(catalog, appView, appRepository);
        appController.Run();
    }
}