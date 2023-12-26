using System.Windows;
using MaterialDesignThemes.Wpf;

namespace Surveys.WPF;

public partial class MainWindow : Window
{
    private readonly PaletteHelper _paletteHelper = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnThemeToggleClicked(object sender, RoutedEventArgs e)
    {
        ITheme theme = _paletteHelper.GetTheme();
        IBaseTheme baseTheme = theme.GetBaseTheme() == BaseTheme.Light ? Theme.Dark : Theme.Light;
        theme.SetBaseTheme(baseTheme);
        _paletteHelper.SetTheme(theme);
    }
}