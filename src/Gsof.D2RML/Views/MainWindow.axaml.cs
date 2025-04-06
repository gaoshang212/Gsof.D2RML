using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Gsof.D2RML.Models;
using Gsof.D2RML.ViewModels;
using ReactiveUI;

namespace Gsof.D2RML.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {

            });
        }


        private async void OnShowAddUserOnClick(object? sender, RoutedEventArgs e)
        {
            var view = new UserView()
            {
                ViewModel = new UserViewModel()
            };

            var user = await view.ShowDialog<D2RUser?>(this);

            if (user == null)
            {
                return;
            }

            this.ViewModel?.AddUser(user);
        }
    }
}