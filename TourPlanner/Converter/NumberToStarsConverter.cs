using System.Globalization;
using System.Windows.Data;

namespace TourPlanner.Converter;

public class NumberToStarsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int number)
        {
            return new string('★', number) + new string('☆', 5 - number);
        }
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}