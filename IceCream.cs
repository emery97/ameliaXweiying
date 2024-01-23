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
            string results = String.Format("{0,-10} {1,-10} {2,-40} {3,-40} \n", "Option", "Scoops", "Flavour [Quantity]", "Topping");

            // Concatenate all flavours into a single string
            string flavoursText = "";
            foreach (Flavour flavour in FlavourList)
            {
                if (flavour.Type != null && flavour.Type != "") // Check if the flavour type is not null or empty
                {
                    if (flavoursText != "") // If already added some flavours, add a comma
                    {
                        flavoursText += ", ";
                    }
                    flavoursText += flavour.Type; // Append flavour type
                    flavoursText+= $" [{flavour.Quantity}]";
                }
            }
            if (flavoursText == "") // If no flavour was added
            {
                flavoursText = "None";
            }

            // Similar process for toppings
            string toppingsText = "";
            foreach (Topping topping in ToppingList)
            {
                if (topping.Type != null && topping.Type != "") // Check if the topping type is not null or empty
                {
                    if (toppingsText != "") // If already added some toppings, add a comma
                    {
                        toppingsText += ", ";
                    }
                    toppingsText += topping.Type; // Append topping type
                }
            }
            if (toppingsText == "") // If no topping was added
            {
                toppingsText = "None";
            }

            // Append the combined details to the result string
            results += String.Format("{0,-10} {1,-10} {2,-40} {3,-15}\n", Option, Scoops, flavoursText, toppingsText);
            return results;
        }
    }
}
