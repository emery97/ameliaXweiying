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
        public void ModifyIceCream(int modifyIceCreamIndex)
        {
            int option = 0;
            IceCream iceCreamToModify = null; // Initialize
            string[] optionsArray = { "cup", "cone", "waffle" };



            while (true)
            {
                if (modifyIceCreamIndex >= 0 && modifyIceCreamIndex < IceCreamList.Count())
                {
                    iceCreamToModify = IceCreamList[modifyIceCreamIndex];
                    Console.WriteLine();
                    Console.WriteLine("How would you like to modify your ice cream:");

                    Console.Write("[1] Ice Cream Option: ");
                    string userOption = Console.ReadLine(); //Dont convert to int yet so can check if the user input for option is null/invalid

                    Console.Write("[2] Ice Cream Scoops: ");
                    int userScoops = int.Parse(Console.ReadLine());

                    Console.Write("[3] Ice Cream Flavour Type: ");
                    string userFlavourType = Console.ReadLine();

                    Console.Write("[4] Ice Cream Toppings: ");
                    string userToppings = Console.ReadLine();

                    Console.WriteLine("[5]  Dipped (Only for Cone)");
                    Console.WriteLine("[6] Waffle Flavour (Only for Waffle)");
                    Console.WriteLine("[0] Return to Main Menu");
                    Console.WriteLine();
                    Console.Write("Enter your desired option: ");
                    Console.WriteLine("");

                    if (string.IsNullOrWhiteSpace(userOption))
                    {
                        Console.WriteLine("Invalid Input! Please try again.");
                        continue;
                    }

                    option = int.Parse(userOption);
                }

                bool modifyFlavour = false;
                if (option == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("No modification has been done to your ice cream.");
                    Console.WriteLine("You have exited the process of modifying your ice cream.");
                    Console.WriteLine();
                    break;
                }
                else if (option == 1)
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("We offer cup, cone and waffles.");
                        Console.Write("Please enter your new ice cream option: ");
                        Console.WriteLine("");
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

                else if (option == 2)
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
                        int scoopsAmt = Convert.ToInt32(stringScoopsAmt);
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
                if (option == 3 || modifyFlavour)
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("We offer regular and premium flavours.");
                        Console.WriteLine("Regular Flavours: Vanilla, Chocolate, Strawberry");
                        Console.WriteLine("Premium Flavours: Durian, Ube, Sea Salt");
                        Console.WriteLine();

                        Console.Write("Please choose your new desired flavours [Do ensure that they are comma separated] : ");
                        string stringSelectedFlavours = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(stringSelectedFlavours))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input! Please enter again.");
                            Console.WriteLine();
                            continue;

                        }
                        string[] flavourInputs = stringSelectedFlavours.ToLower().Split(',');

                        bool validFlavours = false;
                        if (flavourInputs.Length != iceCreamToModify.Scoops)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input! Please enter again.");
                            Console.WriteLine();
                        }
                        else
                        {
                            iceCreamToModify.FlavourList.Clear();

                            foreach (var userChoice in flavourInputs)
                            {
                                string userChoiceTrimmed = userChoice.Trim();

                                if (userChoiceTrimmed == "vanilla")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Vanilla", false, 1));
                                    validFlavours = true;
                                }
                                else if (userChoiceTrimmed == "chocolate")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Chocolate", false, 1));
                                    validFlavours = true;
                                }
                                else if (userChoiceTrimmed == "strawberry")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Strawberry", false, 1));
                                    validFlavours = true;
                                }
                                else if (userChoiceTrimmed == "durian")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Durian", true, 1));
                                    validFlavours = true;
                                }
                                else if (userChoiceTrimmed == "ube")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Ube", true, 1));
                                    validFlavours = true;
                                }
                                else if (userChoiceTrimmed == "sea salt")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Sea Salt", true, 1));
                                    validFlavours = true;
                                }
                            }
                            if (!validFlavours)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid input. Please enter a flavour that we offer!");
                                Console.WriteLine();
                            }
                            else
                            {
                                if (!modifyFlavour)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Flavour has successfully been modified! ");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Desired number of Scoops and flavour has successfully been modified! ");
                                    Console.WriteLine();
                                }
                                break;
                            }
                        }
                    }
                }

                else if (option == 4)
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

                else if (option == 5)
                {
                    if (iceCreamToModify.Option.ToLower() == "cone")
                    {
                        bool dippedOrNo = false;

                        while (!dippedOrNo)
                        {
                            Console.Write("Would you like your cone dipped? [ Y / N ]: ");
                            string dipped = Console.ReadLine();

                            if (dipped.ToLower() == "y")
                            {
                                iceCreamToModify = new Cone(iceCreamToModify.Option, iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, true);
                                dippedOrNo = true;
                                Console.WriteLine();
                                Console.WriteLine("Ice cream has successfully been modified to be dipped!");
                                Console.WriteLine();
                            }
                            else if (dipped.ToLower() == "n")
                            {
                                iceCreamToModify = new Cone(iceCreamToModify.Option, iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, false);
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

                else if (option == 6)
                {
                    if (iceCreamToModify.Option.ToLower() == "waffle")
                    {
                        Waffle waffle = null;
                        Console.WriteLine("We offer premium and original waffles.");
                        Console.Write("Would you like it premium? [ Y / N ]: ");
                        string premiumWaffle;
                        bool premiumOrNot = false;

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

                                    string waffleFlavour = Console.ReadLine();

                                    if (waffleFlavour.ToLower() == "red velvet")
                                    {
                                        iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Red Velvet");
                                        premiumOrNot = true;
                                    }
                                    else if (waffleFlavour.ToLower() == "charcoal")
                                    {
                                        iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Charcoal");
                                        premiumOrNot = true;
                                    }
                                    else if (waffleFlavour.ToLower() == "pandan")
                                    {
                                        iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Pandan");
                                        premiumOrNot = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Invalid input. Please only enter a flavour that we offer.");
                                        Console.WriteLine();
                                        continue;
                                    }
                                }
                                else if (premiumWaffle.ToLower() == "n")
                                {
                                    iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Original");
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