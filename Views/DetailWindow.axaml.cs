using Avalonia.Controls;
using TomatoPotato.Models;

namespace TomatoPotato.Views;

public partial class DetailWindow : Window
{
    public DetailWindow(Film film)
    {
        InitializeComponent();

        TxtNazev.Text = film.Nazev;
        TxtZanr.Text = $"Žánr: {film.Zanr}";
        TxtRok.Text = $"Rok: {film.Rok}";
        TxtDelka.Text = $"Délka: {film.Delka} min";
        TxtHodnoceni.Text = $"⭐ {film.Hodnoceni}/10";
        TxtZhledno.Text = film.Zhledno ? "✅ Zhlédnuto" : "❌ Nezhlédnuto";
        TxtPopis.Text = film.Popis;

        BtnZavrit.Click += (s, e) => Close();
    }
}
