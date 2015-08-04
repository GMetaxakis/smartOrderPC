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

namespace SmartOrder
{
    /// <summary>
    /// Interaction logic for OrdersControl.xaml
    /// </summary>
    public partial class OrdersControl : UserControl
    {
        List<MyTable> tables;
        

        public OrdersControl()
        {
            InitializeComponent();
            this.DataContext = this;
            update();
        }

        public void update()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                try
                {
                    updateTables();
                    updateOrders();
                    updateCosts();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }));
        }
        
        private void updateTables()
        {
            tables = DBController.LoadTables();
            tablesComboBox.ItemsSource = null;
            tablesComboBox.ItemsSource = tables;
            tablesComboBox.SelectedIndex = 0;
        }

        private void updateOrders()
        {
            lbOrders.ItemsSource = null;
            List<Order> ordersToShow = new List<Order>();
            if (checkbox.IsChecked == true)
            {
                if (((MyTable)tablesComboBox.SelectedItem).Orders != null) 
                {
                    foreach (Order o in ((MyTable)tablesComboBox.SelectedItem).Orders)
                    {
                        if (o.Opened)
                            ordersToShow.Add(o);
                    }
                }
                
            }
            else
                ordersToShow = ((MyTable)tablesComboBox.SelectedItem).Orders;
            
            lbOrders.ItemsSource = ordersToShow;
        }

        private void updateCosts()
        {
            double sumcost = 0.0;
            double tablecost = 0.0;
            double sumIncome = 0.0;
            //sumcost
            foreach (MyTable t in tables)
            {
                foreach (Order o in t.Orders)
                {
                    if(t.Table_id == (tablesComboBox.SelectedItem as MyTable).Table_id)
                        if(o.Opened)
                            tablecost += o.Cost;

                    if(o.Opened)
                        sumcost += o.Cost;

                    if (!o.Opened)
                        sumIncome +=o.Cost;
                }
            }
            lbtablecost.Content = "Υπόλοιπο τραπεζιού : " + tablecost + "€";

            lbsummarycost.Content = "Συνολικό υπόλοιπο : " + sumcost + "€";
            
            lbsummaryIncome.Content = "Είσπραξη τώρα : " + sumIncome + "€"; 
        }

        private void clearOrderFromTables()
        {
            foreach (MyTable t in tables)
                t.Orders.Clear();
        }

        private void tablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MyTable selectedItem = (MyTable) (sender as ComboBox).SelectedItem;
            if (selectedItem != null)
            {
                updateOrders();
                updateCosts();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddOrder ao = new AddOrder(tables, tablesComboBox.SelectedIndex);
            ao.ShowDialog();
            updateTables();
            updateCosts();
        }

        private void CloseOrder(object sender, RoutedEventArgs e)
        {
            string id = getId(sender);

            DBController.CloseOrder(id);
            updateTables();
            updateCosts();
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            string id = getId(sender);

            DBController.DeleteOrder(id);
            updateTables();
            updateCosts();
        }
        
        private string getId(object sender)
        {
            MenuItem mnu = sender as MenuItem;
            StackPanel stackpanel = null;
            if (mnu != null)
            {
                stackpanel = ((ContextMenu)mnu.Parent).PlacementTarget as StackPanel;
            }
            return stackpanel.Tag.ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            updateOrders();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            updateOrders();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string msgtext = "Είσαι σίγουρος ότι θέλεις να διαγράψεις όλες τις παραγγελίες";
            string txt = "Διαγραφή παραγγελιών";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
            if (result == MessageBoxResult.OK)
            {
                DBController.DeleteAllOrders();
            }

            update();
        }

    }
}
