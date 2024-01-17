//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh Jia Xuan
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class Cup:IceCream
    {
        public Cup():base() { }
        public Cup(string o , int s, List<Flavour> f, List<Topping> t) : base(o,s,f,t)
        {

        }
        public override double CalculatePrice()
        {
            // checking if got toppings in option
            string[]toppings = {"sprinkles","mochi","sago","oreos"};
            bool contains = false;
            foreach (string t in toppings)
            {
                if (Option.Contains(t))
                {
                    contains = true;
                }
            }
            double price  = 0;
            if (Option == "single")
            {
                price += 4.00;
            }
            else if (Option == "double")
            {
                price += 5.50;
            }
            else if (contains == true)
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
            return base.ToString();
        }
    }
}
