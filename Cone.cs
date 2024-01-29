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
            int toppingCount = 0;
            double dipped = 0;
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

            if (Dipped == true)
            {
                dipped = 2; //so if dipped not true aka no dip, then will just remain as 0 
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

            double finalPrice = (basePrice) + (premiumCount * 2.00) + (toppingCount * 1.00) + (dipped);

            return finalPrice;


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

            return "Dipped?: " + dipOrNot + "\n" + base.ToString();
        }
    }
}
