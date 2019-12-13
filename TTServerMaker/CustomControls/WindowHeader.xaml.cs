// <copyright file="WindowHeader.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.CustomControls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for WindowHeader.xaml.
    /// </summary>
    public partial class WindowHeader : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowHeader"/> class.
        /// </summary>
        public WindowHeader()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window can be maximized.
        /// </summary>
        public bool IsMaximizable { get; set; }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(Window.GetWindow(this));
        }

        private void MaximizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(Window.GetWindow(this));
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(Window.GetWindow(this));
        }
    }
}