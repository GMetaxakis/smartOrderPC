using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ItemsControl.xaml
    /// </summary>
    public partial class ItemsControl : UserControl
    {
        public ItemsControl()
        {
            InitializeComponent();
            loadCategories();
            LoadCharacteristics();
        }

        void loadCategories()
        {
            List<Categories> categories = DBController.LoadCategories();
            categories.Add(new Categories(0, "Όλα"));
            cmbCategories.ItemsSource = null;
            cmbCategories.ItemsSource = categories;
            cmbCategories.SelectedIndex = 0;
        }

        private void LoadCharacteristics()
        {
            cmbCharacteristics.ItemsSource = DBController.LoadCharacteristics();
            cmbCharacteristics.SelectedIndex = 0;
        }

        void loadProducts(int category_id)
        {
            List<Products> products = DBController.LoadProducts(category_id);

            if (products.Count != 0)
            {
                //products.Add(new Products(0, 0, "Όλα"));
                cmbProducts.ItemsSource = products;
                cmbProducts.SelectedIndex = 0;
                cmbProducts.IsEnabled = true;
                btnDeleteProduct.IsEnabled = true;
            }
            else
            {
                cmbProducts.ItemsSource = products;
                cmbProducts.IsEnabled = false;
                btnDeleteProduct.IsEnabled = false;
            }
        }
        void loadProducts()
        {
            loadProducts((cmbCategories.SelectedItem as Categories).Id);
        }

        void loadItems(int product_id)
        {
            List<Items> items = DBController.LoadItems(product_id);

            if (items.Count != 0)
            {
                cmbItems.ItemsSource = items;
                cmbItems.SelectedIndex = 0;
                cmbItems.IsEnabled = true;
                btnDeleteItem.IsEnabled = true;
                btnSavePrice.IsEnabled = true;
                txtPrice.IsEnabled = true;
            }
            else
            {
                cmbItems.ItemsSource = items;
                cmbItems.IsEnabled = false;
                btnDeleteItem.IsEnabled = false;
                btnSavePrice.IsEnabled = false;
                txtPrice.IsEnabled = false;
            }
        }
        void loadItems()
        {
            loadItems((cmbProducts.SelectedItem as Products).Id);
        }

        void emptyItems()
        {
            cmbItems.ItemsSource = null;
            cmbItems.IsEnabled = false;
            btnDeleteItem.IsEnabled = false;
        }

        #region combobox events
        private void cmbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Categories selectedItem = (Categories)(sender as ComboBox).SelectedItem;

            if (selectedItem != null)
            {
                if (selectedItem.Name == "Όλα")
                    btnDeleteCategory.IsEnabled = false;
                else
                    btnDeleteCategory.IsEnabled = true;

                loadProducts(selectedItem.Id);
            }
        }

        private void cmbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Products selectedItem = (Products)(sender as ComboBox).SelectedItem;


            if (selectedItem != null)
            {
                if (selectedItem.Name == "Όλα")
                    btnDeleteProduct.IsEnabled = false;
                else
                    btnDeleteProduct.IsEnabled = true;

                loadItems(selectedItem.Id);
            }
            else
            {
                emptyItems();
            }
        }

        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Items selectedItem = (Items)(sender as ComboBox).SelectedItem;
            if (selectedItem != null)
            {
                txtPrice.Text = selectedItem.Price + "";
            }
            else
            {
                txtPrice.Text = "";
            }
        }

        private void cmbCharacteristics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Characteristics characteristic = (Characteristics)(sender as ComboBox).SelectedItem;
            if (characteristic != null)
            {
                txtCost.Text = characteristic.Cost + "";
            }
            else
            {
                txtCost.Text = "";
            }
        }

        #endregion

        #region buttons add
        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategory ac = new AddCategory();
            bool? rv = ac.ShowDialog();
            if (rv == true)
                loadCategories();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct ap = new AddProduct(cmbCategories.SelectedIndex == cmbCategories.Items.Count - 1 ? 0 : cmbCategories.SelectedIndex);
            bool? rv = ap.ShowDialog();
            if (rv == true)
                loadProducts();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItem ai = new AddItem((cmbCategories.SelectedItem as Categories).Id);
            bool? rv = ai.ShowDialog();
            if (rv == true)
                loadItems();
        }

        private void btnAddCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            AddCharacteristic ac = new AddCharacteristic();
            bool? rv = ac.ShowDialog();
            if (rv == true)
                LoadCharacteristics();
        }
        #endregion


        #region buttons delete
        private void btnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            Categories selected_category = (cmbCategories.SelectedItem as Categories);
            DeleteDialog dd = new DeleteDialog("Είσαι σίγουρος ότι θες να διαγράψεις την κατηγορία : " + selected_category.Name, "CATEGORY", selected_category.Id);
            bool? rv = dd.ShowDialog();
            if (rv == true)
                loadCategories();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Products selected_product = (cmbProducts.SelectedItem as Products);
            DeleteDialog dd = new DeleteDialog("Είσαι σίγουρος ότι θες να διαγράψεις το προϊόν : " + selected_product.Name, "PRODUCT", selected_product.Id);
            bool? rv = dd.ShowDialog();
            if (rv == true)
                loadProducts();
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            Items selected_item = (cmbItems.SelectedItem as Items);
            DeleteDialog dd = new DeleteDialog("Είσαι σίγουρος ότι θες να διαγράψεις το είδος : " + selected_item.Name, "ITEM", selected_item.Id);
            bool? rv = dd.ShowDialog();
            if (rv == true)
                loadItems();
        }

        private void btnDeleteCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            Characteristics characteristic = cmbCharacteristics.SelectedItem as Characteristics;
            DeleteDialog dd = new DeleteDialog("Είσαι σίγουρος ότι θες να διαγράψεις το είδος : " + characteristic.Name, "CHARACTERISTIC", characteristic.Id);
            bool? rv = dd.ShowDialog();
            if (rv == true)
                LoadCharacteristics();
        }
        #endregion

        private void btnSavePrice_Click(object sender, RoutedEventArgs e)
        {
            double price = 0;
            try
            {
                price = Convert.ToDouble(txtPrice.Text.ToString());
                DBController.SavePrice(price, (cmbItems.SelectedItem as Items).Id);
                loadItems((cmbProducts.SelectedItem as Products).Id);
            }
            catch (Exception)
            {
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnSavePrice_Click(sender, null);
            }
        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnSaveCost_Click(sender, null);
            }

        }

        private void btnSaveCost_Click(object sender, RoutedEventArgs e)
        {
            double cost = 0;
            try
            {
                cost = Convert.ToDouble(txtCost.Text.ToString());
                DBController.SaveCost(cost, (cmbCharacteristics.SelectedItem as Characteristics).Id);
                LoadCharacteristics();
            }
            catch (Exception)
            {
            }

        }
    }
}