using System.Globalization;
using System.Windows.Data;

namespace Surveys.WPF.Shared.Converters;

public class RussificationConverter : IValueConverter
{
    private readonly Dictionary<string, string> _matching = new()
    {
        { "Administrator", "Администратор" },
        { "Doctor", "Врач" },
        { "Nurse", "Медсестра" },
        { "Male", "Мужской" },
        { "Female", "Женский" }
    };

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (targetType != typeof(string))
            throw new NotSupportedException();

        return value == null ? string.Empty : _matching.FirstOrDefault(pair => pair.Key == value.ToString()).Value;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (targetType != typeof(string))
            throw new NotSupportedException();

        return value == null ? string.Empty : _matching.FirstOrDefault(pair => pair.Value == value.ToString()).Key;
    }
}