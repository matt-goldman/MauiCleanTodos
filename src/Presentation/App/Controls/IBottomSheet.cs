using BottomSheet;

#if NET8_0
#else
using BottomSheet;
#endif


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
        
#if __ANDROID__ || __IOS__
        App.Current.MainPage.ShowBottomSheet(view as View, animated);
#endif
    }
}

