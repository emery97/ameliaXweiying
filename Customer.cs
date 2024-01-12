using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class Customer
    {
        //Attribute
        private string name;
        private int memberid;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory;
        private PointCard rewards;

        //Properties 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Memberid
        {
            get { return memberid; }
            set { memberid = value; }
        }
        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }
        public Order CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; }
        }
        public List<Order> OrderHistory { get; set; } //Do this instead of the normal get set bc if do normal get set means you assuming there is Order items to be added into this list. By doing THIS method which is correct, just means that youre simply creating empty list first then later add Order items to it
        public PointCard Rewards
        {
            get { return rewards; }
            set { rewards = value; }
        }

        //Constructors
        public Customer() //Non parameterized
        {
        }
        public Customer(string n, int id, DateTime d)
        {
            Name = n;
            Memberid = id;
            Dob = d;
            OrderHistory = new List<Order>(); // !*!*!*!*!**! Initialize the list here, because if you initialize it in property, you are possibly wasting memory since every time you run your program, you will keep initializing it even when you do not need to do so. 
                                              //But if you initialze in constructory, you would only initalize it when you need to do so (by using the constructor). 
        }

        //Methods  
        public Order MakeOrder() //return Order object!
        {
            CurrentOrder = new Order(); //Empty first, so can store the new order being made
            orderHistory.Add(CurrentOrder);  //Add this new order to existing orderHistory
            return CurrentOrder;
        }

        public bool IsBirthday()
        {
            if (DateTime.Today.Month == Dob.Month && DateTime.Today.Day == Dob.Day)  //Because if today's day and month = date of birth's day and month, means true, it IS birthday
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Override string to string
        public override string ToString()
        {
            string stringOrderHistory = "";

            if (orderHistory != null) //Check this to prevent error of NullReferenceException, this error occurs when attempt to access a type variable(that hasn't been instantiated) is made
            {
                foreach (Order dataOHfromList in orderHistory) //dataOH stands for data order from list hist
                {
                    stringOrderHistory += dataOHfromList.ToString();
                }
            }
            return $"Name: {Name} \nMemberID: {Memberid} \nDate Of Birth: {Dob.ToString("dd/MM/yy")} \nCurrent Order: {CurrentOrder} \nOrder History: {stringOrderHistory} \nRewards: {Rewards} \n"; //Dob.ToString("dd/MM/yy") put output into eg 12/01/22 instead of 12/01/22 12:00AM etc. And is MM, for month. Not mm bc mm = minutes
        }
    }
}
