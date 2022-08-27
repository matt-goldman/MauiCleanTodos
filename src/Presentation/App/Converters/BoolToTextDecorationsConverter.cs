using System.Globalization;

namespace MauiCleanTodos.App.Converters;
public class BoolToTextDecorationsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var done = (bool)value;

        if (done)
            return TextDecorations.Strikethrough;

        return TextDecorations.None;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
