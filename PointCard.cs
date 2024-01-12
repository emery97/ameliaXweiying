using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10258645_PRG2Assignment
{
    class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;

        public int Points
        {
            get;set;
        }
        public int PunchCard
        {
            get;set;
        }
        public string Tier
        {
            get;set;
        }

        public PointCard() { }
        // using Points and PunchCard
        public PointCard(int p, int pc)
        {
            Points = p;
            PunchCard = pc;
        }
        // add points to punchcard
        public void AddPoints(int addPoint)
        {
            Points += addPoint;
        }
        public void RedeemPoints(int pointRedeem)
        {
            double moneyMinus = 0.02 * pointRedeem;
        }
        // PunchCard point system
        public void Punch()
        {
            if (punchCard == 10)
            {
                PunchCard = 0;
            }
            else
            {
                PunchCard++;
            }
        }
    }
}
