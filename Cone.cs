
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
            int countPremium = 0;
            int countTopping = ToppingList.Count();
            double dipped = 0;
            foreach (Flavour flavour in FlavourList)
            {
                string flavour_str = Convert.ToString(flavour);
                flavour_str = flavour_str.ToLower();
                if (flavour_str == "durian" || flavour_str == "ueb" || flavour_str == "sea salt")
                {
                    countPremium += 1;
                }
            }
            if (Dipped = true)
            {
                dipped = 2;
            }
            if (Scoops == 1)
            {
                return 4 + dipped + (2 * countPremium) + (1 * countTopping);
            }
            else if (Scoops == 2)
            {
                return 5.5 + dipped + (2 * countPremium) + (1 * countTopping);
            }
            else if (Scoops == 3)
            {
                return 6.5 + dipped + (2 * countPremium) + (1 * countTopping);
            }
            return 0;
        }

        public override string ToString()
        {
            string dippedOutput = "";
            if (Dipped == true)
            {
                dippedOutput = "Yes";
            }
            else
            {
                dippedOutput = "No";
            }
            return "Dipped: " + dippedOutput + "\n" + base.ToString();
        }
    }
}
