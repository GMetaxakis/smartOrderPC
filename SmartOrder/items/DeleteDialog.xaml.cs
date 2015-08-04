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
    /// Interaction logic for DeleteDialog.xaml
    /// </summary>
    public partial class DeleteDialog : Window
    {
        int id;
        string type;

        public DeleteDialog(string textToShow, string type, int id)
        {
            InitializeComponent();
            lblCategory.Content = textToShow;
            this.id = id;
            this.type = type;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            //if delete category
            //delete also products that have as parent this category?
            if (type == "CATEGORY")
                DBController.DeleteCategory(id);
            else if (type == "PRODUCT")
                DBController.DeleteProduct(id);
            else if (type == "ITEM")
                DBController.DeleteItem(id);
            else if (type == "CHARACTERISTIC")
                DBController.DeleteCharacteristics(id);
            else
                MessageBox.Show("ERROR on DeleteDialog");

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
