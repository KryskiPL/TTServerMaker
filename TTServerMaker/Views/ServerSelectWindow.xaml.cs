// <copyright file="ServerSelectWindow.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Views
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using MaterialDesignThemes.Wpf;
    using TTServerMaker.CustomControls.Dialogs.SelectServerWindow;
    using TTServerMaker.ServerEngine.Models.Servers;
    using TTServerMaker.ServerEngine.ViewModels;

    /// <summary>
    /// Interaction logic for ServerSelectWindow.xaml.
    /// </summary>
    public partial class ServerSelectWindow : Window
    {
        private readonly EditServerDialog editDialogContent = new EditServerDialog();
        private readonly AddServerDialog addDialogContent;

        private Point scrollMousePoint = default;
        private double vOff = 1;
        private bool dragScrolling = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSelectWindow"/> class.
        /// </summary>
        public ServerSelectWindow()
        {
            this.InitializeComponent();

            this.DataContext = this.SelectServerVM;
            this.addDialogContent = new AddServerDialog(this.SelectServerVM);
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        public SelectServerVM SelectServerVM { get; set; } = new SelectServerVM();

        /// <summary>
        /// Gets or sets the currently selected server.
        /// </summary>
        public ServerBase SelectedServer { get; set; }

        private async void LoadUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedServer = (ServerBase)(sender as FrameworkElement)?.DataContext;

            // Loading up the server
            await Task.Run(() => this.SelectedServer.LoadUp());

            this.DialogResult = true;
            this.Close();
        }

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.scrollMousePoint = e.GetPosition(this.ScrollViewer);
            this.vOff = this.ScrollViewer.VerticalOffset;
            this.dragScrolling = true;
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragScrolling)
            {
                ScrollViewer scrollViewer1 = this.ScrollViewer;
                this.ScrollViewer.ScrollToVerticalOffset(this.vOff + (this.scrollMousePoint.Y - e.GetPosition(scrollViewer1).Y));
            }
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.dragScrolling = false;
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ServerBase server = (sender as FrameworkElement)?.DataContext as ServerBase;

            this.editDialogContent.NewEdit(server);
            await DialogHost.Show(this.editDialogContent, this.DialogHost.Identifier);
        }

        private async void AddNewServerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((await DialogHost.Show(this.addDialogContent, this.DialogHost.Identifier))?.ToString());

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
                this.SelectServerVM.DeleteServer((sender as FrameworkElement)?.DataContext as ServerBase);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to delete server. " + exception.Message);
            }
        }
    }
}