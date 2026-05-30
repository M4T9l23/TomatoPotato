using System.Collections.ObjectModel;
using System.Linq;
using TomatoPotato.Models;
using TomatoPotato.Services;

namespace TomatoPotato.ViewModels;

public class MainWindowViewModel
{
    private readonly FilmService _service = new();
    public ObservableCollection<Film> Filmy { get; set; } = new();

    public MainWindowViewModel()
    {
        var nactene = _service.NactiFilmy();
        foreach (var f in nactene)
            Filmy.Add(f);
    }

    public void PridejFilm(Film film)
    {
        Filmy.Add(film);
        Uloz();
    }

    public void OdstranFilm(Film film)
    {
        Filmy.Remove(film);
        Uloz();
    }

    public void Uloz()
    {
        _service.UlozFilmy(Filmy.ToList());
    }

    public void Export(string cesta)
    {
        _service.ExportDoTxt(Filmy.ToList(), cesta);
    }
}
