using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Surveys.WPF.Shared.Navigation.Modal;

public class Modal : ContentControl
{
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Modal), new PropertyMetadata(false));

    static Modal()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Modal), new FrameworkPropertyMetadata(typeof(Modal)));
        BackgroundProperty.OverrideMetadata(typeof(Modal), new FrameworkPropertyMetadata(CreateDefaultBackground()));
    }

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    private static object CreateDefaultBackground()
    {
        SolidColorBrush defaultBackground = new(Colors.Black)
        {
            Opacity = 0.3
        };

        return defaultBackground;
    }
}