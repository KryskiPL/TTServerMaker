using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ServerEngine;

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
            bool? ServerSelectDialogResult = serverSelectWindow.ShowDialog();

            if(!ServerSelectDialogResult.HasValue || !ServerSelectDialogResult.Value)
                base.Shutdown();



            MainWindow.Show();
        }
    }
}
