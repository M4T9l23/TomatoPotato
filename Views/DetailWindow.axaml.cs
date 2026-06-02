using Avalonia.Controls;
using TomatoPotato.Models;

namespace TomatoPotato.Views;

public enum DetailResult { None, Smazat, Ulozit }

public partial class DetailWindow : Window
{
    public DetailResult Vysledek { get; private set; } = DetailResult.None;
    private readonly Film _film;

    public DetailWindow(Film film)
    {
        InitializeComponent();
        _film = film;

        TxtNazev.Text = film.Nazev;
        TxtZanr.Text = $"Žánr: {film.Zanr}";
        TxtRok.Text = $"Rok: {film.Rok}";
        TxtDelka.Text = $"Délka: {film.Delka} min";
        TxtHodnoceni.Text = film.Hodnoceni.ToString();
        TxtPopis.Text = film.Popis;
        ChkZhledno.IsChecked = film.Zhledno;

        BtnZavrit.Click += (s, e) => Close();

        BtnUlozit.Click += (s, e) =>
        {
            if (!double.TryParse(TxtHodnoceni.Text, out double hodnoceni) || hodnoceni < 1 || hodnoceni > 10)
            {
                TxtChyba.Text = "Hodnocení musí být číslo 1-10!";
                return;
            }

            _film.Zhledno = ChkZhledno.IsChecked ?? false;
            _film.Hodnoceni = hodnoceni;
            Vysledek = DetailResult.Ulozit;
            Close();
        };

        BtnSmazat.Click += async (s, e) =>
        {
            var confirm = await MessageBoxWindow.Show(this, $"Opravdu smazat '{film.Nazev}'?");
            if (confirm)
            {
                Vysledek = DetailResult.Smazat;
                Close();
            }
        };
    }
}
