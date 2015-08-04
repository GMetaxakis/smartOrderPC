using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartOrder
{
    public class MyTable : IEquatable<MyTable>
    {
        private int table_id;
        private List<Order> orders;

        #region setters/getters
        public int Table_id
        {
            get { return table_id; }
            set { table_id = value; }
        }

        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public MyTable(int table_id)
        {
            Table_id = table_id;
            orders = new List<Order>();
        }
        #endregion

        public void AddOrderInTable(Order order)
        {
            orders.Add(order);
        }

        public bool Equals(MyTable other)
        {
            return this.table_id == other.table_id;
        }
    }
}
