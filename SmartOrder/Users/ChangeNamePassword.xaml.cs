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
    /// Interaction logic for ChangeNamePassword.xaml
    /// </summary>
    public partial class ChangeNamePassword : Window
    {
        string user_id;
        Boolean username;
        public ChangeNamePassword(string lblText, string txtText, string id, Boolean username)
        {
            user_id = id;
            this.username = username;
            InitializeComponent();
            lblNamePassword.Content = lblText;
            txtNamePassword.Text = txtText; 
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            //check password
            if (username)
                DBController.ChangeUserName(user_id, txtNamePassword.Text);
            else
                DBController.ChangeUserPass(user_id, txtNamePassword.Text);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
