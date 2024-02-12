using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using Slab5.Models;
using Slab5.Views;

namespace Slab5.ViewModels;

public class DialogWindowViewModel:ViewModelBase
{
    private readonly MusicTrack _musicTrack;
    private string _name;
    private string _author;
    
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly DialogWindow _dialog;
    private readonly Button _button;

    public DialogWindowViewModel(MainWindowViewModel mwvm, DialogWindow dialog)
    {
        _mainWindowViewModel = mwvm;
        _dialog = dialog;
        _button = _dialog.FindControl<Button>("OkButton")!;
    }
    
    public DialogWindowViewModel(MainWindowViewModel mwvm, DialogWindow dialog, MusicTrack musicTrack)
    {
        _mainWindowViewModel = mwvm;
        _dialog = dialog;
        _musicTrack = musicTrack;
        Name = _musicTrack.Name;
        Author = _musicTrack.Author;
        _button = _dialog.FindControl<Button>("OkButton")!;
    }
    
    
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Author
    {
        get => _author;
        set => this.RaiseAndSetIfChanged(ref _author, value);
    }

    private bool IsInputWordValid()
    {
        return Author != "" && Name != "";
    }

    public async void AddTrack()
    {
        if (IsInputWordValid())
        {
            _button.Background = Brushes.Chartreuse;
            if (_musicTrack != null)
                await _mainWindowViewModel.SaveChanges(new MusicTrack{Name = Name, Author = Author});
            else
                await _mainWindowViewModel.AddTrack(new MusicTrack{Name = Name, Author = Author});
            _dialog.Close();
        }
        else
        {
            _button.Background = Brushes.Red;
        }
    }
}
