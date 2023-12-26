using System.Globalization;
using System.Windows.Data;

namespace Surveys.WPF.Shared.Converters;

[ValueConversion(typeof(string), typeof(string))]
public class TranslateRoleConverter : IValueConverter
{
    private readonly Dictionary<string, string> _roles = new()
    {
        { "Administrator", "Администратор" },
        { "Doctor", "Врач" },
        { "Nurse", "Медсестра" }
    };

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null ? string.Empty : _roles.FirstOrDefault(pair => pair.Key == (string)value).Value;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null ? string.Empty : _roles.FirstOrDefault(pair => pair.Value == (string)value).Key;
    }
}