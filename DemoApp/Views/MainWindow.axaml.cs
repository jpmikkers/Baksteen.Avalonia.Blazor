using Avalonia.ReactiveUI;
using DemoApp.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using DemoApp.Data;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace DemoApp.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        var services = (Avalonia.Application.Current as App)?.Services;
        var rootComponents = new RootComponentsCollection()
        {
            new RootComponent("#app", typeof(DemoApp.Main), null)
        };

        Resources.Add("services", services);
        Resources.Add("rootComponents", rootComponents);

        InitializeComponent();
    }
}
