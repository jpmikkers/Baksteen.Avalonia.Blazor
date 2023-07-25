using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DemoApp.ViewModels;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace DemoApp.Views;

public partial class MainWindow : Window
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

        // see: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/messenger
        WeakReferenceMessenger.Default.Register<MainWindow, DoExitMessage>(this, (r, m) =>
        {
            // Handle the message here, with r being the recipient and m being the
            // input message. Using the recipient passed as input makes it so that
            // the lambda expression doesn't capture "this", improving performance.
            r.Close();
        });
    }
}
