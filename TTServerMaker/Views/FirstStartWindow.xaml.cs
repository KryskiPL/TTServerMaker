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
        }
    }
}