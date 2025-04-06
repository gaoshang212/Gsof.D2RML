using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Gsof.D2RML.Models;
using Gsof.D2RML.Utils;
using Gsof.Extensions;
using ReactiveUI;

namespace Gsof.D2RML.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelActivator Activator { get; }

        public ObservableCollection<D2RUser> Users { get; set; }

        public string? Title { get; set; } = "D2RML";

        public D2RUser? User { get; set; }

        public MainWindowViewModel()
        {
            Title = $"D2RML {GetVersion()}";

            Activator = new ViewModelActivator();
            Users = [.. Database.Instance.Get<D2RUser>()];
        }

        public void AddUser(D2RUser user)
        {
            Users.Add(user);
            Database.Instance.Insert(user);
        }

        public async void Launch()
        {
            var users = Users.Where(i => i.IsLaunch).ToList();
            users.Apply(i => Database.Instance.Update(i));

            await Runner.Runs(users, 5);
        }
        public void OnDeleteUser()
        {
            var user = User;
            if (user == null)
            {
                return;
            }

            Users.Remove(user);

            Database.Instance.Delete<D2RUser>(i => i.Id == user.Id);
        }

        private string GetVersion()
        {
            var fileName = Process.GetCurrentProcess().MainModule?.FileName;

            if (string.IsNullOrEmpty(fileName))
            {
                return "未知";
            }

            var fvi = FileVersionInfo.GetVersionInfo(fileName);
            var version = fvi.FileVersion;

            return string.IsNullOrEmpty(version) ? string.Empty : Version.Parse(version).ToString(3);
        }
    }
}


