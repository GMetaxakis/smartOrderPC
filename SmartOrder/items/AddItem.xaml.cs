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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem(int parent_id)
        {
            InitializeComponent();
            lbParent.ItemsSource = DBController.LoadProducts(parent_id);
            lbParent.SelectedIndex = 0;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            Items item = new Items(-1, (lbParent.SelectedItem as Products).Id, txtName.Text, Convert.ToDouble(txtPrice.Text));
            DBController.AddItem(item);
            this.DialogResult = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
