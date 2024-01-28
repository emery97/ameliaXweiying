//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh
// Partner Name : Lee Wei Ying
//==========================================================


using PairAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairAssignment
{
    class Waffle : IceCream
    {
        private string waffleFlavour;

        public string WaffleFlavour
        {
            get { return waffleFlavour; }
            set { waffleFlavour = value; }
        }
        public Waffle() : base() { }

        public Waffle(string option, int scoops, List<Flavour> flavourList, List<Topping> toppingList, string waffleFlavour) : base(option, scoops, flavourList, toppingList)
        {
            WaffleFlavour = waffleFlavour;
        }

        public override double CalculatePrice()
        {
            int premiumCount = 0;
            int toppingCount = 0;
            double premiumWaffle = 0;
            double basePrice = 0;

            foreach (Flavour flavour in FlavourList)
            {
                if (flavour.Premium == true)
                {
                    premiumCount++;
                }
            }

            foreach (Topping topping in ToppingList)
            {
                if (topping.Type.ToLower() != "none") //so that when topping is none, then no add to topping count
                {
                    toppingCount++;
                }
            }

            WaffleFlavour = WaffleFlavour.ToLower();
            if (WaffleFlavour == "red velvet" || WaffleFlavour == "charcoal" || WaffleFlavour == "pandan")
            {
                premiumWaffle = 3.00; //since if premium then need add $3.
                //but if no, then premiumWaffle will just = 0, as can see from the start
            }

            if (Scoops == 1)
            {
                basePrice = 7.00;
            }
            else if (Scoops == 2)
            {
                basePrice = 8.50;
            }
            else if (Scoops == 3)
            {
                basePrice = 9.50;
            }

            double finalPrice = (basePrice) + (premiumCount * 2.00) + (toppingCount * 1.00) + (premiumWaffle);

            return finalPrice;
        }

        public override string ToString()
        {
            return "Waffle Flavour: " + WaffleFlavour + "\n" + base.ToString();
        }
    }
}
