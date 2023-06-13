// <copyright file="App.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF
{
    using System.Windows;
    using System.Windows.Navigation;
    using CommunityToolkit.Mvvm.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using TTServerMaker.Engine;
    using TTServerMaker.Engine.Models.Servers;
    using TTServerMaker.Engine.Services;
    using TTServerMaker.Engine.ViewModels;
    using TTServerMaker.Engine.ViewModels.ServerSelectWindow;
    using TTServerMaker.WPF.Services;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Registering IoC
            this.RegisterIoC();

            Views.MainWindow mainWindow = new Views.MainWindow();
            this.MainWindow = mainWindow;

            // Showing the greeting screen (with folder selection) on first startup
            if (AppSettings.FirstLaunch)
            {
                this.ShowFirstLaunchWindow();
            }

            // Showing the server select dialog
            BasicInfo info = this.ShowServerSelectWindow();

            MainWindowVM mainWindowVM = new MainWindowVM(new VanillaServer(new BasicInfo() { Name ="My First Server"}));

            //mainWindow.DataContext = mainWindowVM;
            //mainWindow.Show();
        }

        private bool ShowFirstLaunchWindow()
        {
            Views.FirstStartWindow firstStartWindow = new Views.FirstStartWindow();

            bool? dialogResult = firstStartWindow.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                this.Shutdown();
            }

            return true;
        }

        private BasicInfo ShowServerSelectWindow()
        {
            Views.ServerSelectWindow serverSelectWindow = new Views.ServerSelectWindow();
            bool? serverSelectDialogResult = serverSelectWindow.ShowDialog();

            BasicInfo selectedServer = (serverSelectWindow.DataContext as ServerSelectWindowViewModel).SelectedServer;
            if (selectedServer == null)
            {
                this.Shutdown();
                return null;
            }

            return selectedServer;
        }

        private void RegisterIoC()
        {
            Ioc.Default.ConfigureServices(
                  new ServiceCollection()
                  .AddSingleton<IBasicInfoManagerService, BasicInfoManagerService>()
                  .AddSingleton<IFolderSelectorService, WindowsFolderSelectorService>()
                  .AddSingleton<IServerSettingsManagerService, ServerSettingsManagerService>()
                  .BuildServiceProvider());
            /*
            Messenger.Register
                <IMessenger>(() => Messenger.Default);
             */
        }
    }
}