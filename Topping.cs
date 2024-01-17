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
