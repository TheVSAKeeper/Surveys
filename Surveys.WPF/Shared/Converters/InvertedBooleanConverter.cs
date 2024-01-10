using System.Globalization;
using System.Windows.Data;

namespace Surveys.WPF.Shared.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
public class InvertedBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => !(bool)(value ?? throw new ArgumentNullException(nameof(value)));

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => !(bool)(value ?? throw new ArgumentNullException(nameof(value)));
}