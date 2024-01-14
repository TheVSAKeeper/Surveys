using System.Globalization;
using System.Windows.Data;
using Surveys.Domain;
using Surveys.Domain.Exceptions;

namespace Surveys.WPF.Shared.Converters;

[ValueConversion(typeof(Patient), typeof(string))]
public class PatientInfoConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Patient patient)
            return value;

        string gender = patient.Gender switch
        {
            Gender.Male => "М",
            Gender.Female => "Ж",
            Gender.Unspecified => string.Empty,
            var wrongGender => throw new SurveysInvalidOperationException(nameof(PatientInfoConverter), $"Not supported gender {wrongGender}")
        };

        return parameter is not null
            ? $"{patient.LastName} {patient.FirstName} {patient.Patronymic}"
            : $"{patient.LastName} {patient.FirstName} {patient.Patronymic} {gender} {patient.BirthDate.ToLongDateString()}";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}