using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder
{
    /// <summary>
    /// mia voithitiki klasi gia tin epikoinonia me tin basi
    /// </summary>
    static class DBController
    {
        static SQLiteConnection m_dbConnection;

        /// <summary>
        /// sinartisi pou diabazei apo tin basi olous tous xristes
        /// </summary>
        public static List<User> LoadUsers()
        {
            List<User> users = new List<User>();

            DBOpenClose(() =>
            {
                string sql2 = "select * from users order by user_id asc";
                SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);

                SQLiteDataReader reader = command2.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        users.Add(new User(Convert.ToInt32(reader["user_id"]), reader["name"].ToString(), reader["password"].ToString()));
                        //Console.WriteLine("user_id: " + reader["user_id"] + "\torder_id: " + reader["order_id"] + "\ttype_id: " + reader["type_id"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            return users;
        }

        /// <summary>
        /// sinartisi pou fortonei apo tin basi ola ta orders
        /// </summary>
        public static List<Order> LoadOrders()
        {
            List<Order> orders = new List<Order>();
            DBOpenClose(() =>
            {
                string sql = "select * from orders";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        int order_id = reader.GetInt32(0);

                        Order o = new Order(order_id, reader.GetInt32(1), reader.GetInt32(2),
                                            reader.GetInt32(3), reader.GetBoolean(4), reader.GetString(5), LoadCharacteristicsInOrder(order_id));
                        orders.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LoadOrders " + ex.Message);
                }

            });
            return orders;
        }
        
        /// <summary>
        /// sinartisi pou fortonei apo tin basi ola ta characteristics
        /// </summary>
        public static List<Characteristics> LoadCharacteristics()
        {
            List<Characteristics> characteristics = new List<Characteristics>();
            DBOpenClose(() =>
            {
                string sql = "select * from characteristics";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Characteristics characteristic = new Characteristics(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2));
                        characteristics.Add(characteristic);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LoadCharacteristics " + ex.Message);
                }

            });
            return characteristics;
        }

        /// <summary>
        /// sinartisi pou fortonei ola ta characteristics gia ena sigkekrimeno order id
        /// </summary>
        public static List<Characteristics> LoadCharacteristicsInOrder(int order_id)
        {
            List<Characteristics> characteristics = LoadCharacteristics();
            List<Characteristics> characteristicsInOrder = new List<Characteristics>();
            DBOpenClose(() =>
            {
                string sql = "select characteristics_id from characteristicsInOrders where order_id ='" + order_id + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        int characteristics_id = reader.GetInt32(0);

                        foreach (Characteristics ch in characteristics) 
                        { 
                            if (ch.Id == characteristics_id)
                                characteristicsInOrder.Add(ch); 
                        }                    
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LoadCharacteristicsInOrder " + ex.Message);
                }

            });
            return characteristicsInOrder;
        }

        /// <summary>
        /// sinartisi pou fortonei ola ta trapezia apo ton pinaka orders gia ton sigkekrimeno user
        /// </summary>
        internal static List<MyTable> LoadTables(int user_id)
        {
            List<MyTable> tables = new List<MyTable>();
            DBOpenClose(() =>
            {
                string sql = "select table_id from orders group by table_id";
                if (user_id != -1)
                    sql = "select table_id from orders where user_id='" + user_id + "' group by table_id";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        tables.Add(new MyTable(reader.GetInt32(0)));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LoadTables " + ex.Message);
                }

            });
            List<Order> orders = LoadOrders();

            foreach (Order o in orders)
            {
                foreach (MyTable t in tables)
                {
                    if (o.Table_id == t.Table_id)
                        t.AddOrderInTable(o);
                }
            }

            return tables;
        }

        /// <summary>
        /// sinartisi pou fortonei ola ta trapezia gia olous tous xristes
        /// </summary>
        /// <returns></returns>
        internal static List<MyTable> LoadTables()
        {
            return LoadTables(-1);
        }


        internal static List<Items> LoadItems()
        {
            List<Items> items = new List<Items>();
            DBOpenClose(() =>
            {
                //sql = "CREATE TABLE items (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, parent_id INTEGER, name VARCHAR(30), price DOUBLE)";
                string sql = "select * from items";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        items.Add(new Items(reader.GetInt32(0),reader.GetInt32(1),reader.GetString(2),reader.GetDouble(3)));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LoadTables " + ex.Message);
                }

            });
            return items;
        }


        internal static void AddUser(string name, string password)
        {
            DBOpenClose(() =>
            {
                string sql = "insert into users (name, password) values ('" + name + "', '" + password + "')";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void ChangeUserName(string id, string name)
        {
            DBOpenClose(() =>
            {
                string sql = "update users set name = '" + name + "' where user_id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void ChangeUserPass(string id, string password)
        {
            DBOpenClose(() =>
            {
                string sql = "update users set password = '" + password + "' where user_id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void DeleteUser(string id)
        {
            DBOpenClose(() =>
            {
                string sql = "delete from users where user_id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static double getItemCost(int items_id)
        {
            double cost = 0;
            DBOpenClose(() =>
            {
                string sql = "select price from items where id ='" + items_id + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        cost = reader.GetDouble(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("getItemCost " + ex.Message);
                }

            });
            return cost;
        }



        /// <summary>
        /// returns order_id
        /// </summary>
        /// <param name="table_id"></param>
        /// <param name="items_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        internal static int AddOrder(int table_id, int items_id, int user_id)
        {
            int order_id = -1;
            DBOpenClose(() =>
            {
                //sql = "insert into orders (table_id , items_id , user_id , opened) values (1, 1, 2, 1)";
                string sql = "insert into orders (table_id , items_id , user_id , opened) values ('" + table_id + "', '" + items_id + "', '" + user_id + "', '1')";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                sql = "select order_id from orders order by timestamp ASC";
                command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        order_id = reader.GetInt32(0);
                        //Console.WriteLine(order_id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LoadOrders " + ex.Message);
                }
            });

            return order_id;
        }


        internal static void AddCharacteristicsInOrder(int order_id, int characteristics_id)
        {
            DBOpenClose(() =>
            {
                //sql = "insert into characteristicsInOrders (order_id, characteristics_id) values (1,1)";
                string sql = "insert into characteristicsInOrders (order_id , characteristics_id ) values ('" + order_id + "', '" + characteristics_id + "')";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

            });
        }


        internal static void DeleteOrder(string id)
        {
            DBOpenClose(() =>
            {
                string sql = "delete from orders where order_id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }


        internal static void DeleteAllOrders()
        {
            DBOpenClose(() =>
            {
                string sql = "delete from orders";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }


        internal static void CloseOrder(string id)
        {
            DBOpenClose(() =>
            {
                string sql = "update orders set opened = '0' where order_id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static List<Categories> LoadCategories()
        {
            List<Categories> categories = new List<Categories>();

            DBOpenClose(() =>
            {
                string sql = "select * from categories";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        categories.Add(new Categories(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            return categories;
        }

        internal static List<Products> LoadProducts(int category_id)
        {
            List<Products> products = new List<Products>();

            DBOpenClose(() =>
            {
                string sql = "select * from products";
                if (category_id != 0)
                    sql += " where parent_id ='" + category_id + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        products.Add(new Products(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2)));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            return products;
        }

        internal static List<Items> LoadItems(int product_id)
        {
            List<Items> items = new List<Items>();

            DBOpenClose(() =>
            {
                string sql = "select * from items";
                if (product_id != 0)
                    sql += " where parent_id ='" + product_id + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        int parent_id = reader.GetInt32(0);
                        int id = reader.GetInt32(1);
                        string name = reader.GetString(2);
                        double price = reader.GetDouble(3);
                        items.Add(new Items(parent_id, id, name, price));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            return items;
        }

        internal static void SavePrice(double cost, int item_id)
        {
            DBOpenClose(() =>
            {
                string sql = "update items set price = " + cost.ToString(CultureInfo.InvariantCulture) + " where id = " + item_id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }
        
        internal static void AddCategory(string category_name)
        {
            DBOpenClose(() =>
            {
                string sql = "insert into categories (name) values ('" + category_name + "')";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void DeleteCategory(int id)
        {
            DBOpenClose(() =>
            {
                string sql = "delete from categories where id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });

        }


        internal static void AddProduct(string product_name, int parent_id)
        {
            DBOpenClose(() =>
            {
                //string sql = "insert into users (name, password) values ('" + name + "', '" + password + "')";
                string sql = "insert into products (name,parent_id) values ('" + product_name+"', '" + parent_id + "')";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void DeleteProduct(int id)
        {
            DBOpenClose(() =>
            {
                string sql = "delete from products where id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void AddItem(Items item)
        {
            DBOpenClose(() =>
            {
                //string sql = "insert into users (name, password) values ('" + name + "', '" + password + "')";
                string sql = "insert into items (name,parent_id,price) values ('" + item.Name + "', '" + item.Parent_id + "', " + item.Price.ToString(CultureInfo.InvariantCulture) + ")";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void DeleteItem(int id)
        {
            DBOpenClose(() =>
            {
                string sql = "delete from items where id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void SaveCost(double cost, int item_id)
        {
            DBOpenClose(() =>
            {
                string sql = "update characteristics set cost = " + cost.ToString(CultureInfo.InvariantCulture) + " where id = " + item_id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void AddCharacteristics(Characteristics characteristic)
        {
            DBOpenClose(() =>
            {
                //string sql = "insert into users (name, password) values ('" + name + "', '" + password + "')";
                string sql = "insert into characteristics (name,cost) values ('" + characteristic.Name + "', " + characteristic.Cost.ToString(CultureInfo.InvariantCulture) + ")";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }

        internal static void DeleteCharacteristics(int id)
        {
            DBOpenClose(() =>
            {
                string sql = "delete from characteristics where id = " + id;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            });
        }



        internal static int CheckUser(string username, string password)
        {
            int user_id = -1;

            DBOpenClose(() =>
            {
                string sql2 = "select user_id from users where name='" + username + "' AND  password='" + password + "'";
                SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);

                SQLiteDataReader reader = command2.ExecuteReader();
                try
                {
                    reader.Read();
                    user_id = reader.GetInt32(0);
                    Console.WriteLine("user found with user_id = " + user_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                

            });
            return user_id;
        }

        delegate void ActionHandler();

        static void DBOpenClose(ActionHandler action)
        {
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            action();

            m_dbConnection.Close();
        }

    }
}
