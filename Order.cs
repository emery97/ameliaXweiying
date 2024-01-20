//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh 
// Partner Name : Lee Wei Ying
//==========================================================


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
        public List<IceCream> IceCreamList { get; set; } 

        //Constructors 
        public Order()
        {
            IceCreamList = new List<IceCream>();
        }

        public Order(int userOrderId, DateTime userTimeReceived) //****Is the datetime for time recieved or time fulfilled???
        {
            Id = userOrderId;
            TimeReceived = userTimeReceived;
            IceCreamList = new List<IceCream>();
        }

        //Methods
        static List<Flavour> MakingFlavoursList(int scoops)
        {
            string[] flavourOptions = { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
            List<Flavour> flavours = new List<Flavour>();
            bool premium = false;
            int quantity = 0;
            string flavourType = "";
            for (int i = 1; i <= scoops; i++)
            {
                while (true)
                {
                    Console.WriteLine("For regular flavours we've got Vanilla / Chocolate / Strawberry options");
                    Console.WriteLine("For premium flavours we've got Durian / Ube / Sea salt options");
                    Console.Write($"Flavour {i} choice: ");
                    flavourType = Console.ReadLine().ToLower();
                    if (!flavourOptions.Contains(flavourType))
                    {
                        Console.WriteLine("Please enter a flavour from the available options.");
                    }
                    else
                    {
                        break;
                    }
                }
                quantity++;
                if (flavourType == "durian" || flavourType == "ube" || flavourType == "sea salt")
                {
                    premium = true;
                }

                Flavour flavour = new Flavour(flavourType, premium, quantity);
                flavours.Add(flavour);
            }
            return flavours;
        }
        static List<Topping> MakingToppingsList()
        {
            List<Topping> toppings = new List<Topping>();
            Console.WriteLine("We've got sprinkles, mochi, sago and oreos topping options ");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("Topping choice? If you don't want to add anymore flavours enter N: ");
                string toppingChoice = Console.ReadLine().ToLower();
                if (toppingChoice == "n")
                {
                    break;
                }
                else
                {
                    Topping topping = new Topping(toppingChoice);
                    toppings.Add(topping);
                }
            }
            return toppings;
        }
        // this is for option 6 , option 1
        public void ModifyIceCream(int iceCreamIndex)
        {
            IceCream iceCream = IceCreamList[iceCreamIndex - 1];

            Console.WriteLine("[1] Ice Cream Option");
            Console.WriteLine("[2] Ice Cream Scoops");
            Console.WriteLine("[3] Ice Cream Flavours");
            Console.WriteLine("[4] Ice Cream Toppings");
            int menuOption = Convert.ToInt32(Console.ReadLine());
            
            // changing option 
            if (menuOption == 1)
            {
                Console.Write("Options to choose from: Cup / Cone / Waffle: ");
                string iceCreamOption = Console.ReadLine().ToLower();
                iceCream.Option = iceCreamOption;
                IceCream newIceCream = null;
                if (iceCreamOption != iceCream.Option)
                {
                    // if the ice cream option is different
                    switch (iceCreamOption)
                    {
                        case "cup":
                            newIceCream = new Cup();
                            break;
                        case "cone":
                            newIceCream = new Cone();
                            break;
                        case "waffle":
                            newIceCream = new Waffle();
                            break;
                        default:
                            Console.WriteLine("Invalid ice cream option.");
                            break;
                    }
                }
                if (newIceCream != null)
                {
                    newIceCream.Option = iceCreamOption;
                    newIceCream.Scoops = iceCream.Scoops;
                    newIceCream.Flavours = iceCream.Flavours;
                    newIceCream.Toppings = iceCream.Toppings;

                    // set iceCream reference to the new ice cream obj
                    iceCream = newIceCream;
                }
                if (iceCreamOption == "cone" && iceCream is Cone cone)
                {
                    Console.Write("Do you want your cone to be dipped Y / N");
                    string dippedChoice = Console.ReadLine().ToLower();
                    if (dippedChoice == "y")
                    {
                        cone.Dipped = true;
                    }
                }
                if (iceCreamOption == "waffle" && iceCream is Waffle waffle)
                {
                    Console.Write("Do you want to change your waffle flavour Y / N ");
                    string waffleFlavourOption = Console.ReadLine().ToLower();
                    if (waffleFlavourOption == "y")
                    {
                        Console.WriteLine("We've got Red Velvet / Charcoal / Pandan options: ");
                        string waffleFlavour = Console.ReadLine().ToLower();
                        waffle.WaffleFlavour = waffleFlavour;
                    }
                }
            }
            // changing ice cream scoops including check for no. of scoops
            else if (menuOption == 2)
            {
                while (true)
                {
                    Console.Write("Please enter new number of scoops: ");
                    int scoops = Convert.ToInt32(Console.ReadLine());
                    if (scoops >= 1 && scoops <= 3)
                    {
                        iceCream.Scoops = scoops;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("please enter an integer from 1 to 3");
                    }
                }
            }
            // changing ice cream flavours
            else if (menuOption == 3)
            {
                int scoops = iceCream.Scoops;
                List<Flavour> flavours = MakingFlavoursList(scoops);
                iceCream.Flavours = flavours;
            }
            else if (menuOption == 4)
            {
                List<Topping> toppings = MakingToppingsList();
                iceCream.Toppings = toppings;
            }
        }

        public void AddIceCream(IceCream userAddIceCream)
        {
            IceCreamList.Add(userAddIceCream);
            timeReceived = DateTime.Now; // update time when ice cream is added
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
            return $"Id: {Id} \nTime Received: {TimeReceived} \nTime Fulfilled: {TimeFulfilled} \nIce Cream List:\n{stringIceCreamList}";
        }
    }
}