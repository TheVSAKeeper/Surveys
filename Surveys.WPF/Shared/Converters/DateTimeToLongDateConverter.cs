using System.Globalization;
using System.Windows.Data;
using Surveys.Domain.Exceptions;

namespace Surveys.WPF.Shared.Converters;

[ValueConversion(typeof(DateTime), typeof(string))]
public class DateTimeToLongDateConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        DateTime dateTime = ((DateTime)(value ?? throw new SurveysArgumentNullException(nameof(value)))).ToLocalTime();
        return $"{dateTime.ToLongDateString()} {dateTime.ToLongTimeString()}";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}