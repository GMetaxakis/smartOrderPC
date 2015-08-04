using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartOrder
{
    public class Order
    {
        private int order_id;
        private int table_id;
        private int items_id;
        private int user_id;
        private double cost;
        private bool opened;
        private string datetime;
        private List<Characteristics> characteristics;


        #region setters/getters
        public int Order_id
        {
            get { return order_id; }
            set { order_id = value; }
        }

        public int Table_id
        {
            get { return table_id; }
            set { table_id = value; }
        }

        public int Items_id
        {
            get { return items_id; }
            set { items_id = value; }
        }

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public bool Opened
        {
            get { return opened; }
            set { opened = value; }
        }
        public String OpenedState
        {
            get { return Opened ? "Όχι" : "Ναι"; }
        }

        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public string Datetime
        {
            get { return datetime; }
            set { datetime = value; }
        }

        internal List<Characteristics> Characteristics
        {
            get { return characteristics; }
            set { characteristics = value; }
        }

        public string CharacteristicsInfo 
        { 
            get 
            {
                string nameDesc = "";

                foreach (Characteristics c in Characteristics)
                {
                    nameDesc += " " + c.Name;
                }

                return nameDesc;
            } 
        }
        #endregion

        public Order(int order_id, int table_id, int items_id, int user_id, bool opened, string datetime, List<Characteristics> list)
        {
            Order_id = order_id;
            Table_id = table_id;
            Items_id = items_id;
            User_id = user_id;
            Opened = opened;
            Datetime = datetime;
            Characteristics = list;
            Cost = DBController.getItemCost(items_id) + sumCharacteristicsList();
        }

        private double sumCharacteristicsList()
        {
            double extracost = 0;
            foreach (Characteristics ch in characteristics)
            {
                extracost += ch.Cost;
            }
            return extracost;
        }


        public override string ToString()
        {
            return Order_id + " " + User_id + " " + Items_id + " " + " " + Table_id + " " + User_id + " " + Opened + " " + Datetime;
        }

        
    }
}
