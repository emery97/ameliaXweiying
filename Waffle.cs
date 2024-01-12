//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh Jia Xuan
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10258645_PRG2Assignment
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
                price = 7.00;
            }
            else if (Option == "double")
            {
                price = 8.50;
            }
            else if(contains == true)
            {
                price += 1;
            }
            else if (WaffleFlavour != "original")
            {
                price += 3;
            }
            else
            {
                price = 9.50;
            }
            return price;
        }
        public override string ToString()
        {
            return base.ToString()+ "\tWaffle flavour: " + WaffleFlavour;
        }
    }
}
