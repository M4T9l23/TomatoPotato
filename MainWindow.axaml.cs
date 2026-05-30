using Avalonia.Controls;
using Avalonia.Platform.Storage;
using TomatoPotato.ViewModels;
using TomatoPotato.Models;
using TomatoPotato.Views;

namespace AvaloniaLinuxTest;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _vm = new();

    public MainWindow()
    {
        InitializeComponent();
        ListFilmy.ItemsSource = _vm.Filmy;

        BtnPridat.Click += async (s, e) =>
        {
            var win = new AddFilmWindow();
            var film = await win.ShowDialog<Film?>(this);
            if (film != null)
                _vm.PridejFilm(film);
        };

        ListFilmy.DoubleTapped += (s, e) =>
        {
            if (ListFilmy.SelectedItem is Film film)
            {
                var win = new DetailWindow(film);
                win.ShowDialog(this);
            }
        };

        BtnExport.Click += async (s, e) =>
        {
            var options = new FilePickerSaveOptions
            {
                Title = "Exportovat seznam filmů",
                SuggestedFileName = "filmy.txt",
                FileTypeChoices = new[] { new FilePickerFileType("Text") { Patterns = new[] { "*.txt" } } }
            };
            var file = await StorageProvider.SaveFilePickerAsync(options);
            if (file != null)
                _vm.Export(file.Path.LocalPath);
        };
    }
}
