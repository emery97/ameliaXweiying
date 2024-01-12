using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class Order
    {
        //Attributes
        private int id;
        private DateTime timeReceived;
        private DateTime timeFulfilled;
        private List<IceCream> iceCreamList;

        //Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public DateTime TimeReceived
        {
            get { return timeReceived; }
            set { timeReceived = value; }
        }
        public DateTime TimeFulfilled
        {
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();//Do this instead of the normal get set bc if do normal get set means you assuming there is Order items to be added into this list. By doing THIS method which is correct, just means that youre simply creating empty list first then later add Order items to it

        //Constructors 
        public Order()
        {
            Id = 0;
            TimeReceived = DateTime.Now;
            TimeFulfilled = DateTime.Now;
            IceCreamList = new List<IceCream>();
        }

        public Order(int userOrderId, DateTime userTimeReceived) //****Is the datetime for time recieved or time fulfilled???
        {
            Id = userOrderId;
            TimeReceived = userTimeReceived;
        }

        //Methods
        public void ModifyIceCream(int modifyIceCreamIndex)  //User will input index of the ice cream in the ice cream list that they wish to change
        {
            if (modifyIceCreamIndex >= 0 && modifyIceCreamIndex < IceCreamList.Count)
            {
                IceCream modifiedIceCream = IceCreamList[modifyIceCreamIndex];
            }
            else
            {
                Console.WriteLine("Index out of range.");
            }
        }

        public void AddIceCream(IceCream userAddIceCream)
        {
            IceCreamList.Add(userAddIceCream);
        }

        public void DeleteIceCream(int deleteIceCreamIndex)
        {
            IceCreamList.Remove(IceCreamList[deleteIceCreamIndex]);
        }

        public double CalculateTotal()
        {
            double totalPrice = 0;

            foreach (IceCream dataIceCream in IceCreamList)
            {
                totalPrice += dataIceCream.CalculatePrice();
            }

            return totalPrice;
        }

        //Override string 
        public override string ToString()
        {
            string stringIceCreamList = "";
            foreach (IceCream dataICFromList in IceCreamList) //Stands for data ice cream from list aka the icecreamlist
            {
                stringIceCreamList += dataICFromList.ToString();
            }
            return $"Id: {Id} \n Time Received: {TimeReceived} \n Time Fulfilled: {TimeFulfilled} + Ice Cream List: {stringIceCreamList}";
        }
    }
}