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
    class PointCard
    {
        //Attributes
        private int points;
        private int punchCard;
        private string tier;

        //Properties
        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        //Constructors
        public PointCard()
        {
            Points = 0;
            PunchCard = 0;
            Tier = "Ordinary"; //bc in basic 3, will assign pointcard() to new customer and this is the values of the points, punchcard etc bc they are NEW
        }

        public PointCard(int p, int pc)
        {
            Points = p;
            PunchCard = pc;

            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50 && Points < 100)
            {
                Tier = "Silver";
            }
            else
            {
                Tier = "Ordinary";
            }
        }

        //Methods
        public void AddPoints(int totalAmt) //totalAmt is the final amount paid by customer for the order since points r earned from those
        {
            int pointsToAdd = Convert.ToInt32(Math.Floor(totalAmt * 0.72));
            Points += pointsToAdd;

            if (Points >= 100 || Tier == "Gold") // Update their tier since points increase ++ Have the '|| Tier == "Gold"' is so that if your tier = gold, our tier is forveer = gold since you cant drop back down.
            {
                Tier = "Gold";
            }
            else if ((Points >= 50 && Points < 100) || Tier == "Sliver")
            {
                Tier = "Silver"; // Corrected from 'Sliver' to 'Silver'
            }
            else
            {
                Tier = "Ordinary";
            }
        }

        public void Punch()
        {
            /*
            PunchCard++; // Increase for every ice cream ordered
            if (PunchCard >= 10) 
            {
                PunchCard = 0; // Set to 0 again. 
            }
            */



            bool isPunchCardComplete = PunchCard >= 10;
            if (isPunchCardComplete)
            {
                // Do not increment punch card because it's complete
                // Redemption should be asked before calling this method
                // set PunchCard to 0 in Program when user opt to redeem the foc ice cream
                return;
            }
            PunchCard++; // Increment the punch card count

        }

        public void RedeemPoints(int reedemedPoints)
        {
            if (Tier != "Ordinary" && Points >= reedemedPoints) // only can reedeem if they NOT ordinary tier. and the points they want to redeem must be realistic aka they umust have enough points to redeem

            {
                Points -= reedemedPoints;  // Will ask for amt of points they want redeem in main prg. 
            }

        }

        public override string ToString()
        {
            return "Points:" + Points + "PunchCard:" + PunchCard + "Tier:" + Tier;
        }



    }
}