﻿using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;

namespace BottomSheet;

public static partial class PageExtensions
{
    public static void ShowBottomSheet(this Page page, IView bottomSheetContent, bool dimDismiss)
    {
        var bottomSheetDialog = new BottomSheetDialog(Platform.CurrentActivity?.Window?.DecorView.FindViewById(Android.Resource.Id.Content)?.RootView?.Context);
        bottomSheetDialog.SetContentView(bottomSheetContent.ToPlatform(page.Handler?.MauiContext ?? throw new Exception("MauiContext is null")));
        bottomSheetDialog.Window.SetDimAmount(0);
        bottomSheetDialog.Behavior.Hideable = dimDismiss;
        bottomSheetDialog.Behavior.FitToContents = true;
        bottomSheetDialog.Show();
    }
}
