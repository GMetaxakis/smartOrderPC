using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder
{
    class Items
    {
        private int parent_id;
        private int id;
        private string name;
        private double price;
        
        #region setters/getters
        public int Parent_id
        {
            get { return parent_id; }
            set { parent_id = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        #endregion

        public Items(int id, int parent_id, string name, double price)
        {
            Parent_id = parent_id;
            Id = id;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return "Item : " + Parent_id + " " + Id + " " + Name + " " + Price;
        }
    }
}
