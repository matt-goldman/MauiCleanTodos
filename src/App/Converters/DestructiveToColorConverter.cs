using System.Globalization;

namespace MauiCleanTodos.App.Converters;
public class DestructiveToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return Color.FromArgb("#FF0404");
        }

        return Color.FromArgb("#12FF0D");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
