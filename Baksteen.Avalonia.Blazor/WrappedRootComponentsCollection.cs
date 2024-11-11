using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Baksteen.Avalonia.Blazor;

public class WrappedRootComponentsCollection(RootComponentsCollection wrapped) : IList<WrappedRootComponent>
{
    public WrappedRootComponent this[int index] 
    {
        get => new(wrapped[index]); 
        set => wrapped[index] = value.Wrapped; 
    }

    public int Count => wrapped.Count;

    public bool IsReadOnly => false;

    public void Add(WrappedRootComponent item) => wrapped.Add(item.Wrapped);
    public void Clear() => wrapped.Clear();
    public bool Contains(WrappedRootComponent item) => wrapped.Contains(item.Wrapped);
    public void CopyTo(WrappedRootComponent[] array, int arrayIndex) => throw new NotImplementedException();
    public IEnumerator<WrappedRootComponent> GetEnumerator() => wrapped.Select(x => new WrappedRootComponent(x)).GetEnumerator();
    public int IndexOf(WrappedRootComponent item) => wrapped.IndexOf(item.Wrapped);
    public void Insert(int index, WrappedRootComponent item) => wrapped.Insert(index, item.Wrapped);
    public bool Remove(WrappedRootComponent item) => wrapped.Remove(item.Wrapped);
    public void RemoveAt(int index) => wrapped.RemoveAt(index);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
