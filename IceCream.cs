//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh
// Partner Name : Lee Wei Ying
//==========================================================

using System;
using Code;

namespace Code
{
    abstract class IceCream
    {
        // HAVENT CHECK THE LENGTH OF THE 2 LISTS !! ASSOCIATION !!
        private string option;
        private int scoops;
        private List<Flavour> flavours;
        private List<Topping> toppings;

        public string Option { get; set; }
        public int Scoops { get; set; }

        public List<Flavour> Flavours
        {
            get { return flavours; }
            set { flavours = value; }
        }
        public List<Topping> Toppings { get; set; }
        public IceCream() { }

        public IceCream(string o, int s, List<Flavour> f, List<Topping> t)
        {
            Option = o;
            Scoops = s;
            List<Flavour> Flavours = new List<Flavour>();
            List<Topping> toppings = new List<Topping>();
        }
        public abstract double CalculatePrice();
        public override string ToString()
        {
            string flavours = "";
            string toppings = "";
            foreach (Flavour f in Flavours)
            {
                flavours += f + "\n";
            }
            foreach (Topping t in Toppings)
            {
                toppings += t + "\n";
            }
            return "Option: " + Option + "\tScoops: " + Scoops + "\nFlavours: " + flavours + "\nToppings: " + toppings;
        }


    }
}