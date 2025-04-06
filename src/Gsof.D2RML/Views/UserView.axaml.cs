using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Gsof.D2RML.ViewModels;

namespace Gsof.D2RML;

public partial class UserView : ReactiveWindow<UserViewModel>
{
    public UserView()
    {
        InitializeComponent();
    }


    private void OnSubmitClick(object? sender, RoutedEventArgs e)
    {
        this.Close(this.ViewModel?.User);
    }
}