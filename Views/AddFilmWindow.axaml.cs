using Avalonia.Controls;
using TomatoPotato.Models;

namespace TomatoPotato.Views;

public partial class AddFilmWindow : Window
{
    public AddFilmWindow()
    {
        InitializeComponent();

        BtnZrusit.Click += (s, e) => Close(null);

        BtnUlozit.Click += (s, e) =>
        {
            // Validace
            if (string.IsNullOrWhiteSpace(TxtNazev.Text))
            {
                TxtChyba.Text = "Název nesmí být prázdný!";
                return;
            }
            if (!int.TryParse(TxtRok.Text, out int rok) || rok < 1888 || rok > 2100)
            {
                TxtChyba.Text = "Neplatný rok!";
                return;
            }
            if (!int.TryParse(TxtDelka.Text, out int delka) || delka <= 0)
            {
                TxtChyba.Text = "Neplatná délka!";
                return;
            }
            if (!double.TryParse(TxtHodnoceni.Text, out double hodnoceni) || hodnoceni < 1 || hodnoceni > 10)
            {
                TxtChyba.Text = "Hodnocení musí být 1-10!";
                return;
            }

            var film = new Film
            {
                Nazev = TxtNazev.Text,
                Zanr = (CmbZanr.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                Rok = rok,
                Delka = delka,
                Hodnoceni = hodnoceni,
                Popis = TxtPopis.Text ?? "",
                Zhledno = ChkZhledno.IsChecked ?? false
            };

            Close(film);
        };
    }
}
