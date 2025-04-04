using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.MusicStore.Models;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        RxApp.MainThreadScheduler.Schedule(LoadAlbums);

        ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

        BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new MusicStoreViewModel();
            var result = await ShowDialog.Handle(store);
            if (result is not null)
            {
                Albums.Add(result);
                await result.SaveToDiskAsync();
            }
        });
    }

    public ICommand BuyMusicCommand { get; }
    public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }
    public ObservableCollection<AlbumViewModel> Albums { get; } = [];

    private async void LoadAlbums()
    {
        // Loads the list of albums from the disk cache, then transforms each data model (`Album`)
        // into a view model (`AlbumViewModel`).
        var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

        // Adds the AlbumViewModels to the ObservableCollection (updating the UI)
        foreach (var album in albums)
        {
            Albums.Add(album);
        }

        // Cover art is loaded asynchronously - ensuring the app remains responsive
        // during the image loading process.
        foreach (var album in Albums.ToList())
        {
            await album.LoadCover();
        }
    }
}
