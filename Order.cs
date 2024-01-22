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
        private DateTime ?timeFulfilled;
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
        public DateTime? TimeFulfilled
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

        public Order(int userOrderId, DateTime userTimeReceived)
        {
            Id = userOrderId;
            TimeReceived = userTimeReceived;
            IceCreamList = new List<IceCream>();
        }

        //Methods
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
                    if (Convert.ToInt32(data[1])!= 0)
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

            foreach (KeyValuePair<string,int> kvp in flavourQuantity)
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
        // this is for option 6 , option 1

        public void ModifyIceCream(int iceCreamIndex)
        {
            // Assuming IceCreamList is a List<IceCream>
            IceCream iceCream = IceCreamList[iceCreamIndex - 1];

            Console.WriteLine("[1] Ice Cream Option");
            Console.WriteLine("[2] Ice Cream Scoops");
            Console.WriteLine("[3] Ice Cream Flavours");
            Console.WriteLine("[4] Ice Cream Toppings");
            int menuOption = Convert.ToInt32(Console.ReadLine());

            // Changing option 
            if (menuOption == 1)
            {
                Console.Write("Options to choose from: Cup / Cone / Waffle: ");
                string iceCreamOption = Console.ReadLine().ToLower();
                IceCream newIceCream = null;

                if (iceCreamOption != iceCream.Option)
                {
                    switch (iceCreamOption)
                    {
                        case "cup":
                            newIceCream = new Cup(iceCreamOption, iceCream.Scoops, iceCream.Flavours, iceCream.Toppings);
                            break;
                        case "cone":
                            newIceCream = new Cone(iceCreamOption, iceCream.Scoops, iceCream.Flavours, iceCream.Toppings, false); // Assume not dipped by default
                            break;
                        case "waffle":
                            newIceCream = new Waffle(iceCreamOption, iceCream.Scoops, iceCream.Flavours, iceCream.Toppings, "Original"); // Default flavor
                            break;
                        default:
                            Console.WriteLine("Invalid ice cream option.");
                            break;
                    }
                }

                if (newIceCream != null)
                {
                    if (iceCreamOption == "cone" && newIceCream is Cone cone)
                    {
                        Console.Write("Do you want your cone to be dipped [ Y / N ]: ");
                        string dippedChoice = Console.ReadLine().ToLower();
                        cone.Dipped = dippedChoice == "y";
                    }
                    else if (iceCreamOption == "waffle" && newIceCream is Waffle waffle)
                    {
                        Console.Write("Do you want to change your waffle flavour [ Y / N ]: ");
                        string waffleFlavourOption = Console.ReadLine().ToLower();
                        if (waffleFlavourOption == "y")
                        {
                            Console.WriteLine("We've got Red Velvet / Charcoal / Pandan options: ");
                            waffle.WaffleFlavour = Console.ReadLine().ToLower();
                        }
                    }

                    IceCreamList[iceCreamIndex - 1] = newIceCream;
                }
            }
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
            else if (menuOption == 3)
            {
                List<Flavour> flavours = MakingFlavoursList(iceCream.Scoops); // Assuming this method exists
                iceCream.Flavours = flavours;
            }
            else if (menuOption == 4)
            {
                List<Topping> toppings = MakingToppingsList(); // Assuming this method exists
                iceCream.Toppings = toppings;
            }
        }


        /*
        public void ModifyIceCream(int iceCreamIndex) //methods here onwards are those that need to do bc qn says so 
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
               // iceCream.Option = iceCreamOption; //Equate the current exisitng option (iceCream.Option) to the new choice (iceCreamOption)
                IceCream newIceCream = null; //Initialize 
                if (iceCreamOption != iceCream.Option)
                {
                    // if the ice cream option is different
                    switch (iceCreamOption) //Basically is testing iceCreamOption. 
                    {
                        case "cup": //So if iceCreamOption is cup then...
                            newIceCream = new Cup(); 
                            break;
                        case "cone":
                            newIceCream = new Cone();
                            break;
                        case "waffle":
                            newIceCream = new Waffle();
                            break;
                        default:  //Default is if iceCreamOption is none of those listed (the cases), then....
                            Console.WriteLine("Invalid ice cream option.");
                            break;
                    }
                }
                if (newIceCream != null)
                {
                    newIceCream.Option = iceCreamOption;
                    newIceCream.Scoops = iceCream.Scoops; //Set it to previous ones since did not change those
                    newIceCream.Flavours = iceCream.Flavours;
                    newIceCream.Toppings = iceCream.Toppings;

                    // set iceCream reference to the new ice cream obj
                    iceCream = newIceCream; //current ice cream is now replaced with the new ice cream
                }
                if (iceCreamOption == "cone" && iceCream is Cone cone) //use iceCream and iceCreamOption to equate now instead of newIceCream since you alrd set iceCream = newIceCream above already + "iceCream is Cone cone" is just checking if iceCream is of CONE class type! Eg if want check if it is of waffle type, then would be if(iceCream is Waffle waffle) etc
                {
                    Console.Write("Do you want your cone to be dipped [ Y / N ]");
                    string dippedChoice = Console.ReadLine().ToLower();
                    if (dippedChoice == "y")
                    {
                        cone.Dipped = true;
                    }
                }
                if (iceCreamOption == "waffle" && iceCream is Waffle waffle)
                {
                    Console.Write("Do you want to change your waffle flavour [ Y / N ]");
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
        } */

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