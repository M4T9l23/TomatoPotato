namespace TomatoPotato.Models;

public class Film
{
    public string Nazev { get; set; } = "";
    public string Zanr { get; set; } = "";
    public string Popis { get; set; } = "";
    public int Delka { get; set; }
    public double Hodnoceni { get; set; }
    public bool Zhledno { get; set; } = false;
    public int Rok { get; set; }

    public override string ToString()
    {
        return $"{Nazev}|{Zanr}|{Popis}|{Delka}|{Hodnoceni}|{Zhledno}|{Rok}";
    }

    public static Film FromString(string line)
    {
        var parts = line.Split('|');
        return new Film
        {
            Nazev = parts[0],
            Zanr = parts[1],
            Popis = parts[2],
            Delka = int.Parse(parts[3]),
            Hodnoceni = double.Parse(parts[4]),
            Zhledno = bool.Parse(parts[5]),
            Rok = int.Parse(parts[6])
        };
    }
}
