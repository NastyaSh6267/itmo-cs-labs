using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace Slab5.Models;

public class MusicTrack:ReactiveObject
{
    [Key]
    public int Id { get; set; }
    
    
    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    private string _author;
    public string Author
    {
        get => _author;
        set => this.RaiseAndSetIfChanged(ref _author, value);
    }
}
