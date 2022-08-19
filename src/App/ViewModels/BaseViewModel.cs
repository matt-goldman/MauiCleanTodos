using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiCleanTodos.App.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    public INavigation Navigation { get; set; }

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string title;
}
