// <copyright file="HelpIcon.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.CustomControls
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for HelpIcon.xaml.
    /// </summary>
    public partial class HelpIcon : Button, INotifyPropertyChanged
    {
        /// <summary>
        /// The property icon clicked command.
        /// </summary>
        public static readonly RoutedUICommand PropertyIconClicked = new RoutedUICommand(
            "Help icon clicked",
            "HelpIconClicked",
            typeof(HelpIcon));

        private string iconType;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpIcon"/> class.
        /// </summary>
        public HelpIcon()
        {
            this.InitializeComponent();
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the fallback custom message to show when the button is clicked if no property name was found.
        /// </summary>
        public string HelpMessage { get; set; }

        /// <summary>
        /// Gets or sets the name of the server property which the help is for.
        /// </summary>
        public string ServerPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the string representing the Material Design icon type.
        /// </summary>
        public string IconType
        {
            get
            {
                return this.iconType;
            }

            set
            {
                this.iconType = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the message based on the ServerPropertyName and the HelpMessage.
        /// </summary>
        public string Message
        {
            get
            {
                string msg = this.HelpMessage;
                if (!string.IsNullOrEmpty(this.ServerPropertyName))
                {
                    msg = ServerEngine.Models.Properties.GetDescriptionByName(this.ServerPropertyName);
                }

                return msg;
            }
        }

        /// <summary>
        /// Handles a property change.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}