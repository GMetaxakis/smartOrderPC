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
using System.Windows.Shapes;

namespace SmartOrder
{
    /// <summary>
    /// Interaction logic for CreateUserInputDialog.xaml
    /// </summary>
    public partial class CreateUserInputDialog : Window
    {
        public CreateUserInputDialog()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            //check password
            DBController.AddUser(txtName.Text, txtPassword.Text);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
