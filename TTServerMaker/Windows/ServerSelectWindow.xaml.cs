using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServerEngine.ViewModels;
using ServerEngine.Models.Servers;
using MaterialDesignThemes.Wpf;
using ServerEngine.Models;
using TTServerMaker.CustomControls.Dialogs.SelectServerWindow;

namespace TTServerMaker.Windows
{
    /// <summary>
    /// Interaction logic for ServerSelectWindow.xaml
    /// </summary>
    public partial class ServerSelectWindow : Window
    {
        private EditServerDialog editDialogContent = new EditServerDialog();

        public SelectServerVM SelectServerVM { get; set; } = new SelectServerVM();

        public ServerBase SelectedServer;


        public ServerSelectWindow()
        {
            InitializeComponent();

            DataContext = SelectServerVM;
        }


        private async void LoadUpButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedServer = (ServerBase)(sender as FrameworkElement)?.DataContext;

            // Loading up the server
            await Task.Run(() => SelectedServer.LoadUp());


            DialogResult = true;
            Close();
        }

        #region Dragscrolling
        private Point _scrollMousePoint = new Point();
        private double _vOff = 1;
        private bool _dragScrolling = false;

        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _scrollMousePoint = e.GetPosition(scrollViewer);
            _vOff = scrollViewer.VerticalOffset;
            _dragScrolling = true;
        }

        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragScrolling)
                scrollViewer.ScrollToVerticalOffset(_vOff + (_scrollMousePoint.Y - e.GetPosition(scrollViewer).Y));
        }

        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragScrolling = false;
        }


        #endregion

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ServerBase server = (sender as FrameworkElement)?.DataContext as ServerBase;

            editDialogContent.NewEdit(server);
            
            await DialogHost.Show(editDialogContent, dialogHost.Identifier);
        }

        private void AddNewServerButton_Click(object sender, RoutedEventArgs e)
        {
            /* TODO
            EditDialogContent.DataContext = null;
            await EditServerDialog.ShowDialog(EditDialogContent);
            ServerTypeCombobox.SelectedIndex = -1;
            */
        }


        private void DeleteServerButton_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectServerVM.DeleteServer(((sender as FrameworkElement)?.DataContext as ServerBase));
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to delete server. " + exception.Message);
            }
            
        }
        
    }
}
