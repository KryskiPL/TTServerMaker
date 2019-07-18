using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MaterialDesignThemes.Wpf;
using TTServerMaker.CustomControls;
using TTServerMaker.CustomControls.Dialogs;

namespace TTServerMaker.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HelpDialog helpDialog = new HelpDialog();

        public MainWindow()
        {
            InitializeComponent();

            // Hiding the tab item headers
            foreach(TabItem item in TabControl.Items.OfType<TabItem>())
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
            string message = (e.Source as HelpIcon)?.GetMessage();
            helpDialog.HelpText = message;

            await DialogHost.Show(helpDialog, "MainDialogHost");
        }

        private void PropertyIconClicked_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty((e.Source as HelpIcon)?.GetMessage());
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand PropertyIconClicked = new RoutedUICommand
        (
            "PropertyIconClicked",
            "PropertyIconClicked",
            typeof(CustomCommands)
        );

    }
}
