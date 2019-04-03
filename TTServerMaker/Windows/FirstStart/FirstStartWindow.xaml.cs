using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using MaterialDesignThemes.Wpf;

namespace TTServerMaker.Windows.FirstStart
{
    /// <summary>
    /// Interaction logic for FirstStartWindow.xaml
    /// </summary>
    public partial class FirstStartWindow : Window
    {
        private string ServerFolder_Temp;

        public FirstStartWindow()
        {
            InitializeComponent();

            DefaultBox.IsChecked = true;
            ServerFolder_Temp = ServerEngine.GeneralSettings.GetDefaultServersPath();
        }

        private void DefaultCheckboxChanged(object sender, RoutedEventArgs e)
        {
            if (!IsInitialized)
                return;


            if(DefaultBox.IsChecked ?? false)
            {
                ServerFolder_Temp = InputBox.Text;
                InputBox.Text = ServerEngine.GeneralSettings.GetDefaultServersPath();
                InputBox.IsEnabled = false;
            }
            else
            {
                InputBox.Text = ServerFolder_Temp;
                InputBox.IsEnabled = true;
            }
        }

        /// <summary>
        /// When the textbox gets focus, the folder selection window will pop up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputBox_GotFocus(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = ServerFolder_Temp;
                folderBrowserDialog.Description = "Please select an empty folder to store your servers in";
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ServerFolder_Temp = folderBrowserDialog.SelectedPath;
                    // Checking if the folder is empty, and showing dialog to confirm that the selected folder is right
                    if ((Directory.GetDirectories(folderBrowserDialog.SelectedPath).Length > 0 ||
                        Directory.GetFiles(folderBrowserDialog.SelectedPath).Length > 0) &&
                        System.Windows.MessageBox.Show( "The folder you selected is not empty." +
                            "Are you sure this is where you want to store your server?",
                            "Folder not empty", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    {
                        InputBox_GotFocus(sender, e);
                        return;
                    }

                    InputBox.Text = ServerEngine.AppSettings.EnforceTrailingBackslash(folderBrowserDialog.SelectedPath);
                }
            }
        }

        /// <summary>
        /// The server folder has been selected, it's time to validate it
        /// </summary>
        private void ServerFolderNextButton_Click(object sender, RoutedEventArgs e)
        {
            ServerFolder_Temp = InputBox.Text.Trim();

            // If the input was null, just assume the default folder
            if (string.IsNullOrEmpty(ServerFolder_Temp))
                ServerFolder_Temp = ServerEngine.GeneralSettings.GetDefaultServersPath();

            // Create directory if it dosn't exist
            if(!Directory.Exists(ServerFolder_Temp))
            {
                try
                {
                    Directory.CreateDirectory(ServerFolder_Temp);
                }
                catch
                {
                    System.Windows.MessageBox.Show("There was an error creating the folder you specified. Please try again, on select a different folder",
                            "Failed to create folder", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    return;
                }
            }

            // If everything was fine, go to the next page
            // Transitioner.SelectedIndex++;                <--- If more pages are added in the future

            SetupOver();
        }

        /// <summary>
        /// Gets called when the last screen was validated
        /// </summary>
        private void SetupOver()
        {
            ServerEngine.AppSettings.GeneralSettings = new ServerEngine.GeneralSettings();
            ServerEngine.AppSettings.GeneralSettings.ServerFoldersPath = ServerFolder_Temp;

            try
            {
                ServerEngine.AppSettings.GeneralSettings.Save();
            }
            catch
            {
                System.Windows.MessageBox.Show("Failed to save the settings. Please try again.");
            }

            this.DialogResult = true;
        }
    }
}
