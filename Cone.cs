//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh
// Partner Name : Lee Wei Ying
//==========================================================


using PairAssignment;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace PairAssignment
{
    class Cone : IceCream
    {
        private bool dipped;
        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }
        public Cone() : base() { }
        public Cone(string option, int scoops, List<Flavour> flavourList, List<Topping> toppingList, bool dipped) : base(option, scoops, flavourList, toppingList)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            int premiumCount = 0;
            int toppingCount = ToppingList.Count();
            double dipped = 0;
            foreach (Flavour flavour in FlavourList)
            {
                string flavour_str = Convert.ToString(flavour);
                flavour_str = flavour_str.ToLower();
                if (flavour_str == "durian" || flavour_str == "ube" || flavour_str == "sea salt")
                {
                    premiumCount += 1;
                }
            }
            if (Dipped = true)
            {
                dipped = 2;
            }
            if (Scoops == 1)
            {
                return 4 + dipped + (2 * premiumCount) + (1 * toppingCount);
            }
            else if (Scoops == 2)
            {
                return 5.5 + dipped + (2 * premiumCount) + (1 * toppingCount);
            }
            else if (Scoops == 3)
            {
                return 6.5 + dipped + (2 * premiumCount) + (1 * toppingCount);
            }
            return 0;
        }

        public override string ToString()
        {
            string dipOrNot = "";
            if (Dipped == true)
            {
                dipOrNot = "Dipped";
            }
            else
            {
                dipOrNot = "Not Dipped";
            }

            return "Dipped: " + dipOrNot + "\n" + base.ToString();
        }
    }
}
