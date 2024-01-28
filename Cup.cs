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
            int toppingCount = 0;
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

            if (Scoops == 1)
            {
                basePrice = 4.00;
            }
            else if (Scoops == 2)
            {
                basePrice = 5.50;
            }
            else if (Scoops == 3)
            {
                basePrice = 6.50;
            }

            double finalPrice = (basePrice) + (premiumCount * 2.00) + (toppingCount * 1.00);

            return finalPrice;
         

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
