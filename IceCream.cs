//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh
//==========================================================

using PairAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairAssignment
{
    abstract class IceCream
    {
        private string option;
        private int scoops;
        private List<Flavour> flavourList;
        private List<Topping> toppingList;

        public abstract double CalculatePrice();
        public string Option { get; set; }
        public int Scoops { get; set; }
        public List<Flavour> FlavourList { get; set; } = new List<Flavour>();
        public List<Topping> ToppingList { get; set; } = new List<Topping>();

        public IceCream() { }
        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            FlavourList = flavours;
            ToppingList = toppings;
        }

        //Add in calculate price constructor inside hereeeee 


        public override string ToString()
        {
            string results = String.Format("{0,-10} {1,-10} {2,-45} {3,-40} \n", "Option", "Scoops", "Flavour [Quantity]", "Topping");


            string flavoursText = "";
            foreach (Flavour flavour in FlavourList)
            {
                if (flavour.Type != null && flavour.Type != "")
                {
                    if (flavoursText != "") // if not empty
                    {
                        flavoursText += ", ";
                    }
                    flavoursText += flavour.Type; // Append flavour type
                    flavoursText += $" [{flavour.Quantity}]";
                }
            }
            if (flavoursText == "")
            {
                flavoursText = "None"; //When no toppings, this wil print!
            }


            string toppingsText = "";
            foreach (Topping topping in ToppingList)
            {
                if (topping.Type != null && topping.Type != "")
                {
                    if (toppingsText != "")
                    {
                        toppingsText += ", ";
                    }
                    toppingsText += topping.Type;
                }
            }
            if (toppingsText == "")
            {
                toppingsText = "None";
            }


            results += String.Format("{0,-10} {1,-10} {2,-45} {3,-25}\n", Option, Scoops, flavoursText, toppingsText);

            return results;
        }
    }
}
