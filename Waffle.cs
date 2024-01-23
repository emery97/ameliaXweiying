
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
            int countPremium = 0;
            int countTopping = ToppingList.Count();
            double premiumWaffle = 0;

            foreach (Flavour flavour in FlavourList)
            {
                string flavour_str = Convert.ToString(flavour).ToLower();
                if (flavour_str == "durian" || flavour_str == "ube" || flavour_str == "sea salt")
                {
                    countPremium += 1;
                }
            }
            WaffleFlavour = WaffleFlavour.ToLower();
            if (WaffleFlavour == "red velvet" || WaffleFlavour == "charcoal" || WaffleFlavour == "pandan")
            {
                premiumWaffle = 3;
            }

            if (Scoops == 1)
            {
                return 7 + premiumWaffle + countTopping + (2 * countPremium);
            }
            else if (Scoops == 2)
            {
                return 8.5 + premiumWaffle + countTopping+ (2 * countPremium);
            }
            else if (Scoops == 3)
            {
                return 9.5 + premiumWaffle + countTopping + (2 * countPremium);
            }
            return 0;
        }

        public override string ToString()
        {
            return "Waffle Flavour: " + WaffleFlavour + "\n" + base.ToString();
        }
    }
}
