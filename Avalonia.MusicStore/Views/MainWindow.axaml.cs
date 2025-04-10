using System.Threading.Tasks;
using Avalonia.MusicStore.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Avalonia.MusicStore.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action =>
            action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    // This code is only valid in newer ReactiveUI which is shipped since Avalonia 11.2.0
    private async Task DoShowDialogAsync(IInteractionContext<MusicStoreViewModel, AlbumViewModel?> interaction)
    {
        var dialog = new MusicStoreWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<AlbumViewModel?>(this);
        interaction.SetOutput(result);
    }
}
