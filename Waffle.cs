//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh Jia Xuan
//==========================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class Waffle:IceCream
    {
        private string waffleFlavour;
        public string WaffleFlavour { get; set; }
        public Waffle() : base() { }
        public Waffle(string o,int s,List<Flavour> f, List<Topping> t,string w) : base(o, s, f, t)
        {
            WaffleFlavour = w;
        }
        public override double CalculatePrice()
        {
            // as long as customer choose waffle base price $0
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
                price += 7.00;
            }
            else if (Scoops == 2)
            {
                price += 8.50;
            }
            else if (Scoops == 3)
            {
                price += 9.50;
            }

            return price;
        }
        public override string ToString()
        {
            return base.ToString()+ "Waffle flavour: " + WaffleFlavour + "\n";
        }
    }
}
