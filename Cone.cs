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
                if (flavour.Premium == true) //see if is premium flavour --> bool
                {
                    premiumCount++;
                }
            }

            if (Dipped = true)
            {
                dipped = 2; //so if dipped not true aka no dip, then will just remain as 0 
            }

            if (Scoops == 1)
            {
                return 4.00 + dipped + (2 * premiumCount) + (1 * toppingCount);  //2 * premium.. bc each premium flavour is $2 per scoop. 1 * Toppngs bc each topping $1
            }
            else if (Scoops == 2)
            {
                return 5.50 + dipped + (2 * premiumCount) + (1 * toppingCount);
            }
            else if (Scoops == 3)
            {
                return 6.50 + dipped + (2 * premiumCount) + (1 * toppingCount);
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

            return "Dipped?: " + dipOrNot + "\n" + base.ToString();
        }
    }
}
