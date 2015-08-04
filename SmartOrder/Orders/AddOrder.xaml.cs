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
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {

        public AddOrder(List<MyTable> tables, int myTableIndex)
        {
            InitializeComponent();
            loadTables(tables, myTableIndex);
            loadItems();
            loadUsers();
            loadCharacteristics();
        }

        private void loadCharacteristics()
        {
            try
            {
                lbCharacteristics.ItemsSource = DBController.LoadCharacteristics();
                lbCharacteristics.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void loadTables(List<MyTable> tables, int myTableIndex)
        {
            try
            {
                lbTables.ItemsSource = tables;
                lbTables.SelectedIndex = myTableIndex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void loadItems()
        {
            try
            {
                lbItem.ItemsSource = DBController.LoadItems();
                lbItem.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void loadUsers()
        {
            try
            {
                lbUser.ItemsSource = DBController.LoadUsers();
                lbUser.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            //add order
            //AddOrder(int table_id, int items_id, int user_id)
            if (lbTables.Text == null)
                return;
            try
            {
                int table_id = Convert.ToInt32(lbTables.Text.ToString());

                int items_id = ((Items)lbItem.SelectedItem).Id;
                int user_id = ((User)lbUser.SelectedItem).User_id;

                int order_id = DBController.AddOrder(table_id, items_id, user_id);

                //add characteristicsInOrder
                foreach (Characteristics ch in lbCharacteristics.Items)
                {
                    if (ch.Selected)
                    {
                        Console.WriteLine(ch);
                        DBController.AddCharacteristicsInOrder(order_id, ch.Id);
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
