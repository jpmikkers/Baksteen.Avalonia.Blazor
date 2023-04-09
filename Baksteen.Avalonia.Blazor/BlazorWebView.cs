using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using DynamicData;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using System;

namespace Baksteen.Avalonia.Blazor;

public class BlazorWebView : NativeControlHost
{
    private Uri? _source = null;
    private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView? _blazorWebView;
    private double _zoomFactor = 1.0;
    private string? _hostPage;
    private IServiceProvider _serviceProvider = default!;
    private RootComponentsCollection _rootComponents = new();

    /// <summary>
    /// The <see cref="AvaloniaProperty" /> which backs the <see cref="ZoomFactor" /> property.
    /// </summary>
    public static readonly DirectProperty<BlazorWebView, double> ZoomFactorProperty
        = AvaloniaProperty.RegisterDirect<BlazorWebView, double>(
            nameof(ZoomFactor),
            x => x.ZoomFactor,
            (x, y) => x.ZoomFactor = y);

    public static readonly DirectProperty<BlazorWebView, IServiceProvider> ServicesProperty
        = AvaloniaProperty.RegisterDirect<BlazorWebView, IServiceProvider>(
            nameof(Services),
            x => x.Services,
            (x, y) => x.Services = y);

    public static readonly DirectProperty<BlazorWebView, RootComponentsCollection> RootComponentsProperty
        = AvaloniaProperty.RegisterDirect<BlazorWebView, RootComponentsCollection>(
            nameof(RootComponents),
            x => x.RootComponents,
            (x, y) => x.RootComponents = y);

    public string? HostPage
    {
        get
        {
            if(_blazorWebView != null)
            {
                _hostPage = _blazorWebView.HostPage;
            }
            return _hostPage;
        }

        set
        {
            if(_hostPage != value)
            {
                _hostPage = value;
                if(_blazorWebView != null)
                {
                    _blazorWebView.HostPage = value;
                }
            }
        }
    }

    public Uri? Source
    {
        get
        {
            if(_blazorWebView != null)
            {
                _source = _blazorWebView.WebView.Source;
            }
            return _source;
        }

        set
        {
            if(_source != value)
            {
                _source = value;
                if(_blazorWebView != null)
                {
                    _blazorWebView.WebView.Source = value;
                }
            }
        }
    }

    public double ZoomFactor
    {
        get
        {
            if(_blazorWebView != null)
            {
                _zoomFactor = _blazorWebView.WebView.ZoomFactor;
            }
            return _zoomFactor;
        }

        set
        {
            if(_zoomFactor != value)
            {
                _zoomFactor = value;
                if(_blazorWebView != null)
                {
                    _blazorWebView.WebView.ZoomFactor = value;
                }
            }
        }
    }

    public IServiceProvider Services
    {
        get
        {
            return _serviceProvider;
        }
        set
        {
            _serviceProvider = value;
            if(_blazorWebView != null)
            {
                _blazorWebView.Services = _serviceProvider;
            }
        }
    }

    public RootComponentsCollection RootComponents
    {
        get
        {
            return _rootComponents;
        }
        set
        {
            _rootComponents = value;
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        if(OperatingSystem.IsWindows())
        {
            _blazorWebView = new()
            {
                HostPage = _hostPage,
                Services = _serviceProvider,
            };
            _blazorWebView.WebView.ZoomFactor = Math.Clamp(_zoomFactor, 0.1, 4.0);
            _blazorWebView.RootComponents.AddRange(_rootComponents);
            return new PlatformHandle(_blazorWebView.Handle, "HWND");
        }

        return base.CreateNativeControlCore(parent);
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        if(OperatingSystem.IsWindows())
        {
            _blazorWebView?.Dispose();
            _blazorWebView = null;
        }

        base.DestroyNativeControlCore(control);
    }

    // DestroyNativeControlCore doesn't seem to get called when the app is shutting down, so lets dispose the blazorwebview earlier..
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        if(OperatingSystem.IsWindows())
        {
            _blazorWebView?.Dispose();
            _blazorWebView = null;
        }

        base.OnDetachedFromVisualTree(e);
    }
}
