using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Surveys.WPF.Shared.Converters;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool bValue = (bool)(value ?? throw new ArgumentNullException(nameof(value)));

        return bValue ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Visibility vValue = (Visibility)(value ?? throw new ArgumentNullException(nameof(value)));

        return vValue == Visibility.Visible;
    }
}