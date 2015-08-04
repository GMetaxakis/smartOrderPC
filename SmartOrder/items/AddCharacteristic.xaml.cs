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
    /// Interaction logic for AddCharacteristic.xaml
    /// </summary>
    public partial class AddCharacteristic : Window
    {
        public AddCharacteristic()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            Characteristics characteristic = new Characteristics(-1, txtName.Text, Convert.ToDouble(txtCost.Text));
            DBController.AddCharacteristics(characteristic);
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
