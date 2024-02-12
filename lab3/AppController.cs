using System.Text.RegularExpressions;

namespace Slab3;

public partial class AppController
{
    private List<MusicTrack> _tracks;
    private readonly AppView _appView;
    private readonly AppRepository _appRepository;


    public AppController(List<MusicTrack> tracks, AppView appView, AppRepository appRepository)
    {
        _tracks = tracks;
        _appView = appView;
        _appRepository = appRepository;
    }

    public void Add(MusicTrack track)
    {
        _tracks.Add(track);
    }

    public void Delete(string keyword)
    {
        foreach (var track in _tracks.Where(track => MyRegex().Replace((track.Author + track.Name).ToLower(), "")
                                                     == MyRegex1().Replace(keyword.ToLower(), "")))
        {
            _tracks.Remove(track);
            break;
        }
    }

    public List<MusicTrack> Search(string keyword, int mode)
    {
        var flag = false;
        var temp = new List<MusicTrack>();
        var regex = new Regex($@"{keyword.ToLower()}(\w*)");
        foreach (var track in _tracks)
        {
            switch (mode)
            {
                case 1:
                    if (regex.Matches(track.Name.ToLower()).Count > 0)
                    {
                        flag = true;
                        temp.Add(track);
                    }

                    break;
                case 2:
                    if (regex.Matches(track.Author.ToLower()).Count > 0)
                    {
                        flag = true;
                        temp.Add(track);
                    }

                    break;
                case 3:
                    if (regex.Matches(track.Name.ToLower()).Count > 0 ||
                        regex.Matches(track.Author.ToLower()).Count > 0)
                    {
                        flag = true;
                        temp.Add(track);
                    }

                    break;
            }
        }

        if (!flag)
            Console.WriteLine("Треки не найдены.");
        return temp;
    }


    public void Run()
    {
        Console.WriteLine(
            "Справка:\nВведите одну из команд:\n'list' чтобы увидеть все треки в каталоге.\n'search' чтобы найти треки в каталоге." +
            "\n'add' чтобы добавить новый трек.\n'del' чтобы удалить трек из каталога.\n'quit' чтобы выйти.");
        var dataMode = _appView.DataModeRequest();
        _tracks = dataMode switch
        {
            1 => _appRepository.DbLoad(),
            2 => _appRepository.JsonLoad(),
            _ => _tracks
        };

        var running = true;
        while (running)
        {
            Console.WriteLine("========\nВведите команду:");
            var input = Console.ReadLine() ?? "";

            switch (input.ToLower())
            {
                case "list":
                    Console.WriteLine("Все треки в каталоге:");
                    _appView.TrackList(_tracks);
                    break;
                case "search":
                    int mode = AppView.SearchMode();
                    Console.WriteLine("Введите часть названия трека, чтобы найти его в каталоге:");
                    _appView.TrackList(Search(_appView.KeywordRequest(), mode));
                    break;
                case "add":
                    var temp = new MusicTrack();
                    Console.WriteLine("Введите имя исполнителя:");
                    temp.Author = _appView.KeywordRequest();
                    Console.WriteLine("Введите название трека:");
                    temp.Name = _appView.KeywordRequest();
                    Add(temp);
                    break;
                case "del":
                    Console.WriteLine("Введите полностью название трека, чтобы удалить его:");
                    Delete(_appView.KeywordRequest());
                    break;
                case "quit":
                    running = !running;
                    break;
                default:
                    throw new Exception("Неизвестная команда.");
            }
        }
        switch (dataMode)
        {
            case 1:
                _appRepository.DbSave(_tracks);
                break;
            case 2:
                _appRepository.JsonSave(_tracks);
                break;
        }
    }

    [GeneratedRegex(@"[\s-]")]
    private static partial Regex MyRegex();

    [GeneratedRegex(@"[\s-]")]
    private static partial Regex MyRegex1();
}