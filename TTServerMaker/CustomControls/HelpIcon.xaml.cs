using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using MaterialDesignThemes.Wpf;
using ServerEngine.Models;

namespace TTServerMaker.CustomControls
{
    /// <summary>
    /// Interaction logic for HelpIcon.xaml
    /// </summary>
    public partial class HelpIcon : Button, INotifyPropertyChanged
    {
        public static readonly RoutedUICommand PropertyIconClicked = new RoutedUICommand
        (
            "Help icon clicked",
            "HelpIconClicked",
            typeof(HelpIcon)
        );

        private string _iconType;

        /// <summary>
        /// The custom message to show when the button is clicked
        /// </summary>
        public string HelpMessage { get; set; }

        /// <summary>
        /// The name of the server property which the help is for
        /// </summary>
        public string ServerPropertyName { get; set; }

        public string IconType
        {
            get { return _iconType; }
            set
            {
                _iconType = value;
                OnPropertyChanged();
            }
        }

        public HelpIcon()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Returns the message based on the ServerPropertyName and the HelpMessage
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            string msg = HelpMessage;
            if (!string.IsNullOrEmpty(ServerPropertyName))
                msg = ServerEngine.Models.Properties.GetDescriptionByName(ServerPropertyName);

            return msg;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;


        /*
          This was a fail
        private async void PropertyIconClicked_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string message = (e.Source as HelpIcon)?.GetMessage();
            helpDialog.HelpText = message;

            await DialogHost.Show(helpDialog, "MainDialogHost");
        }

        private void PropertyIconClicked_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty((e.Source as HelpIcon).GetMessage());
        }

        


        */

    }
}
