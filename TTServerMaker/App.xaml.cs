// <copyright file="App.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF
{
    using System.Windows;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;
    using TTServerMaker.Engine;
    using TTServerMaker.Engine.Models.Servers;
    using TTServerMaker.Engine.Services;
    using TTServerMaker.Engine.ViewModels;
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

            // MainWindowVM mainWindowVM = new MainWindowVM(serverSelectWindow.SelectServerVM.LoadedServer);

            // mainWindow.DataContext = mainWindowVM;
            // mainWindow.Show();
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

            BasicInfo selectedServer = (serverSelectWindow.DataContext as ServerSelectWindowVM).SelectedServer;
            if (!serverSelectDialogResult.HasValue || !serverSelectDialogResult.Value || selectedServer == null)
            {
                this.Shutdown();
                return null;
            }

            return selectedServer;
        }

        private void RegisterIoC()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default
                .Register<IBasicInfoManagerService, BasicInfoManagerService>();

            SimpleIoc.Default
                .Register<IFolderSelectorService, WindowsFolderSelectorService>();

            SimpleIoc.Default
                .Register<IBasicInfoManagerService, BasicInfoManagerService>();

            SimpleIoc.Default.Register
                <IMessenger>(() => Messenger.Default);
        }
    }
}