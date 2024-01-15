using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Surveys.WPF.Shared.Converters;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool boolean = (bool)(value ?? throw new ArgumentNullException(nameof(value)));

        return boolean ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Visibility visibility = (Visibility)(value ?? throw new ArgumentNullException(nameof(value)));

        return visibility == Visibility.Visible;
    }
}