using Avalonia.ReactiveUI;
using DemoApp.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
//using LibVLCSharp.Avalonia.Unofficial;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using WPFBlazorAppTemplate.Data;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace DemoApp.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
	public MainWindow()
    {
        var services = (Avalonia.Application.Current as App)?.MyServices;
        var rootComponents = new RootComponentsCollection() { new RootComponent("#app", typeof(DemoApp.Main), null) };

		Resources.Add("services", services);
        Resources.Add("rootComponents", rootComponents);
		
        InitializeComponent();
        //this.WhenActivated(d => d(ViewModel!.InteractionOpenFile.RegisterHandler(DoShowOpenFileDialogAsync)));
        //this.WhenActivated(d => d(ViewModel!.InteractionShowError.RegisterHandler(DoShowErrorAsync)));
		//this.Title = $"{Assembly.GetEntryAssembly()!.GetName().Version}";
    }

	private async Task DoShowErrorAsync(InteractionContext<string, Unit> ic)
    {
        var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error", ic.Input);
        await messageBoxStandardWindow.ShowDialog(this);
        ic.SetOutput(Unit.Default);
    }

    private async Task DoShowOpenFileDialogAsync(InteractionContext<Unit,System.Uri?> ic)
    {
        var files = await StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions { 
                AllowMultiple = false, 
                FileTypeFilter = new[] { FilePickerFileTypes.All },
                Title = "Select file to play.." 
            }
        );

        if(files.Count > 0)
        {
            ic.SetOutput(files[0].Path);
        }
        else
        {
            ic.SetOutput(null);
        }
    }
}
