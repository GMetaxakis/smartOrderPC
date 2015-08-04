using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder
{
    class Products
    {
        private int parent_id;
        private int id;
        private string name;

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

        public Products(int id, int parent_id, string name)
        {
            Parent_id = parent_id;
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return "Product : " + Parent_id + " " + Id + " " + Name;
        }
    }
}
