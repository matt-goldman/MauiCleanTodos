using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Google.Android.Material.BottomSheet;

namespace MauiCleanTodos.App.Platforms.Android;
public class CustomBottomSheet : BottomSheetDialog
{
    public CustomBottomSheet(Context context) : base(context)
    {
    }

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

    }
}
