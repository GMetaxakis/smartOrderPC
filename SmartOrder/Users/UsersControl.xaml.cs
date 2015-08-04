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
    /// Interaction logic for UsersControl.xaml
    /// </summary>
    public partial class UsersControl : UserControl
    {
        List<int> onlineUsers = new List<int>();
        public UsersControl()
        {
            InitializeComponent();
            updateUsers();
        }
        
        public void setUserState(int user_id, Boolean online)
        {
             this.Dispatcher.Invoke((Action)(() =>
            {
            if (online)
            {
                if (onlineUsers.Contains(user_id))
                    return;
                else 
                    onlineUsers.Add(user_id);
            }
            else
            {
                if (onlineUsers.Contains(user_id))
                    onlineUsers.Remove(user_id);
            }
            updateUsers();
            }));
            /*
            this.Dispatcher.Invoke((Action)(() =>
            {
                List<User> users = DBController.LoadUsers();
                foreach (User u in users)
                {
                    if(u.User_id == user_id)
                        u.Online = online;
                }

                try
                {
                    lbUsers.ItemsSource = null;
                    lbUsers.ItemsSource = users;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }));*/
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateUserInputDialog cuid = new CreateUserInputDialog();
            cuid.ShowDialog();
            updateUsers();
        }

        private void updateUsers()
        {

            List<User> users = DBController.LoadUsers();
            foreach (User u in users)
            {
                if (onlineUsers.Contains(u.User_id))
                    u.Online = true;
                else
                    u.Online = false;
            }
            try
            {
                lbUsers.ItemsSource = null;
                lbUsers.ItemsSource = users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ChangeName(object sender, RoutedEventArgs e)
        {
            string id = getId(sender);
            List<User> users = DBController.LoadUsers();

            ChangeNamePassword cnp = new ChangeNamePassword("Όνομα : ", getName(users, id), id, true);
            cnp.ShowDialog();
            updateUsers();
        }

        private string getName(List<User> users, string id)
        {
            int id_int = Convert.ToInt32(id);
            string rv = "";
            foreach (User u in users)
            {
                if (u.User_id == id_int)
                    rv = u.Name;
            }
            return rv;
        }
        private string getPass(List<User> users, string id)
        {
            int id_int = Convert.ToInt32(id);
            string rv = "";
            foreach (User u in users)
            {
                if (u.User_id == id_int)
                    rv = u.Password;
            }
            return rv;
        }

        
        private void ChangePass(object sender, RoutedEventArgs e)
        {
            string id = getId(sender);
            List<User> users = DBController.LoadUsers();

            ChangeNamePassword cnp = new ChangeNamePassword("Κωδικός : ", getPass(users,id) , id, false);
            cnp.ShowDialog();
            updateUsers();

        }


        private void Delete(object sender, RoutedEventArgs e)
        {
            string id = getId(sender);

            DBController.DeleteUser(id);

            updateUsers();
        }
        private string getId(object sender)
        {
            MenuItem mnu = sender as MenuItem;
            Grid grid = null;
            if (mnu != null)
            {
                grid = ((ContextMenu)mnu.Parent).PlacementTarget as Grid;
            }
            return grid.Tag.ToString();
        }
    }
}
