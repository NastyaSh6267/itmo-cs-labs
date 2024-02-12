namespace Slab3;

public class AppView
{
    public void TrackList(List<MusicTrack> tracks)
    {
        var count = tracks.Count;
        if (count > 0)
        {
            for (var i = 0; i < count; i++)
                Console.WriteLine(tracks[i].Author + " - " + tracks[i].Name);
        }
        else
            Console.WriteLine("Треки не найдены.");
    }
    
    public string KeywordRequest()
    {
        var input = Console.ReadLine() ?? "";
        if (input == "")
            throw new Exception("Поле не может быть пустым");
        return input;
    }
    
    public static int SearchMode()
    {
        Console.WriteLine(
            "Поиск по:\n1. Названию.\n2. Исполнителю.\n3. Названию и Исполнителю");
        Console.Write("> ");
        var isValid = int.TryParse(Console.ReadLine(), out var n);
        if (!isValid || n < 1 || n > 3)
            throw new Exception("Некорректное значение.");
        return n;
    }
    
    public int DataModeRequest()
    {
        Console.WriteLine(
            "Выберите режим сохранения и загрузки данных:\n1. Database.\n2. JSON.");
        Console.Write("> ");
        var isValid = int.TryParse(Console.ReadLine(), out var n);
        if (!isValid || n < 1 || n > 2)
            throw new Exception("Некорректное значение.");
        return n;
    }
}