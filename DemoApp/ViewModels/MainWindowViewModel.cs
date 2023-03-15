using System;
using ReactiveUI;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.ComponentModel.DataAnnotations;
using Avalonia.Threading;
using static System.Net.WebRequestMethods;

namespace DemoApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private double _zoom = 1.0;
    public ReactiveCommand<Unit, Unit> ExitCommand { get; }
    public ReactiveCommand<double, Unit> ZoomCommand { get; }
    public Interaction<Unit, Unit> ExitInteraction { get; }

    public double Zoom
    {
        get => _zoom;
        set => this.RaiseAndSetIfChanged(ref _zoom, value);
    }

    public MainWindowViewModel()
    {
        if(!Avalonia.Controls.Design.IsDesignMode)
        {
        }
        ExitCommand = ReactiveCommand.CreateFromTask(OnExit);
        ZoomCommand = ReactiveCommand.CreateFromTask<double,Unit>(OnZoom);
        ExitInteraction = new Interaction<Unit, Unit>();
    }

    private async Task OnExit()
    {
        await ExitInteraction.Handle(Unit.Default);
    }

    private async Task<Unit> OnZoom(double zoom)
    {
        Zoom = zoom;
        await Task.CompletedTask;
        return Unit.Default;
    }
}
