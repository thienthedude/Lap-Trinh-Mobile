

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiApp2.ViewModel;

public partial class MainViewModel : ObservableObject
{
    IConnectivity connectivity;

    public MainViewModel(IConnectivity connectivity)
    {
        Items = new ObservableCollection<string>();
        Text = string.Empty;
        this.connectivity = connectivity;
    }

    [ObservableProperty]
    public partial ObservableCollection<string> Items { get; set; }

    [ObservableProperty]
    public partial string Text { get; set; }

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlertAsync("Uh Oh!", "No Internet", "OK");
            return;
        }

        Items.Add(Text);
        // Add our item
        Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
        if (Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
}

