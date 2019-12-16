// <copyright file="App.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF
{
    using System.Windows;
    using TTServerMaker.Engine;
    using TTServerMaker.Engine.ViewModels;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Views.MainWindow mainWindow = new Views.MainWindow();
            this.MainWindow = mainWindow;

            // Showing the greeting screen (with folder selection) on first startup
            if (AppSettings.FirstLaunch)
            {
                Views.FirstStart.FirstStartWindow firstStartWindow = new Views.FirstStart.FirstStartWindow();

                bool? dialogResult = firstStartWindow.ShowDialog();
                if (!dialogResult.HasValue || !dialogResult.Value)
                {
                    this.Shutdown();
                }
            }

            // Showing the server select dialog
            Views.ServerSelectWindow serverSelectWindow = new Views.ServerSelectWindow();
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