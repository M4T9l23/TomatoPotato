using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaLinuxTest;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnHelloClick(object? sender, RoutedEventArgs e)
    {
        StatusText.Text = "Hello there! 👋";
    }

    private void OnChangeTextClick(object? sender, RoutedEventArgs e)
    {
        StatusText.Text = "The text has been changed.";
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}