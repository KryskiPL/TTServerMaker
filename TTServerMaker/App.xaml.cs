// <copyright file="App.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker
{
    using System.Windows;
    using TTServerMaker.ServerEngine;
    using TTServerMaker.ServerEngine.ViewModels;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Windows.MainWindow mainWindow = new Windows.MainWindow();
            this.MainWindow = mainWindow;

            // Showing the greeting screen (with folder selection) on first startup
            if (AppSettings.FirstLaunch)
            {
                Windows.FirstStart.FirstStartWindow firstStartWindow = new Windows.FirstStart.FirstStartWindow();

                bool? dialogResult = firstStartWindow.ShowDialog();
                if (!dialogResult.HasValue || !dialogResult.Value)
                {
                    this.Shutdown();
                }
            }

            // Showing the server select dialog
            Windows.ServerSelectWindow serverSelectWindow = new Windows.ServerSelectWindow();
            bool? serverSelectDialogResult = serverSelectWindow.ShowDialog();

            if (!serverSelectDialogResult.HasValue || !serverSelectDialogResult.Value ||
                serverSelectWindow.SelectedServer == null)
            {
                this.Shutdown();
                return;
            }

            MainWindowVM mainWindowVM = new MainWindowVM(serverSelectWindow.SelectedServer);

            mainWindow.DataContext = mainWindowVM;
            mainWindow.Show();
        }
    }
}