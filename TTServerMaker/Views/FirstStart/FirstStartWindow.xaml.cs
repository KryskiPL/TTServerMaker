// <copyright file="FirstStartWindow.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Views.FirstStart
{
    using System.IO;
    using System.Windows;
    using System.Windows.Forms;

    /// <summary>
    /// Interaction logic for FirstStartWindow.xaml.
    /// </summary>
    public partial class FirstStartWindow : Window
    {
        private string serverFolderTemp;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstStartWindow"/> class.
        /// </summary>
        public FirstStartWindow()
        {
            this.InitializeComponent();

            this.DefaultBox.IsChecked = true;
            this.serverFolderTemp = ServerEngine.GeneralSettings.GetDefaultServersPath;
        }

        private void DefaultCheckboxChanged(object sender, RoutedEventArgs e)
        {
            if (!this.IsInitialized)
            {
                return;
            }

            if (this.DefaultBox.IsChecked ?? false)
            {
                this.serverFolderTemp = this.InputBox.Text;
                this.InputBox.Text = ServerEngine.GeneralSettings.GetDefaultServersPath;
                this.InputBox.IsEnabled = false;
            }
            else
            {
                this.InputBox.Text = this.serverFolderTemp;
                this.InputBox.IsEnabled = true;
            }
        }

        /// <summary>
        /// When the textbox gets focus, the folder selection window will pop up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event args.</param>
        private void InputBox_GotFocus(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = this.serverFolderTemp;
                folderBrowserDialog.Description = "Please select an empty folder to store your servers in";
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.serverFolderTemp = folderBrowserDialog.SelectedPath;

                    // Checking if the folder is empty, and showing dialog to confirm that the selected folder is right
                    if ((Directory.GetDirectories(folderBrowserDialog.SelectedPath).Length > 0 ||
                        Directory.GetFiles(folderBrowserDialog.SelectedPath).Length > 0) &&
                        System.Windows.MessageBox.Show(
                            "The folder you selected is not empty." +
                            "Are you sure this is where you want to store your server?",
                            "Folder not empty",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    {
                        this.InputBox_GotFocus(sender, e);
                        return;
                    }

                    this.InputBox.Text = ServerEngine.AppSettings.EnforceTrailingBackslash(folderBrowserDialog.SelectedPath);
                }
            }
        }

        /// <summary>
        /// The server folder has been selected, it's time to validate it.
        /// </summary>
        private void ServerFolderNextButton_Click(object sender, RoutedEventArgs e)
        {
            this.serverFolderTemp = this.InputBox.Text.Trim();

            // If the input was null, just assume the default folder
            if (string.IsNullOrEmpty(this.serverFolderTemp))
            {
                this.serverFolderTemp = ServerEngine.GeneralSettings.GetDefaultServersPath;
            }

            // Create directory if it dosn't exist
            if (!Directory.Exists(this.serverFolderTemp))
            {
                try
                {
                    Directory.CreateDirectory(this.serverFolderTemp);
                }
                catch
                {
                    System.Windows.MessageBox.Show(
                        "There was an error creating the folder you specified. Please try again, on select a different folder",
                        "Failed to create folder",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);
                    return;
                }
            }

            // If everything was fine, go to the next page
            // Transitioner.SelectedIndex++;                <--- If more pages are added in the future
            this.SetupOver();
        }

        /// <summary>
        /// Gets called when the last screen was validated.
        /// </summary>
        private void SetupOver()
        {
            ServerEngine.AppSettings.GeneralSettings = new ServerEngine.GeneralSettings
            {
                ServerFoldersPath = this.serverFolderTemp,
            };

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