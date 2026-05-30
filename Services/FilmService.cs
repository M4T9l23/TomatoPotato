using System;
using System.Collections.Generic;
using System.IO;
using TomatoPotato.Models;

namespace TomatoPotato.Services;

public class FilmService
{
    private readonly string _dataPath;
    private readonly string _appDataPath;

    public FilmService()
    {
        _appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TomatoPotato"
        );
        Directory.CreateDirectory(_appDataPath);
        _dataPath = Path.Combine(_appDataPath, "filmy.txt");
    }

    public List<Film> NactiFilmy()
    {
        var filmy = new List<Film>();
        if (!File.Exists(_dataPath)) return filmy;

        foreach (var line in File.ReadAllLines(_dataPath))
        {
            if (!string.IsNullOrWhiteSpace(line))
                filmy.Add(Film.FromString(line));
        }
        return filmy;
    }

    public void UlozFilmy(List<Film> filmy)
    {
        var lines = new List<string>();
        foreach (var f in filmy)
            lines.Add(f.ToString());
        File.WriteAllLines(_dataPath, lines);
    }

    public void ExportDoTxt(List<Film> filmy, string cesta)
    {
        var lines = new List<string>();
        lines.Add("=== SEZNAM FILMŮ ===");
        lines.Add("");
        foreach (var f in filmy)
        {
            lines.Add($"Název:     {f.Nazev}");
            lines.Add($"Žánr:      {f.Zanr}");
            lines.Add($"Rok:       {f.Rok}");
            lines.Add($"Délka:     {f.Delka} min");
            lines.Add($"Hodnocení: {f.Hodnoceni}/10");
            lines.Add($"Zhlédnuto: {(f.Zhledno ? "Ano" : "Ne")}");
            lines.Add($"Popis:     {f.Popis}");
            lines.Add(new string('-', 40));
        }
        File.WriteAllLines(cesta, lines);
    }
}
