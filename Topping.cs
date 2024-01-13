//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh
// Partner Name : Lee Wei Ying
//==========================================================

using System;
using Code;

namespace Code
{
    class Topping
    {
        private string type;
        public string Type { get; set; }

        public Topping() { }
        public Topping(string t)
        {
            Type = t;
        }
        public override string ToString()
        {
            return "Type:" + Type;
        }
    }
}