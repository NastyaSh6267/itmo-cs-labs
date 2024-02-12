using System.ComponentModel.DataAnnotations;

namespace Slab3;

public class MusicTrack
{
    [Key]
    public int Id { get; set; }
    public string Author { get; set; }
    public string Name { get; set; }
}