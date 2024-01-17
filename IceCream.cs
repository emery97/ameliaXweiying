//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh Jia Xuan
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    abstract class IceCream
    {
        // HAVENT CHECK THE LENGTH OF THE 2 LISTS !! ASSOCIATION !!
        private string option;
        private int scoops;
        private List<Flavour> flavours ;
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
            Flavours = f;
            Toppings = t;
        }

        public abstract double CalculatePrice();
        public override string ToString()
        {
            string flavours = "";
            string toppings = "";
            foreach (Flavour f in Flavours)
            {
                flavours += f+ "\n" ;
            }
            foreach (Topping t in Toppings)
            {
                toppings += t + "\n";
            }
            return "Option: " + Option + "\tScoops: " + Scoops + "\nFlavours: \n" + flavours +"Toppings: \n" + toppings; 
        }


    }
}
