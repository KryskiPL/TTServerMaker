// <copyright file="EditServerDialog.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.CustomControls.Dialogs.SelectServerWindow
{
    using System.Windows;
    using System.Windows.Controls;
    using TTServerMaker.Engine.Models;
    using TTServerMaker.Engine.Models.Servers;

    /// <summary>
    /// Interaction logic for EditServerDialog.xaml.
    /// </summary>
    public partial class EditServerDialog : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditServerDialog"/> class.
        /// </summary>
        public EditServerDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Starts a new server edit.
        /// </summary>
        /// <param name="server">The server that is being edited.</param>
        public void NewEdit(ServerBase server)
        {
            this.editStackPanel.BindingGroup.BeginEdit();
            this.DataContext = server;
        }

        private void EditDialogCancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.editStackPanel.BindingGroup.CancelEdit();
        }

        private void EditDoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.editStackPanel.BindingGroup.CommitEdit())
            {
                // Saving changes to file
                (this.editStackPanel.BindingGroup.Items[1] as ServerSettings)?.SaveChangesAsync();
                this.editStackPanel.BindingGroup.BeginEdit();
            }
        }
    }
}