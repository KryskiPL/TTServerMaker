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
using ServerEngine.Models;
using ServerEngine.Models.Servers;

namespace TTServerMaker.CustomControls.Dialogs.SelectServerWindow
{
    /// <summary>
    /// Interaction logic for EditServerDialog.xaml
    /// </summary>
    public partial class EditServerDialog : UserControl
    {
        public EditServerDialog()
        {
            InitializeComponent();
        }

        public void NewEdit(ServerBase server)
        {
            editStackPanel.BindingGroup.BeginEdit();
            this.DataContext = server;
        }

        private void EditDialogCancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            editStackPanel.BindingGroup.CancelEdit();
        }

        private void EditDoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (editStackPanel.BindingGroup.CommitEdit())
            {
                // Saving changes to file
                (editStackPanel.BindingGroup.Items[1] as BasicServerInfo)?.SaveBasicServerInfo();
                editStackPanel.BindingGroup.BeginEdit();
            }
        }
    }
}
