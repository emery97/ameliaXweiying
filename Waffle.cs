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
            int toppingCount = ToppingList.Count();
            double premiumWaffle = 0;

            foreach (Flavour flavour in FlavourList)
            {
                if (flavour.Premium == true)
                {
                    premiumCount++;
                }
            }

            WaffleFlavour = WaffleFlavour.ToLower();
            if (WaffleFlavour == "red velvet" || WaffleFlavour == "charcoal" || WaffleFlavour == "pandan")
            {
                premiumWaffle = 3; //since if premium then need add $3.
                //but if no, then premiumWaffle will just = 0, as can see from the start
            }

            if (Scoops == 1)
            {
                return 7.00 + premiumWaffle + ( 1 * toppingCount ) + (2 * premiumCount);
            }
            else if (Scoops == 2)
            {
                return 8.50 + premiumWaffle + (1 * toppingCount) + (2 * premiumCount);
            }
            else if (Scoops == 3)
            {
                return 9.50 + premiumWaffle + (1 * toppingCount) + (2 * premiumCount);
            }

            return 0;
        }

        public override string ToString()
        {
            return "Waffle Flavour: " + WaffleFlavour + "\n" + base.ToString();
        }
    }
}
