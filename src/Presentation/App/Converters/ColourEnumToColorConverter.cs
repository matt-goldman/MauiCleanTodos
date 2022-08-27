using System.Globalization;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.App.Converters;
public class ColourEnumToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var colour = (Colours)value;

        switch (colour)
        {
            case Colours.Orange:
                return Colors.Orange;
            case Colours.Purple:
                return Colors.Purple;
            case Colours.Blue:
                return Colors.Blue;
            case Colours.Yellow:
                return Colors.Yellow;
            case Colours.White:
            default:
                return Colors.White;
            case Colours.Red:
                return Colors.Red;
            case Colours.Green:
                return Colors.Green;
            case Colours.Grey:
                return Colors.Grey;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
