using System.Collections.ObjectModel;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MusicStoreViewModel : ViewModelBase
{
    public MusicStoreViewModel()
    {
        SearchResults.Add(new AlbumViewModel());
        SearchResults.Add(new AlbumViewModel());
        SearchResults.Add(new AlbumViewModel());
    }

    private string? _searchText;
    private bool _isBusy;

    public string? SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    private AlbumViewModel? _selectedAlbum;

    public ObservableCollection<AlbumViewModel> SearchResults { get; } = new();

    public AlbumViewModel? SelectedAlbum
    {
        get => _selectedAlbum;
        set => this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
    }
}
