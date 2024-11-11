using System;
using System.ComponentModel;

namespace Baksteen.Avalonia.Blazor;

public class WrappedRootComponent
{
    private Microsoft.AspNetCore.Components.WebView.WindowsForms.RootComponent? _wrapped;

    /// <summary>
    /// Gets the CSS selector string that specifies where in the document the component should be placed.
    /// This must be unique among the root components within the <see cref="BlazorWebView"/>.
    /// </summary>
    public string Selector
    {
        get; set;
    } = default!;

    /// <summary>
    /// Gets the type of the root component. This type must implement <see cref="IComponent"/>.
    /// </summary>
    public Type ComponentType
    {
        get;set;
    } = default!;

    public WrappedRootComponent()
    {
    }

    public WrappedRootComponent(Microsoft.AspNetCore.Components.WebView.WindowsForms.RootComponent wrapped)
    {
        _wrapped = wrapped;
    }

    public Microsoft.AspNetCore.Components.WebView.WindowsForms.RootComponent Wrapped
    {
        get
        {
            _wrapped ??= new Microsoft.AspNetCore.Components.WebView.WindowsForms.RootComponent(Selector, ComponentType, null);
            return _wrapped;
        }
    }
}
