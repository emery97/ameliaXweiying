//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh Jia Xuan
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace S10258645_PRG2Assignment
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
            // checking if got toppings in option
            string[] toppings = { "sprinkles", "mochi", "sago", "oreos" };
            bool contains = false;
            foreach (string t in toppings)
            {
                if (Option.Contains(t))
                {
                    contains = true;
                }
            }
            double price = 0;
            if (Option == "single")
            {
                price += 4.00;
            }
            else if (Option == "double")
            {
                price += 5.50;
            }
            else if(Option.Contains("chocolate-dipped cone"))
            {
                price += 2;
            }
            else if (contains == true) // checking if got toppings
            {
                price += 1;
            }
            else
            {
                price += 6.50;
            }
            return price;
        }
        public override string ToString()
        {
            return base.ToString() + "\tDipped: " + Dipped;
        }
        
    }
}
