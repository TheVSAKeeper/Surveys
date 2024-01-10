using System.Globalization;
using System.Windows.Data;

namespace Surveys.WPF.Shared.Converters;

public class DateTimeToLongDateConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        switch (value)
        {
            case DateOnly date:
                return $"{date.ToLongDateString()}";

            case DateTime dateTime:
                dateTime = dateTime.ToLocalTime();
                return $"{dateTime.ToLongDateString()} {dateTime.ToLongTimeString()}";

            default:
                return value;
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}