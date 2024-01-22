//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh 
// Partner Name : Lee Wei Ying
//==========================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        public List<Order> OrderHistory { get; set; }
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
            Rewards = new PointCard();
            //CurrentOrder = new Order();
        }

        //Methods  

        // making new flavoursList
        private static List<Flavour> MakingFlavoursList(int scoops) //own method to use later
        {
            List<string> premiumFlavour = new List<string>();
            // looking through flavours.csv
            using (StreamReader sr = new StreamReader("flavours.csv"))
            {
                string s = sr.ReadLine(); //header
                string[] header = s.Split(',');
                while ((s = sr.ReadLine()) != null)
                {
                    string[] data = s.Split(',');
                    if (Convert.ToInt32(data[1]) != 0)
                    {
                        premiumFlavour.Add(data[0].ToLower());
                    }
                }
            }

            //key = flavour, value = quantity
            Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();
            string[] flavourOptions = { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" }; //This is an array. Do this so later can check if user's chosen flavour is indeed one of the flavour options in our menu. Aka this part of the code for validation (!flavourOptions.Contains(flavourType))
            //Initializing
            List<Flavour> flavours = new List<Flavour>(); //So that can store all flavours user choose
            bool premium = false;
            string flavourType = "";
            for (int i = 1; i <= scoops; i++) //for loop is for asking for flavours
            {
                while (true) // this while loop is for validation. bc if user enters a flavour type that dont exist, then prg will ask them to "Please enter a flavour from the available options." ---> then will while loop again to ask them the flvours. Finally when they enter a flavour that exist, then will break from this while loop and go back to the for loop whr it is for the flavours.
                {
                    Console.WriteLine("For regular flavours we've got Vanilla / Chocolate / Strawberry options");
                    Console.WriteLine("For premium flavours we've got Durian / Ube / Sea salt options");
                    Console.Write($"Flavour {i} choice: ");
                    flavourType = Console.ReadLine().ToLower();
                    if (!flavourOptions.Contains(flavourType))  //The ! is saying If flavourType cannot be found in array called flavourOptions...
                    {
                        Console.WriteLine("Please enter a flavour from the available options.");
                    }
                    else
                    {
                        break;
                    }
                }

                //Dtermine quantity
                if (flavourQuantity.ContainsKey(flavourType))
                {
                    // Update quantity if the key already exists
                    flavourQuantity[flavourType]++; //flavourQuantity[type] is dictionary[yourchosenkey] which will give you the value aka quantity 
                }
                else
                {
                    // Add a new entry if the key doesn't exist
                    flavourQuantity.Add(flavourType, 1);
                }

            }

            foreach (KeyValuePair<string, int> kvp in flavourQuantity)
            {
                string actualFlavourType = kvp.Key;
                foreach (string pf in premiumFlavour)
                {
                    if (actualFlavourType == pf)
                    {
                        premium = true;
                    }
                }

                Flavour flavour = new Flavour(actualFlavourType, premium, kvp.Value);
                flavours.Add(flavour);
            }
            return flavours;
        }

        // making toppings list
        private static List<Topping> MakingToppingsList() //own method to use later
        {
            List<string> toppingList = new List<string>();
            // looking through toppings.csv
            using (StreamReader sr = new StreamReader("toppings.csv"))
            {
                string s = sr.ReadLine(); //header
                while ((s = sr.ReadLine()) != null)
                {
                    string[] data = s.Split(',');
                    toppingList.Add(data[0].ToLower());
                }
            }
            string toppingChoice = "";
            List<Topping> toppings = new List<Topping>();
            for (int i = 0; i < 4; i++) //Bc can only choose up to 4 toppings!
            {
                while (true)
                {
                    Console.WriteLine("We've got sprinkles, mochi, sago and oreos topping options ");
                    Console.Write("Topping choice? If you don't want to add anymore toppings enter N: ");
                    toppingChoice = Console.ReadLine().ToLower();
                    if (toppingChoice == "n")
                    {
                        break;
                    }
                    else if ((toppingList.Contains(toppingChoice) == true))
                    {
                        Topping topping = new Topping(toppingChoice);
                        toppings.Add(topping); //add topping obj to list called toppings
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid topping");
                    }

                }
                if (toppingChoice == "n")
                {
                    break;
                }

            }
            return toppings;
        }

        public Order MakeOrder()
        {
            string[] waffleFlavourOptions = { "original", "red velvet" , "charcoal" ,"pandan"  }
        Order newOrder = new Order();
            string option = "";
            int scoops = 0;

            // getting option
            while (true)
            {
                Console.Write("Choose from cup / waffle / cone: ");
                option = Console.ReadLine().ToLower();
                if (option == "cup" || option == "waffle" || option == "cone")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please choose an option from those listed");
                }
            }

            // asking for scoops
            try
            {
                Console.Write("Enter number of scoops choose an integer from 1 to 3: ");
                scoops = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please enter an integer from 1 to 3");
            }

            // making flavour and toppings list
            List<Flavour> flavours = MakingFlavoursList(scoops); //MakingFlavourList is the method youu call out but flavours is the name of the list that is returned when you call out MakingFlavourList
            List<Topping> toppings = MakingToppingsList();

            switch (option)
            {
                case "cup":
                    Cup cup = new Cup(option, scoops, flavours, toppings);
                    CurrentOrder = newOrder;  // Set CurrentOrder to newOrder after adding the ice cream
                    OrderHistory.Add(newOrder);
                    CurrentOrder.IceCreamList.Add(cup);

                    break;
                case "waffle":
                    string waffleFlavour = "original";
                    while (true)
                    {
                        Console.Write("We've got Original / Red Velvet / Charcoal / Pandan options: ");
                        waffleFlavour = Console.ReadLine().ToLower();
                        if (waffleFlavour == "original" ||  waffleFlavour == "red velvet" || waffleFlavour == "charcoal" || waffleFlavour == "pandan")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please choose from available options.");
                        }
                    }
                    Waffle waffle = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
                    newOrder.AddIceCream(waffle);
                    CurrentOrder = newOrder;  // Set CurrentOrder to newOrder after adding the ice cream
                    OrderHistory.Add(newOrder);
                    break;
                case "cone":
                    bool dipped = false;
                    Console.Write("Would you like a chocolate dipped cone Y / N");
                    string userInput = Console.ReadLine().ToLower();
                    if (userInput == "y")
                    {
                        dipped = true;
                    }
                    Cone cone = new Cone(option, scoops, flavours, toppings, dipped);
                    newOrder.AddIceCream(cone);
                    CurrentOrder = newOrder;  // Set CurrentOrder to newOrder after adding the ice cream
                    OrderHistory.Add(newOrder);
                    break;
            }

            return newOrder;
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

            if (OrderHistory.Count>0) //Check this to prevent error of NullReferenceException, this error occurs when attempt to access a type variable(that hasn't been instantiated) is made
            {
                foreach (Order dataOHfromList in OrderHistory) //dataOH stands for data order from list hist
                {
                    stringOrderHistory += dataOHfromList.ToString() + "\n";
                }
            }
            return $"Name: {Name} \nMemberID: {Memberid} \nDate Of Birth: {Dob.ToString("dd/MM/yy")} \nCurrent Order: {CurrentOrder} \nOrder History:\n{stringOrderHistory}Points: {Rewards.Points} \nPunch Card: {Rewards.PunchCard} \nTier: {Rewards.Tier} \n"; //Dob.ToString("dd/MM/yy") put output into eg 12/01/22 instead of 12/01/22 12:00AM etc. And is MM, for month. Not mm bc mm = minutes
                                                                                                                                                                                                                                                                    //^^ Rewards.Points  also because i want output Points, but it is in class pointcard. so i have to go through rewards first, which is in class customer but it is of the pointcard type. so i access rewards first before accessing the attributes in pointcard class itself.
        }
    }
}
