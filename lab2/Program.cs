namespace Slab2;

public static class Program
{
    public static void Main()
    {
        var catalog = new List<MusicTrack>();
        var appView = new AppView();
        var appController = new AppController(catalog, appView);
        appController.Run();
    }
}