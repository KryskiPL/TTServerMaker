// <copyright file="HelpDialog.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.CustomControls.Dialogs
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for HelpDialog.xaml.
    /// </summary>
    public partial class HelpDialog : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpDialog"/> class.
        /// </summary>
        public HelpDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the help text.
        /// </summary>
        public string HelpText
        {
            get { return this.helpTextBlock.Text; }
            set { this.helpTextBlock.Text = value; }
        }
    }
}