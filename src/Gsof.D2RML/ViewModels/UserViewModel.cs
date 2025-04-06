using System.Collections.Generic;
using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using Gsof.D2RML.Models;
using ReactiveUI;
using Splat;

namespace Gsof.D2RML.ViewModels;

public class UserViewModel : ViewModelBase, IActivatableViewModel
{
    public ViewModelActivator Activator { get; }

    public D2RUser User { get; set; } = new();

    public Dictionary<string, string> Addresses { get; set; } = new();

    public UserViewModel()
    {
        Activator = new ViewModelActivator();
        Addresses["KR"] = "kr.actual.battle.net";
        Addresses["US"] = "us.actual.battle.net";
        Addresses["EU"] = "eu.actual.battle.net";

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            /* 处理激活 */
            Disposable
                .Create(() => { /* 处理停用 */ })
                .DisposeWith(disposables);
        });
    }


    public void Submit()
    {
        
    }


}