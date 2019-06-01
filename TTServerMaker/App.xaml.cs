using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ServerEngine;
using ServerEngine.Models.Servers;
using ServerEngine.ViewModels;

namespace TTServerMaker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Windows.MainWindow MainWindow = new Windows.MainWindow();
            this.MainWindow = MainWindow;

            // Showing the greeting screen (with folder selection) on first startup
            if (AppSettings.FirstLaunch)
            {
                Windows.FirstStart.FirstStartWindow firstStartWindow = new Windows.FirstStart.FirstStartWindow();

                bool? dialogResult = firstStartWindow.ShowDialog();
                if (!dialogResult.HasValue || !dialogResult.Value)
                    base.Shutdown();
            }

            // Showing the server select dialog
            Windows.ServerSelectWindow serverSelectWindow = new Windows.ServerSelectWindow();
            bool? serverSelectDialogResult = serverSelectWindow.ShowDialog();

            if (!serverSelectDialogResult.HasValue || !serverSelectDialogResult.Value ||
                serverSelectWindow.SelectedServer == null)
            {
                base.Shutdown();
                return;
            }


            MainWindowVM mainWindowVM = new MainWindowVM(serverSelectWindow.SelectedServer);

            // TODO nem tom itt kéne-e csinálni, lehet jobb lenne még a ServerSelectWindow-nál
            mainWindowVM.Server.LoadUp();

            MainWindow.DataContext = mainWindowVM;
            MainWindow.Show();
        }
    }
}
