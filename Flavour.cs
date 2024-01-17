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
using System.Web;

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
            return "Type: " + Type + "\tPremium: " + Premium + "\tQuantity: " + Quantity;
        }
    }
}
