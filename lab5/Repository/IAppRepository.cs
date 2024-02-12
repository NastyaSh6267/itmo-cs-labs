using System.Collections.Generic;
using System.Collections.ObjectModel;
using Slab5.Models;

namespace Slab5.Repository;

public interface IAppRepository
{
    void Save(ObservableCollection<MusicTrack> tracks);
    List<MusicTrack> Load();
}