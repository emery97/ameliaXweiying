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
    class Customer
    {
        private string name;
        private int memberId;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory;
        private PointCard rewards;

        public string Name { get; set; }
        public int MemberId { get; set; }
        public DateTime Dob { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard Rewards { get; set; }


        public Customer() { }

        public Customer(string n, int m, DateTime d)
        {
            Name = n;
            MemberId = m;
            Dob = d;
            Rewards = new PointCard();
            OrderHistory = new List<Order>();
        }

        public Order MakeOrder()
        {
            Order currentOrder = new Order();
            return currentOrder;
        }

        public bool IsBirthday()
        {

            DateTime today = DateTime.Now;
            if (today.Day == Dob.Day && today.Month == Dob.Month && today.Year == Dob.Year)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Name: " + Name + "\nMember ID: " + MemberId + "\nOrder: " + CurrentOrder + "\nPoints: " + Rewards.Points + "\nTier: " + Rewards.Tier;
        }

    }
}
