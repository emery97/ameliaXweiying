//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh
// Partner Name : Lee Wei Ying
//==========================================================

using System;
using Code;

namespace Code
{
    class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;

        public string Type { get; set; }
        public bool Premium { get; set; }
        public int Quantity { get; set; }

        public Flavour() { }
        public Flavour(string t, bool p, int q)
        {
            Type = t;
            Premium = p;
            Quantity = q;
        }
        public override string ToString()
        {
            return "Type: " + Type + "\tPremium: " + Premium + "Quantity: " + Quantity;
        }
    }
}