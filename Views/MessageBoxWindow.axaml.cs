using System.Threading.Tasks;
using Avalonia.Controls;

namespace TomatoPotato.Views;

public partial class MessageBoxWindow : Window
{
    public MessageBoxWindow()
    {
        InitializeComponent();
        BtnAno.Click += (s, e) => Close(true);
        BtnNe.Click += (s, e) => Close(false);
    }

    public static async Task<bool> Show(Window parent, string zprava)
    {
        var win = new MessageBoxWindow();
        win.TxtZprava.Text = zprava;
        return await win.ShowDialog<bool>(parent);
    }
}
