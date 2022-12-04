#if net7_0
#else
using BottomSheet;
#endif

using BottomSheet;

namespace MauiCleanTodos.App.Controls;

public interface IBottomSheet
{
    void ShowBottomSheet(object view, bool animated = true);
}

public class BottomSheetControl : IBottomSheet
{
    public void ShowBottomSheet(object view, bool animated = true)
    {
        if (view is not View)
            return;

#if net7_0
// TODO: this should be inside this condition, however a bug
        // requires this to be here at the moment.
        App.Current.MainPage.ShowBottomSheet(view as View, animated);
#else
        App.Current.MainPage.ShowBottomSheet((IView)view, animated);
#endif
    }
}

