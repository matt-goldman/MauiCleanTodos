#if NET6_0
#else
using BottomSheet;
#endif

namespace MauiCleanTodos.App.Controls;

public interface IBottomSheet
{
    void ShowBottomSheet(View view, bool animated = true);
}

public class BottomSheetControl : IBottomSheet
{
    public void ShowBottomSheet(View view, bool animated = true)
    {
#if NET6_0
#else
        App.Current.MainPage.ShowBottomSheet(view, animated);
#endif
    }
}

