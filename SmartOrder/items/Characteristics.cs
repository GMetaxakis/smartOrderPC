using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder
{
    public class Characteristics
    {
        private int id;
        private string name;
        private double cost;
        private Boolean selected;

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

        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public Boolean Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public Characteristics(int id, string name, double cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }

        public Characteristics(int id, string name)
        {
            Id = id;
            Name = name;
            Cost = 0;
        }

        public override string ToString()
        {
            return "Characteristic : " + Id + " " + Name + " " + Cost;
        }
    }
}
