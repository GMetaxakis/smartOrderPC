using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder
{
    class Categories
    {
        private int id;
        private string name;

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

        public Categories(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return "Category : " + Id + " " + Name ;
        }
    }
}
