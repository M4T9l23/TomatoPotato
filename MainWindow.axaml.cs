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
        CmbZanrFilter.SelectedIndex = 0;

        BtnPridat.Click += async (s, e) =>
        {
            var win = new AddFilmWindow();
            var film = await win.ShowDialog<Film?>(this);
            if (film != null)
                _vm.PridejFilm(film);
        };

        ListFilmy.DoubleTapped += async (s, e) =>
        {
            if (ListFilmy.SelectedItem is Film film)
            {
                var win = new DetailWindow(film);
                await win.ShowDialog(this);

                if (win.Vysledek == DetailResult.Smazat)
                    _vm.OdstranFilm(film);
                else if (win.Vysledek == DetailResult.Ulozit)
                    _vm.Uloz();

                ListFilmy.SelectedItem = null;
            }
        };

        BtnHledat.Click += (s, e) => Hledej();
        TxtHledat.KeyUp += (s, e) => { if (e.Key == Avalonia.Input.Key.Enter) Hledej(); };

        BtnReset.Click += (s, e) =>
        {
            TxtHledat.Text = "";
            TxtMinHodnoceni.Text = "";
            CmbZanrFilter.SelectedIndex = 0;
            _vm.Filtruj("", "Vše", 0);
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

    private void Hledej()
    {
        var text = TxtHledat.Text ?? "";
        var zanr = (CmbZanrFilter.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Vše";
        double.TryParse(TxtMinHodnoceni.Text, out double minHodnoceni);
        _vm.Filtruj(text, zanr, minHodnoceni);
    }
}
