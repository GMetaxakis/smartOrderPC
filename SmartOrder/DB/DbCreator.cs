using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder
{
    public class DbCreator
    {
        SQLiteConnection m_dbConnection;
        public void init()
        {
            try
            {
                //create db
                //createDBFile();

                //create tables
                //createTables();

                //add some data
                //addsampleData();

                //show data
                //showData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        private void createDBFile()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
            m_dbConnection.Close();
        }


        private void createTables()
        {
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql;
            SQLiteCommand command;
            /*
             CREATE TABLE my_table (
                    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    name VARCHAR(64),
                    sqltime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
             );
             
             INSERT INTO my_table(name) VALUES('test3');
             
             */
            sql = "CREATE TABLE users (user_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name VARCHAR(30), password VARCHAR(6))";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE orders (order_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, table_id INT," +
                "items_id INT, user_id INT, opened BOOLEAN, timestamp TIMESTAMP DEFAULT (datetime('now','localtime')) NOT NULL)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE categories (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name VARCHAR(30))";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE products (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, parent_id INTEGER, name VARCHAR(30))";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE items (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, parent_id INTEGER, name VARCHAR(30), price DOUBLE)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE characteristics (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name VARCHAR(30), cost DOUBLE)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE characteristicsInOrders (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, order_id INT, characteristics_id INT)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        private void addsampleData()
        {
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql;
            SQLiteCommand command;

            #region insert users
            sql = "insert into users (name, password) values ('admin', 'admin')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into users (name, password) values ('waiter', 'waiter')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into users (name, password) values ('barman', 'barman')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            #endregion

            //TODO FIX ORDERS
            #region insert orders
            sql = "insert into orders (table_id , items_id , user_id , opened) values (1, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (1, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (1, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into orders (table_id , items_id , user_id , opened) values (2, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (2, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (2, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (2, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into orders (table_id , items_id , user_id , opened) values (3, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (3, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (3, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (3, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into orders (table_id , items_id , user_id , opened) values (4, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (4, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (4, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (4, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (4, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (4, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into orders (table_id , items_id , user_id , opened) values (5, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (5, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (5, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (5, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (5, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into orders (table_id , items_id , user_id , opened) values (5, 1, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            #endregion

            #region insert categories
            sql = "insert into categories (name) values ('αλκοολούχα')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into categories (name) values ('τρόφιμα')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into categories (name) values ('μη αλκοολούχα')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            #endregion

            #region insert products
            sql = "insert into products (parent_id,name) values (3,'καφές')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into products (parent_id,name) values (3,'σοκολάτα')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into products (parent_id,name) values (3,'χυμός')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            #endregion

            #region insert items
            sql = "insert into items (parent_id,name,price) values (1,'espresso',1.5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into items (parent_id,name,price) values (1,'φραπέ',2.5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into items (parent_id,name,price) values (1,'ελληνικό',1.0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            #endregion

            #region insert characteristics
            sql = "insert into characteristics (name,cost) values ('σκέτος',0.0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into characteristics (name,cost) values ('μέτριος',0.0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into characteristics (name,cost) values ('γλυκός',0.0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into characteristics (name,cost) values ('παγωτό',0.5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            #endregion

            #region insert characteristicsInOrders
            sql = "insert into characteristicsInOrders (order_id, characteristics_id) values (1,1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into characteristicsInOrders (order_id,characteristics_id) values (1,2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into characteristicsInOrders (order_id,characteristics_id) values (2,1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into characteristicsInOrders (order_id,characteristics_id) values (3,4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            #endregion

            m_dbConnection.Close();
        }

        private void showData()
        {
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql2 = "select * from users order by user_id asc";
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);

            SQLiteDataReader reader = command2.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("user_id: " + reader["user_id"] + "\tName: " + reader["name"] + "\tpassword: " + reader["password"]);


            sql2 = "select * from categories";
            command2 = new SQLiteCommand(sql2, m_dbConnection);

            reader = command2.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("id: " + reader["id"] + "\tname: " + reader["name"]);
            }

            //sql = "CREATE TABLE categories (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name VARCHAR(30))";

            sql2 = "select * from products";
            command2 = new SQLiteCommand(sql2, m_dbConnection);

            reader = command2.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("id: " + reader["id"] + "\tparent_id: " + reader["parent_id"] + "\tname: " + reader["name"]);
            }
            //sql = "CREATE TABLE products (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, parent_id INTEGER, name VARCHAR(30))";

            sql2 = "select * from items";
            command2 = new SQLiteCommand(sql2, m_dbConnection);

            reader = command2.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("id: " + reader["id"] + "\tparent_id: " + reader["parent_id"] + "\tname: " + reader["name"] + "\tprice: " + reader["price"]);
            }
            //sql = "CREATE TABLE items (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, parent_id INTEGER, name VARCHAR(30)), price DOUBLE";

            sql2 = "select * from characteristics";
            command2 = new SQLiteCommand(sql2, m_dbConnection);

            reader = command2.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("id: " + reader["id"] + "\tname: " + reader["name"] + "\tcost: " + reader["cost"]);
            }
            //sql = "CREATE TABLE characteristics (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name VARCHAR(30)), cost DOUBLE";

            sql2 = "select * from characteristicsInOrders";
            command2 = new SQLiteCommand(sql2, m_dbConnection);

            reader = command2.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("id: " + reader["id"] + "\torder_id: " + reader["order_id"] + "\tcharacteristics_id: " + reader["characteristics_id"]);
            }
            //sql = "CREATE TABLE characteristicsInOrders (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, order_id INT, characteristics_id INT)";

            m_dbConnection.Close();

        }

    }
}
