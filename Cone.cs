//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh Jia Xuan
//==========================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class Cone:IceCream
    {
        private bool dipped;
        public bool Dipped
        {
            get;set;
        }
        public Cone():base() { }
        public Cone(string o, int s, List<Flavour> f, List<Topping> t, bool d) : base(o,s,f,t)
        {
            Dipped = d;
        }
        public override double CalculatePrice()
        {
            double price = 0;
            Dictionary<string, int> flavourDict = new Dictionary<string, int>();
            Dictionary<string, int> toppingDict = new Dictionary<string, int>();

            // looking through toppings.csv
            using (StreamReader sr = new StreamReader("toppings.csv"))
            {
                string s = sr.ReadLine(); //header
                string[] header = s.Split(',');
                while ((s = sr.ReadLine()) != null)
                {
                    string[] data = s.Split(',');
                    toppingDict.Add(data[0], Convert.ToInt32(data[1]));
                }
            }
            // looking through flavours.csv
            using (StreamReader sr = new StreamReader("flavours.csv"))
            {
                string s = sr.ReadLine(); //header
                string[] header = s.Split(',');
                while ((s = sr.ReadLine()) != null)
                {
                    string[] data = s.Split(',');
                    toppingDict.Add(data[0], Convert.ToInt32(data[1]));
                }
            }
            // checking if got toppings in toppingsDict
            foreach (Topping t in Toppings)
            {
                foreach (KeyValuePair<string, int> kvp in toppingDict)
                {
                    if (kvp.Key == t.Type)
                    {
                        price += kvp.Value;
                    }
                }
            }
            // checking if got flavours in flavoursDict
            foreach (Flavour f in Flavours)
            {
                foreach (KeyValuePair<string, int> kvp in flavourDict)
                {
                    if (kvp.Key == f.Type)
                    {
                        price += kvp.Value;
                    }
                }
            }
            if (Scoops == 1)
            {
                price += 4.00;
            }
            else if (Scoops == 2)
            {
                price += 5.50;
            }
            else if (Scoops == 3)
            {
                price += 6.50;
            }
            else if (Dipped == true)
            {
                price += 2;
            }
            return price;
        }
        public override string ToString()
        {
            return base.ToString() + "Dipped: " + Dipped+"\n";
        }
        
    }
}
