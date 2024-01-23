
using PairAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairAssignment
{
    class Cup : IceCream
    {
        public Cup() : base() { }
        public Cup(string option, int scoops, List<Flavour> flavourList, List<Topping> toppingList) : base(option, scoops, flavourList, toppingList)
        {

        }
        public override double CalculatePrice()
        {
            int topping_count = ToppingList.Count();
            int countPremium = 0;
            foreach (Flavour flavour in FlavourList)
            {
                string flavourName = Convert.ToString(flavour).ToLower();
                if (flavourName == "DURIAN" || flavourName == "UBE" || flavourName == "SEA SALT")
                {
                    countPremium += 1;
                }
            }
            if (Scoops == 1)
            {
                return 4 + topping_count + (2 * countPremium);
            }
            else if (Scoops == 2)
            {
                return 5.5 + topping_count + (2 * countPremium);
            }
            else if (Scoops == 3)
            {
                return 6.5 + topping_count + (2 * countPremium) ;
            }
            return 0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
