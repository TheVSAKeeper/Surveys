using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Surveys.WPF.Shared.Navigation.Bar;

/// <summary>
///     Interaction logic for NavigationBarView.xaml
/// </summary>
public partial class NavigationBarView : UserControl
{
    private readonly PaletteHelper _paletteHelper = new();

    public NavigationBarView()
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