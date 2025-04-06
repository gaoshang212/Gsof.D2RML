using System.Collections.ObjectModel;
using System.Linq;
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

        public D2RUser? User { get; set; }

        public MainWindowViewModel()
        {
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
    }
}


