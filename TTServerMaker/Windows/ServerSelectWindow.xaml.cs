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

namespace TTServerMaker.Windows
{
    /// <summary>
    /// Interaction logic for ServerSelectWindow.xaml
    /// </summary>
    public partial class ServerSelectWindow : Window
    {
        public SelectServerVM SelectServerVM { get; set; } = new SelectServerVM();

        public ServerBase SelectedServer;

        private bool isEditing;

        public ServerSelectWindow()
        {
            InitializeComponent();

            DataContext = SelectServerVM;
        }


        private void LoadUpButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedServer = (ServerBase)(sender as FrameworkElement)?.DataContext;

            DialogResult = true;
            Close();

            /*
            selectedServer?.LoadUp();
            */
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
            
            isEditing = true;
            ServerBase server = (sender as FrameworkElement)?.DataContext as ServerBase;
            EditDialogContent.DataContext = server;
            
            // Setting the server type combobox value
            foreach (ComboBoxItem comboBoxItem in ServerTypeCombobox.Items)
            {
                string item = comboBoxItem?.Content.ToString();
                if (server != null && item == server.ServerTypeStr)
                    ServerTypeCombobox.SelectedItem = comboBoxItem;
            }

           EditDialogContent.BindingGroup.BeginEdit();
            await EditServerDialog.ShowDialog(EditDialogContent);
        }

        private async void AddNewServerButton_Click(object sender, RoutedEventArgs e)
        {
            isEditing = false;
            EditDialogContent.DataContext = null;
            await EditServerDialog.ShowDialog(EditDialogContent);
            ServerTypeCombobox.SelectedIndex = -1;
        }

        private bool editDialogValidated = false;

        private async void EditDoneButton_Click(object sender, RoutedEventArgs e)
        {
            editDialogValidated = EditDialogContent.BindingGroup.CommitEdit();

            if (editDialogValidated)
            {
                // Saving changes to file
                await (EditDialogContent.BindingGroup.Items[1] as BasicServerInfo)?.SaveBasicServerInfo();
                EditDialogContent.BindingGroup.BeginEdit();
            }

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

        private void EditDialogContent_OnError(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }

        private void EditDialogCancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Cancel the pending changes and begin a new edit transaction.
            EditDialogContent.BindingGroup.CancelEdit();
            EditDialogContent.BindingGroup.BeginEdit();
        }

        private void EditServerDialog_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            throw new NotImplementedException();
        }
    }
}
