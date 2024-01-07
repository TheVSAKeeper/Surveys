using System.Globalization;
using System.Windows.Data;

namespace Surveys.WPF.Shared.Converters;

public class DateToAgeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            DateOnly date => DateTime.Today.Year - date.Year,
            DateTime dateTime => DateTime.Today.Year - dateTime.Year,
            var _ => value
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}