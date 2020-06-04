// <copyright file="AddServerDialog.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.CustomControls.Dialogs.SelectServerWindow
{
    using System.Windows;
    using System.Windows.Controls;
    using TTServerMaker.Engine.ViewModels;

    /// <summary>
    /// Interaction logic for AddServerDialog.xaml.
    /// </summary>
    public partial class AddServerDialog : UserControl
    {
        private readonly ServerSelectWindowVM selectServerVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddServerDialog"/> class.
        /// </summary>
        /// <param name="selectServerViewModel">The parent view model.</param>
        public AddServerDialog(ServerSelectWindowVM selectServerViewModel)
        {
            this.InitializeComponent();

            this.selectServerVM = selectServerViewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO Validation
            // this.selectServerVM.CreateNewServer(this.ServerNameTextBox.Text); // TODO Server type
        }
    }
}