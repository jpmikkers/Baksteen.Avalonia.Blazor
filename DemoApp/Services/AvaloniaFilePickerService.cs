﻿namespace DemoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Dialogs;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.Hosting.Internal;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

public class AvaloniaFilePickerService
{
#pragma warning disable CA1822 // Mark members as static
    public async Task<string?> SelectFile()
#pragma warning restore CA1822 // Mark members as static
    {
        if(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            var mainWindow = lifetime.MainWindow;

            if (mainWindow is not null) 
            {
                var selectedFiles = await mainWindow.StorageProvider.OpenFilePickerAsync(
                    new FilePickerOpenOptions { 
                        AllowMultiple = false, 
                        Title = "select text file" 
                    });
                return selectedFiles.FirstOrDefault()?.TryGetLocalPath();
            }
        }
        return null;
    }
}
