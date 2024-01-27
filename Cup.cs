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
    class Cup : IceCream
    {
        public Cup() : base() { }
        public Cup(string option, int scoops, List<Flavour> flavourList, List<Topping> toppingList) : base(option, scoops, flavourList, toppingList)
        {

        }
        public override double CalculatePrice()
        {
            int premiumCount = 0;
            int toppingCount = ToppingList.Count();

            foreach (Flavour flavour in FlavourList)
            {
                if (flavour.Premium == true)
                {
                    premiumCount++;
                }
            }

            if (Scoops == 1)
            {
                return 4.00 + (1 * toppingCount) + (2 * premiumCount);
            }
            else if (Scoops == 2)
            {
                return 5.50 + (1 * toppingCount) + (2 * premiumCount);
            }
            else if (Scoops == 3)
            {
                return 6.50 + (1 * toppingCount) + (2 * premiumCount);
            }
            return 0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
