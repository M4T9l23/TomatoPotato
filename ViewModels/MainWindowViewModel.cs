using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using TomatoPotato.Models;
using TomatoPotato.Services;

namespace TomatoPotato.ViewModels;

public class MainWindowViewModel
{
    private readonly FilmService _service = new();
    private List<Film> _vsechnyFilmy = new();
    public ObservableCollection<Film> Filmy { get; set; } = new();

    public MainWindowViewModel()
    {
        var nactene = _service.NactiFilmy();
        _vsechnyFilmy = nactene;
        foreach (var f in nactene)
            Filmy.Add(f);
    }

    public void Filtruj(string hledanyText, string zanr, double minHodnoceni)
    {
        var vysledky = _vsechnyFilmy.Where(f =>
            (string.IsNullOrWhiteSpace(hledanyText) || f.Nazev.Contains(hledanyText, System.StringComparison.OrdinalIgnoreCase)) &&
            (zanr == "Vše" || string.IsNullOrWhiteSpace(zanr) || f.Zanr == zanr) &&
            f.Hodnoceni >= minHodnoceni
        ).ToList();

        Filmy.Clear();
        foreach (var f in vysledky)
            Filmy.Add(f);
    }

    public void PridejFilm(Film film)
    {
        _vsechnyFilmy.Add(film);
        Filmy.Add(film);
        Uloz();
    }

    public void OdstranFilm(Film film)
    {
        _vsechnyFilmy.Remove(film);
        Filmy.Remove(film);
        Uloz();
    }

    public void Uloz()
    {
        _service.UlozFilmy(_vsechnyFilmy);
    }

    public void Export(string cesta)
    {
        _service.ExportDoTxt(_vsechnyFilmy, cesta);
    }
}
