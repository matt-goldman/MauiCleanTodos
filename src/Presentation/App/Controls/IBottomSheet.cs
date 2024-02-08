#if __ANDROID__ || __IOS__
using BottomSheet;
#elif NET8_0
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
#elif NET8_0 
#endif
    }
}

