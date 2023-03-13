using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DemoApp.ViewModels;
using DemoApp.Views;
using ReactiveUI;
using Avalonia.ReactiveUI;
namespace DemoApp;

public partial class App : Application
{
    public IServiceProvider MyServices { get; private set; }

    public App(IServiceProvider services) : base()
    {
        MyServices = services;  
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
		if(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = new MainWindow
			{
				DataContext = new MainWindowViewModel(),
			};
		}
		base.OnFrameworkInitializationCompleted();
	}
}
