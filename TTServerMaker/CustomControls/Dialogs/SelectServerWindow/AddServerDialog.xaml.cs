using System;
using System.Collections.Generic;
using System.Linq;
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
using ServerEngine.ViewModels;

namespace TTServerMaker.CustomControls.Dialogs.SelectServerWindow
{
    /// <summary>
    /// Interaction logic for AddServerDialog.xaml
    /// </summary>
    public partial class AddServerDialog : UserControl
    {
        private readonly SelectServerVM selectServerVM;

        public AddServerDialog(SelectServerVM ssVM)
        {
            InitializeComponent();

            selectServerVM = ssVM;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO Validation

            selectServerVM.CreateNewServer(ServerNameTextBox.Text); // TODO Server type
        }
    }
}
