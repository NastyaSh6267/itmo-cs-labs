using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using Slab5.DB;
using Slab5.Models;
using Slab5.Repository;
using Slab5.Views;

namespace Slab5.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IAppRepository _repository;

    public MainWindowViewModel()
    {
        var context = new AppContextDb();
        _repository = new AppRepository(context);
        var t = _repository.Load();
        TrackList.AddRange(t);
        TrackList = new ObservableCollection<MusicTrack>(_repository.Load());
        FilteredList = new ObservableCollection<MusicTrack>(TrackList);
    }

    private string _name;
    public string Name
    {
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _author;
    public string Author
    {
        set => this.RaiseAndSetIfChanged(ref _author, value);
    }

    private string _filterWord;
    public string FilterWord
    {
        get => _filterWord;
        set => this.RaiseAndSetIfChanged(ref _filterWord, value);
    }


    private int selectedIndex;

    public int SelectedIndex
    {
        get => selectedIndex;
        set => this.RaiseAndSetIfChanged(ref selectedIndex, value);
    }


    private MusicTrack _selectedItem;
    public MusicTrack SelectedItem
    {
        get => _selectedItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            if (value == null) return;
            Name = SelectedItem.Name;
            Author = SelectedItem.Author;
        }
    }

    private ObservableCollection<MusicTrack> TrackList { get;} = [];

    public ObservableCollection<MusicTrack> FilteredList { get; set; }

    public void ShowAddTrackDialog()
    {
        var dialog = new DialogWindow();
        dialog.DataContext = new DialogWindowViewModel(this, dialog);

        dialog.Show();
    }

    public void ShowEditTrackDialog()
    {
        if (SelectedItem == null) return;
        var dialog = new DialogWindow();
        dialog.DataContext = new DialogWindowViewModel(this, dialog, SelectedItem);

        dialog.Show();
    }

    private void UpdateFiltered(IEnumerable<MusicTrack> musicTracks)
    {
        FilteredList.Clear();
        FilteredList.AddRange(musicTracks);
    }

    public async Task AddTrack(MusicTrack musicTrack)
    {
        await Task.Delay(1000);

        TrackList.Add(musicTrack);
        UpdateFiltered(TrackList);
        _repository.Save(TrackList);
    }

    public async Task SaveChanges(MusicTrack musicTrack)
    {
        await Task.Delay(1000);

        foreach (var track in TrackList)
        {
            if (track != SelectedItem) continue;
            track.Name = musicTrack.Name;
            track.Author = musicTrack.Author;
        }

        _repository.Save(TrackList);
    }

    public void DeleteTrack()
    {
        if (SelectedItem == null) return;
        TrackList.Remove(SelectedItem);
        UpdateFiltered(TrackList);
    }

    public void Filter()
    {
        FilteredList.Clear();
        var regex = new Regex($@"{FilterWord}(\w*)");
        if (FilterWord == "")
            FilteredList.AddRange(TrackList);
        else
        {
            switch (SelectedIndex)
            {
                case 0:
                {
                    foreach (var track in TrackList)
                    {
                        if (regex.Matches(track.Name).Count > 0)
                            FilteredList.Add(track);
                    }

                    break;
                }
                case 1:
                {
                    foreach (var track in TrackList)
                    {
                        if (regex.Matches(track.Author).Count > 0)
                            FilteredList.Add(track);
                    }

                    break;
                }
                case 2:
                {
                    foreach (var track in TrackList)
                    {
                        if (regex.Matches(track.Author).Count > 0 || regex.Matches(track.Name).Count > 0)
                            FilteredList.Add(track);
                    }
                    break;
                }
            }
        }
    }
}