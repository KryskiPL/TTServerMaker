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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServerEngine.ViewModels;
using ServerEngine.Models.Servers;
using MaterialDesignThemes.Wpf;

namespace TTServerMaker.Windows
{
    /// <summary>
    /// Interaction logic for ServerSelectWindow.xaml
    /// </summary>
    public partial class ServerSelectWindow : Window
    {
        public SelectServerVM SelectServerVM { get; set; } = new SelectServerVM();

        public ServerSelectWindow()
        {
            InitializeComponent();

            DataContext = SelectServerVM;
        }
        /*
        private void ServerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddLoadButton.IsChecked = !AddLoadButton.IsChecked;
        }*/

        private void ServerListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private async void EditServerButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var result = await DialogHost.Show((sender as FrameworkElement).DataContext, "AddEditDialog");
        }

        private void LoadUpButton_Click(object sender, RoutedEventArgs e)
        {
            ServerBase SelectedServer = (ServerBase)(sender as FrameworkElement).DataContext;
            SelectedServer.LoadUp();
        }
    }
}
