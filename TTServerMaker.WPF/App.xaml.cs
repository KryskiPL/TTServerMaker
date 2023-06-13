// <copyright file="App.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF;

using System.Windows;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TTServerMaker.Engine;
using TTServerMaker.Engine.Models.Servers;
using TTServerMaker.Engine.Services;
using TTServerMaker.Engine.ViewModels.Main;
using TTServerMaker.Engine.ViewModels.ServerSelect;
using TTServerMaker.WPF.Services;
using TTServerMaker.WPF.Views;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    private static void RegisterIoC()
    {
        Ioc.Default.ConfigureServices(
              new ServiceCollection()
              .AddSingleton<IBasicInfoManagerService, BasicInfoManagerService>()
              .AddSingleton<IFolderSelectorService, WindowsFolderSelectorService>()
              .AddSingleton<IServerSettingsManagerService, ServerSettingsManagerService>()
              .BuildServiceProvider());
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        // Registering IoC
        RegisterIoC();

        MainWindow mainWindow = new ();
        this.MainWindow = mainWindow;

        // Showing the greeting screen (with folder selection) on first startup
        if (AppSettings.FirstLaunch)
        {
            this.ShowFirstLaunchWindow();
        }

        // Showing the server select dialog
        BasicInfo info = this.ShowServerSelectWindow();

        MainViewModel mainWindowVM = new (new VanillaServer(new BasicInfo() { Name = "My First Server" }));

        // mainWindow.DataContext = mainWindowVM;
        // mainWindow.Show();
    }

    private bool ShowFirstLaunchWindow()
    {
        FirstStartWindow firstStartWindow = new ();

        bool? dialogResult = firstStartWindow.ShowDialog();
        if (!dialogResult.HasValue || !dialogResult.Value)
        {
            this.Shutdown();
        }

        return true;
    }

    private BasicInfo ShowServerSelectWindow()
    {
        ServerSelectWindow serverSelectWindow = new ();
        serverSelectWindow.ShowDialog();

        BasicInfo selectedServer = (serverSelectWindow.DataContext as ServerSelectViewModel).SelectedServer;
        if (selectedServer == null)
        {
            this.Shutdown();
            return null;
        }

        return selectedServer;
    }
}