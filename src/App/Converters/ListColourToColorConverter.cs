using System.Globalization;

namespace MauiCleanTodos.App.Converters;
public class ListColourToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var colour = value as string;

        return Color.FromHex(colour);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
