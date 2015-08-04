using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartOrder
{
    public class MyHttpServer : HttpServer
    {
        public MyHttpServer(int port)
            : base(port)
        {
        }

        public event setUserStateHandler setUserState;
        public delegate void setUserStateHandler(int user_id, Boolean online);

        public event updateOrdersHandler updateOrders;
        public delegate void updateOrdersHandler();




        public override void handleGETRequest(HttpProcessor p)
        {
            //here i have to implement basic get functions
            // get categories/products/items
            // get tables

            if (p.http_url.Equals("/"))
            {
                Console.WriteLine("request: {0}", p.http_url);
                p.writeSuccess();
                p.outputStream.WriteLine("<html><body><h1>test server</h1>");
                p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());
                p.outputStream.WriteLine("url : {0}", p.http_url);

                p.outputStream.WriteLine("<form method=post action=/form>");
                p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
                p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
                p.outputStream.WriteLine("</form>");
            }
            else
            {

                string output = "error";
                
                if (p.http_url.Equals("/getCategories"))
                {
                    output = JsonConvert.SerializeObject(DBController.LoadCategories());
                    p.writeSuccess();
                }
                else if (p.http_url.Equals("/getProducts"))
                {
                    output = JsonConvert.SerializeObject(DBController.LoadProducts(0));
                    p.writeSuccess();
                }

                else if (p.http_url.Equals("/getItems"))
                {
                    output = JsonConvert.SerializeObject(DBController.LoadItems());
                    p.writeSuccess();
                }
                else if (p.http_url.Equals("/getCharacteristics"))
                {
                    output = JsonConvert.SerializeObject(DBController.LoadCharacteristics());
                    p.writeSuccess();
                }
                else if (p.http_url.Equals("/getTables"))
                {
                    p.writeSuccess();
                    //fixme with userId
                    string user_id = (string)p.httpHeaders["user_id"];
                    //check here
                    //Console.WriteLine("get tables with userid: " + user_id);
                    output = JsonConvert.SerializeObject(DBController.LoadTables(Convert.ToInt32(user_id)));
                    //Console.WriteLine("and output: " + output);
                    
                }
                else
                {
                    output = "/getCategories\n";
                    output += "/getProducts\n";
                    output += "/getItems\n";
                    output += "/getTables\n";
                    //p.writeFailure();
                }

                p.outputStream.WriteLine(output);

            }



        }


        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
            //here i have to implement basic post functions
            // post user name and pass
            // post new order
            // post close order
            Console.WriteLine("POST request: {0}", p.http_url);

            if (p.http_url.Equals("/postUser"))
            {
                
                string data = inputData.ReadToEnd();
                string username = getUsernameFromArgs(data);
                string password = getPasswordFromArgs(data);
                //Console.WriteLine("username: {0}, password: {1}", username, password);

                p.writeSuccess();
                int user_id = DBController.CheckUser(username, password);
                p.outputStream.WriteLine(user_id);

                if (setUserState != null)
                {
                    setUserState(user_id, true);
                }
            }
            else if (p.http_url.Equals("/postRemoveOrder"))
            {
                string order_id = (string)p.httpHeaders["order_id"];
                DBController.CloseOrder(order_id);
                p.writeSuccess();
                if(updateOrders!=null)
                {
                    updateOrders();
                }
            }
            else if (p.http_url.Equals("/postOrder"))
            {
                /*
                 *  add order
                 *  then add characteristics for that order  
                 */
                string table_id = (string)p.httpHeaders["table_id"];
                string item_id = (string)p.httpHeaders["item_id"];
                string user_id = (string)p.httpHeaders["user_id"];
                int order_id = DBController.AddOrder(Convert.ToInt32(table_id), Convert.ToInt32(item_id), Convert.ToInt32(user_id));
                int characteristicsCount = Convert.ToInt32((string)p.httpHeaders["characteristicsCount"]);
                List<Characteristics> characteristics = new List<Characteristics>();
                List<Characteristics> allCharacteristics = DBController.LoadCharacteristics();
                for (int i = 0; i < characteristicsCount; i++)
                {
                    int characteristic_id = Convert.ToInt32((string)p.httpHeaders["characteristic" + i]);
                    foreach (Characteristics ch in allCharacteristics)
                    {
                        if(characteristic_id== ch.Id)
                        {
                            characteristics.Add( new Characteristics(ch.Id,ch.Name));
                            break;
                        }
                    }
                    DBController.AddCharacteristicsInOrder(order_id, characteristic_id);
                }
                p.writeSuccess();
                
                //print here

                //Printer.printOrder(new Order(order_id, Convert.ToInt32(table_id), Convert.ToInt32(item_id), Convert.ToInt32(user_id), true, "", characteristics));

                if (updateOrders != null)
                {
                    updateOrders();
                }

            }
            else if (p.http_url.Equals("/postUserOffline"))
            {
                string user_id = (string)p.httpHeaders["user_id"];
                p.writeSuccess();
                if (setUserState!= null)
                {
                    setUserState(Convert.ToInt32(user_id), false);
                }
            }
            else
            {

            }
            

        }

        private string getArg(string data, string argName)
        {
            string rv = "null";
            argName += "=";
            int i = data.IndexOf(argName);
            int startIndex = i + argName.Length;
            if (i != -1)
            {
                int k = data.IndexOf('&', i);
                if (k == -1)
                //    k = data.IndexOf('&');
                //if (k == -1)
                    k = data.Length;
                rv = data.Substring(startIndex, k - startIndex);
            }

            return rv;
        }
       
        private string getUsernameFromArgs(string data)
        {
            return getArg(data, "username");
        }

        private string getPasswordFromArgs(string data)
        {
            return getArg(data, "password");
        }
    }
}
