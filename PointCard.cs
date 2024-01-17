//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh 
// Partner Name : Lee Wei Ying
//==========================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class PointCard
    {
        //Attributes
        private int points;
        private int punchCard;
        private string tier;

        //Properties 
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public int PunchCard  //Rmb to make the PunchCard increase with every ICE CREAM ordered. NOT FOR every order made bc qn said PunchCad, where for every 10 ice creams not where for every 10 orders
        {
            get { return punchCard; }
            set { punchCard = value; }
        }
        public string Tier
        {
            get { return tier; }
            set { tier = value; }
        }

        //Constructors
        public PointCard()
        {
            Points = 0;
            PunchCard = 0;
            Tier = "Ordinary";
        }

        public PointCard(int pt, int pc)
        {
            Points = pt;
            PunchCard = pc;
        }

        //Methods
        public void AddPoints(int totalAmountPaid)  //Not sure if this is correct... or is it must somehow link to the calling of method CalculateTotal() in Order's class
        {
            int addPointsAmt = Convert.ToInt32(Math.Floor(totalAmountPaid * 0.72));
            Points += addPointsAmt;

            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50 && Points < 100)
            {
                Tier = "Sliver";
            }
            else
            {
                Tier = "Ordinary";
            }

        }
        public void RedeemPoints(int redeemPointsAmt)
        {
            if (Tier == "Gold" || Tier == "Sliver") //Only can redeem if gold or sliver member
            {
                Points -= redeemPointsAmt; //Assuming this is a redeem method that will only allow user to redeem either ALL points or NO points. So this is for in case got left over points that user did not need to redeem since their item maybe cheap 
            }
            else
            {
                Console.WriteLine("You are not egligible to redeem points");
            }
        }

        public void Punch() //Call this method after you double check that PunchCard == 11 since assuming that PunchCard is a record in numbers of how many ice cream customer has ordered so far... NOT for every order, but for every ice cream
        {
            if (PunchCard == 11)
            {
                Console.WriteLine("Congratulations! Your 11th ice cream would be free of charge!");
                //^^Then just exclude this in the calling out of CalculateTotal()..
                PunchCard = 0;
            }
            else
            {
                PunchCard++;  //Remember to call this method out everytime ice cream is ordered, if not your PunchCard will not increment
            }
        }

        public override string ToString()
        {
            return $"Points: {Points} \n Punch Card: {PunchCard} \n Tier: {Tier}";
        }
    }
}
