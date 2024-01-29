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
                    premiumCount += flavour.Quantity;
                }
            }

            foreach (Topping topping in ToppingList)
            {
                // Check if topping type is not "none" and not an empty string
                if (!string.IsNullOrEmpty(topping.Type) && topping.Type.ToLower() != "none")
                {
                    toppingCount++;
                }
            }



            List<string> premiumWaffleFlavors = new List<string> { "red velvet", "charcoal", "pandan" };

            if (premiumWaffleFlavors.Contains(WaffleFlavour.ToLower()))
            {
                premiumWaffle = 3;
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
