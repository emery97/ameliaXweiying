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
using static System.Formats.Asn1.AsnWriter;

namespace PairAssignment
{
    class Order
    {
        //Attributes
        private int id;
        private DateTime timeReceived;
        private DateTime? timeFulfilled;
        private List<IceCream> iceCreamlist;

        //Properties
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        //Connstructors 
        public Order()
        {
            TimeReceived = DateTime.Now;
            IceCreamList = new List<IceCream>();
        }

        public Order(int i, DateTime tr)
        {
            Id = i;
            TimeReceived = tr;
            IceCreamList = new List<IceCream>();

        }

        //Methods
        static List<string> FlavourOptionsAvail()
        {
            List<string> flavourOptions = new List<string>();
            using (StreamReader sr = new StreamReader("flavours.csv"))
            {
                // read header first
                string s = sr.ReadLine();

                while ((s = sr.ReadLine()) != null)
                {
                    string[] items = s.Split(',');
                    flavourOptions.Add(items[0].ToLower());

                }
            }
            return flavourOptions;
        }
        static bool PremiumFlavours(string flavourName)
        {
            List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
            return premiumFlavours.Contains(flavourName);
        }
        public void ModifyIceCream(int modifyIceCreamIndex)
        {
            int option = 0;
            IceCream iceCreamToModify = null; // Initialize
            string[] optionsArray = { "cup", "cone", "waffle" };
            bool modifyFlavour = false;
            List<string> flavourOptionsAvail = FlavourOptionsAvail();
            Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();
            while (true)
            {
                try
                {
                    iceCreamToModify = IceCreamList[modifyIceCreamIndex];
                    int scoopsAmt = iceCreamToModify.Scoops;
                    Console.WriteLine();
                    Console.WriteLine("How would you like to modify your ice cream:");
                    Console.WriteLine("[1] Ice Cream Option ");
                    Console.WriteLine("[2] Ice Cream Scoops ");
                    Console.WriteLine("[3] Ice Cream Flavour Type ");
                    Console.WriteLine("[4] Ice Cream Toppings ");
                    Console.WriteLine("[5] Dipped (Only for Cone)");
                    Console.WriteLine("[6] Waffle Flavour (Only for Waffle)");
                    Console.WriteLine("[0] Return to Main Menu");
                    Console.WriteLine();
                    Console.Write("Enter your desired option: ");

                    int userOption = Convert.ToInt32(Console.ReadLine());
                    if (userOption == null)
                    {
                        Console.WriteLine("Invalid Input! Please try again.");
                        continue;
                    }
                    if (userOption == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("No modification has been done to your ice cream.");
                        Console.WriteLine("You have exited the process of modifying your ice cream.");
                        Console.WriteLine();
                        break;
                    }
                    if (userOption == 1)
                    {
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("We offer cup, cone and waffles.");
                            Console.Write("Please enter your new ice cream option: ");
                            try
                            {
                                string iceCreamOption = Console.ReadLine(); //If modify option, then need ask everything again since diff option have diff stuff eg Cone have dipped but cup doesnt.
                                if (optionsArray.Contains(iceCreamOption) == true) //Use array check if user enter valid option
                                {
                                    iceCreamToModify.Option = iceCreamOption; //set to new option
                                    Console.WriteLine("You have successfully changed your ice cream option!");
                                    Console.WriteLine();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.Please try again.");
                                    Console.WriteLine();
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error has occurred: {0}", ex.Message);
                                Console.WriteLine();
                            }
                        }
                    }
                    else if (userOption == 2)
                    {
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("We offer single [1], double [2], and triple [3] scoops.");
                            Console.Write("Please enter your desired scoops amount in numbers: ");

                            string stringScoopsAmt = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(stringScoopsAmt))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid input! Please enter again.");
                                Console.WriteLine();
                                continue;

                            }
                            scoopsAmt = Convert.ToInt32(stringScoopsAmt);
                            if (scoopsAmt > 3 || scoopsAmt < 1)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid input! Please enter again.");
                                Console.WriteLine();
                                continue;
                            }
                            else
                            {
                                iceCreamToModify.Scoops = scoopsAmt;
                                Console.WriteLine();
                                Console.WriteLine("Amount of scoops has successfully been modified!");
                                Console.WriteLine();
                                modifyFlavour = true;
                                break;
                            }
                        }

                    }
                    else if (userOption == 3 || modifyFlavour)
                    {
                        Console.WriteLine();
                        Console.WriteLine("We offer regular and premium flavours.");
                        Console.WriteLine("Regular Flavours: Vanilla, Chocolate, Strawberry");
                        Console.WriteLine("Premium Flavours: Durian, Ube, Sea Salt");
                        Console.WriteLine();

                        while (true)
                        {
                            Console.WriteLine("Take note to enter a flavour for each scoop.");
                            Console.Write("Please enter your desired flavours [Do ensure that they are comma separated]: ");
                            string[] selectedFlavours = Console.ReadLine().Split(','); //Split become array

                            // Trim each flavor in the array
                            for (int i = 0; i < selectedFlavours.Length; i++)
                            {
                                selectedFlavours[i] = selectedFlavours[i].Trim();
                            }

                            if (selectedFlavours.Length != scoopsAmt) //meaning they choose eg 3 scoops but write desired flavours as Ube, Strawberry only.
                            {
                                Console.WriteLine();
                                Console.WriteLine("Error: The number of flavours should match the amount of scoops: {0}.", scoopsAmt);
                                Console.WriteLine("Please enter again!");
                                Console.WriteLine();
                                continue;
                            }
                            else if (selectedFlavours.All(item => flavourOptionsAvail.Contains(item)) == false)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Vanilla | Chocolate | Strawberry | Durian | Ube | Sea Salt");
                                Console.WriteLine("Please flavours that are listed above");
                                Console.WriteLine();
                                continue;
                            }

                            // FLAVOUR QUANTITY CHECK 
                            else
                            {
                                foreach (var flavour in selectedFlavours)
                                {
                                    // Check if the flavourType already exists in the dictionary
                                    if (flavourQuantity.ContainsKey(flavour))
                                    {
                                        // Increment the count for this flavour type
                                        flavourQuantity[flavour]++;
                                    }
                                    else
                                    {
                                        // Add the flavour type with an initial count of 1
                                        flavourQuantity.Add(flavour, 1);
                                    }
                                }
                            }
                            List<Flavour> flavours = new List<Flavour>();
                            int quantity = 0;
                            foreach (KeyValuePair<string, int> kvp in flavourQuantity)
                            {
                                quantity = kvp.Value;
                                flavours.Add(new Flavour(kvp.Key, PremiumFlavours(kvp.Key), quantity));
                            }                                
                            if (flavours.Count == iceCreamToModify.FlavourList.Count)
                            {
                                iceCreamToModify.FlavourList = flavours;
                                break;
                            }
                        }
                    }
                    else if (userOption == 4)
                    {
                        while (true)
                        {
                            Console.WriteLine("We offer toppings.");
                            Console.WriteLine("Toppings: Sprinkles, Mochi, Sago, Oreos");
                            Console.Write("Please enter your desired toppings [Ensure that they are comma separated]: ");
                            string[] selectedToppings = Console.ReadLine().Split(',');
                            iceCreamToModify.ToppingList.Clear();

                            bool validToppingInput = true;
                            foreach (var toppingData in selectedToppings)
                            {
                                string toppingName = toppingData.Trim().ToLower();

                                if (toppingName == "sprinkles" || toppingName == "mochi" || toppingName == "sago" || toppingName == "oreos")
                                {
                                    iceCreamToModify.ToppingList.Add(new Topping(toppingName));
                                }
                                else
                                {
                                    validToppingInput = false;
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid input. Please enter a topping that we offer!");
                                    Console.WriteLine();
                                    break;
                                }
                            }
                            if (validToppingInput)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Toppings has been successsfully modified!");
                                Console.WriteLine();
                                break;
                            }
                        }
                    }
                    else if (userOption == 5)
                    {
                        if (iceCreamToModify is Cone cone)
                        {
                            bool dippedOrNo = false;

                            while (!dippedOrNo)
                            {
                                Console.Write("Would you like your cone dipped? [ Y / N ]: ");
                                string dipped = Console.ReadLine();

                                if (dipped.ToLower() == "y")
                                {
                                    cone.Dipped = true;
                                    dippedOrNo = true;
                                    Console.WriteLine();
                                    Console.WriteLine("Ice cream has successfully been modified to be dipped!");
                                    Console.WriteLine();
                                }
                                else if (dipped.ToLower() == "n")
                                {
                                    cone.Dipped = false;
                                    Console.WriteLine();
                                    Console.WriteLine("Ice cream has successfully been modified to not be dipped!");
                                    Console.WriteLine();
                                    dippedOrNo = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid input. Please enter again!");
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error detected. You can only choose the dipping option if you ordered a cone!");
                            Console.WriteLine();
                        }
                    }
                    else if (userOption == 6)
                    {
                        if (iceCreamToModify is Waffle waffle)
                        {
                            string[] premiumFlavour = { "red velvet", "pandan", "charcoal" };
                            Console.WriteLine("We offer premium and original waffles.");
                            Console.Write("Would you like it premium? [ Y / N ]: ");
                            string premiumWaffle;
                            bool premiumOrNot = false;
                            bool waffleFlavourCheck = false;
                            while (!premiumOrNot)
                            {
                                try
                                {
                                    premiumWaffle = Console.ReadLine();

                                    if (premiumWaffle.ToLower() == "y")
                                    {
                                        Console.WriteLine();

                                        Console.WriteLine("Premium waffles: Red Velvet, Charcoal, Pandan");
                                        Console.Write("Enter your desired premium waffle Flavour: ");

                                        string waffleFlavour = Console.ReadLine().Trim().ToLower();
                                        foreach (string pf in premiumFlavour)
                                        {
                                            if (waffleFlavour == pf)
                                            {
                                                waffle.WaffleFlavour = pf;
                                                premiumOrNot = true;
                                                waffleFlavourCheck = true;
                                            }
                                        }
                                        if (!waffleFlavourCheck)
                                        {
                                            Console.WriteLine("Invalid input. Please only enter a flavour that we offer.");
                                            Console.WriteLine();
                                            continue;
                                        }
                                    }
                                    else if (premiumWaffle.ToLower() == "n")
                                    {
                                        waffle.WaffleFlavour = "original";
                                        premiumOrNot = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Invalid input. Please enter again!");
                                        Console.WriteLine();
                                        continue;
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid input. Please enter again!");
                                    Console.WriteLine();
                                    continue;
                                }
                                catch (ArgumentNullException)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid input. Please enter again!");
                                    Console.WriteLine();
                                    continue;
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid input. Please enter again!");
                                    Console.WriteLine();
                                    continue;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Error detected: {0}", ex.Message);
                                    Console.WriteLine();
                                }
                            }
                            if (premiumOrNot)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Waffle flavour has successfully been modified! ");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error detected. You can only choose the waffle flavour if you ordered a waffle!");
                            Console.WriteLine();
                        }
                    }
                }

                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: We only accept integers");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: An error seemed to have occured");
                    Console.WriteLine();
                }
            }
        }


        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int icecreamId)
        {

            if (icecreamId >= 0 && icecreamId < IceCreamList.Count())
            {
                IceCreamList.RemoveAt(icecreamId);
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid index.");
            }

        }

        public double CalculateTotal()
        {
            double totalamount = 0;

            foreach (var icecream in IceCreamList)
            {
                totalamount += icecream.CalculatePrice();
            }
            return totalamount;  //NOT INCLUDING FREE ONES 

        }

        public override string ToString()
        {
            string TimeFulfilledText = ""; //initialize

            if (TimeFulfilled == null)
            {
                TimeFulfilledText = "Pending";
            }

            else
            {
                TimeFulfilledText = TimeFulfilled.ToString();
            }


            string orderDetails = "";
            orderDetails = "Order ID: " + Id + "\nTime Received: " + TimeReceived + "\nTime Fulfilled: " + TimeFulfilledText + "\nIce Cream Details: ";

            foreach (IceCream iceCream in IceCreamList)
            {

                orderDetails += "\n" + iceCream;
            }

            return orderDetails;
        }


    }
}