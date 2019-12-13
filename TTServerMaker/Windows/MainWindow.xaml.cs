// <copyright file="MainWindow.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Windows
{
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using MaterialDesignThemes.Wpf;
    using TTServerMaker.CustomControls;
    using TTServerMaker.CustomControls.Dialogs;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HelpDialog helpDialog = new HelpDialog();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            // Hiding the tab item headers
            foreach (TabItem item in this.TabControl.Items.OfType<TabItem>())
            {
                item.Visibility = Visibility.Collapsed;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private async void PropertyIconClicked_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string message = (e.Source as HelpIcon)?.Message;
            this.helpDialog.HelpText = message;

            await DialogHost.Show(this.helpDialog, "MainDialogHost");
        }

        private void PropertyIconClicked_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty((e.Source as HelpIcon)?.Message);
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand PropertyIconClicked = new RoutedUICommand(
            "PropertyIconClicked",
            "PropertyIconClicked",
            typeof(CustomCommands));
    }
}