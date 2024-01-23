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
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("How would you like to modify your ice cream:");

                    Console.WriteLine("[1] Ice Cream Option");
                    string userOption = Console.ReadLine(); //Dont convert to int yet so can check if the user input for option is null/invalid

                    Console.WriteLine("[2] Ice Cream Scoops");
                    int userScoops = int.Parse(Console.ReadLine());

                    Console.WriteLine("[3] Ice Cream Flavour Type");
                    string userFlavourType = Console.ReadLine();

                    Console.WriteLine("[4] Ice Cream Toppings");
                    string userToppings = Console.ReadLine();

                    Console.WriteLine("[5]  Dipped (Only for Cone)");
                    Console.WriteLine("6. Waffle Flavour (Only for Waffle)");
                    Console.WriteLine("0. Return to Main Menu");
                    Console.WriteLine("--------------------------------------------");
                    Console.Write("Enter choice: ");
                    Console.WriteLine("");

                    if (string.IsNullOrWhiteSpace(userOption))
                    {
                        Console.WriteLine("Invalid Input! Please try again.");
                        continue; //Skip rest of the code and go back to tart of while loop
                    }

                    option = int.Parse(userOption);
                }

                bool modifyFlavour = false;
                if (option == 0)
                {
                    Console.WriteLine("You did not modify any order.");
                    break; 
                }
                else if (option == 1)
                {
                    while (true)
                    {
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
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid option.Please try again.");
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error has occurred: {0}",ex.Message);
                        }
                    }
                }

                else if (option == 2)
                {
                    while (true)
                    {
                        Console.Write("Enter new number of scoops (Single[1], Double[2], Triple[3]): ");
                        string stringNoOfScoops = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(stringNoOfScoops))
                        {
                            Console.WriteLine();
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine("*** Invalid number of Scoops! Please re-enter valid number of Scoops! ***");
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine();
                            continue;

                        }
                        int noOfScoops = Convert.ToInt32(stringNoOfScoops);
                        if (noOfScoops > 3 || noOfScoops < 1)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine("*** Invalid number of Scoops! Please re-enter valid number of Scoops! ***");
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        else
                        {
                            iceCreamToModify.Scoops = noOfScoops;
                            Console.WriteLine();
                            Console.WriteLine("Success! Number of Scoops have been modified!");
                            Console.WriteLine();
                            modifyFlavour = true;
                            break;
                        }
                    }
                }
                if (option == 3 || modifyFlavour)  //----------
                {
                    while (true)
                    {
                        Console.WriteLine("\nRegular Flavours: Vanilla, Chocolate, Strawberry\nPremium Flavours: Durian, Ube, Sea salt");
                        Console.Write("\nPlease reselect the Ice Cream Flavours (comma separated) : ");
                        string stringFlavourInputs = Console.ReadLine();
                        if(string.IsNullOrWhiteSpace(stringFlavourInputs))
                        {
                            Console.WriteLine();
                            Console.WriteLine("**************************************************************");
                            Console.WriteLine("*** Invalid Input! Please enter a valid Flavour selection! ***");
                            Console.WriteLine("**************************************************************");
                            Console.WriteLine();
                            continue;

                        }
                        string[] flavourInputs = stringFlavourInputs.ToLower().Split(',');
                        bool allFlavoursValid = false;
                        if (flavourInputs.Length != iceCreamToModify.Scoops)
                        {
                            Console.WriteLine();
                            Console.WriteLine("**************************************************************************");
                            Console.WriteLine("*** Error! Number of Flavours should match the number of Scoops ({0})! ***", iceCreamToModify.Scoops);
                            Console.WriteLine("**************************************************************************");
                            Console.WriteLine();
                        }
                        else
                        {
                            iceCreamToModify.FlavourList.Clear();

                            foreach (var input in flavourInputs)
                            {
                                string trimmedInput = input.Trim();

                                if (trimmedInput == "vanilla")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Vanilla", false, 1));
                                    allFlavoursValid = true;
                                }
                                else if (trimmedInput == "chocolate")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Chocolate", false, 1));
                                    allFlavoursValid = true;
                                }
                                else if (trimmedInput == "strawberry")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Strawberry", false, 1));
                                    allFlavoursValid = true;
                                }
                                else if (trimmedInput == "durian")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Durian", true, 1));
                                    allFlavoursValid = true;
                                }
                                else if (trimmedInput == "ube")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Ube", true, 1));
                                    allFlavoursValid = true;
                                }
                                else if (trimmedInput == "sea salt")
                                {
                                    iceCreamToModify.FlavourList.Add(new Flavour("Sea Salt", true, 1));
                                    allFlavoursValid = true;
                                }
                            }
                            if (!allFlavoursValid)
                            {
                                Console.WriteLine();
                                Console.WriteLine("**************************************************************");
                                Console.WriteLine("*** Invalid Input! Please enter a valid Flavour selection! ***");
                                Console.WriteLine("**************************************************************");
                                Console.WriteLine();
                            }
                            else
                            {
                                if (!modifyFlavour)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Success! Desired Flavour has been modified! ");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Success! Number of Scoops and the desired Flavour has been modified! ");
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
                        Console.WriteLine("Toppings: Sprinkles, Mochi, Sago, Oreos");
                        Console.Write("Please reselect the ice cream toppings (comma separated): ");
                        string[] toppingsInputs = Console.ReadLine().Split(',');
                        iceCreamToModify.ToppingList.Clear();

                        bool validToppingInput = true;
                        foreach (var toppingInput in toppingsInputs)
                        {
                            string toppingName = toppingInput.Trim().ToLower();

                            if (toppingName == "sprinkles" || toppingName == "mochi" || toppingName == "sago" || toppingName == "oreos")
                            {
                                iceCreamToModify.ToppingList.Add(new Topping(toppingName));
                            }
                            else
                            {
                                validToppingInput = false;
                                Console.WriteLine("****************************************************************************************************");
                                Console.WriteLine("*** Invalid Topping input! Please re-enter Customer's Topping choice from the available options! ***");
                                Console.WriteLine("****************************************************************************************************");
                                break;
                            }
                        }
                        if (validToppingInput)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Success! Ice Cream Toppings have been modified!");
                            Console.WriteLine();
                            break;
                        }
                    }
                }

                else if (option == 5)
                {
                    if (iceCreamToModify.Option.ToLower() == "cone")
                    {
                        bool validResponse = false;

                        while (!validResponse)
                        {
                            Console.Write("Chocolate Dipped Cone? [Y] / [N]: ");
                            string dipped = Console.ReadLine();

                            if (dipped.ToUpper() == "Y")
                            {
                                iceCreamToModify = new Cone(iceCreamToModify.Option, iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, true);
                                validResponse = true;
                                Console.WriteLine();
                                Console.WriteLine("Success! The Ice Cream has been modified to include a chocolate dip!");
                                Console.WriteLine();
                            }
                            else if (dipped.ToUpper() == "N")
                            {
                                iceCreamToModify = new Cone(iceCreamToModify.Option, iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, false);
                                Console.WriteLine();
                                Console.WriteLine("Success! The Ice Cream has been modified to exclude a chocolate dip!");
                                Console.WriteLine();
                                validResponse = true;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("******************************************************************************");
                        Console.WriteLine("*** Error! Chocolate Dipped Cones are applicable for Cone Ice Creams only! ***");
                        Console.WriteLine("*******************************************************************************");
                        Console.WriteLine();
                    }
                }

                else if (option == 6)
                {
                    if (iceCreamToModify.Option.ToLower() == "waffle")
                    {
                        Waffle waffle = null;
                        Console.Write("\nPremium or Original Waffle Flavour? [Y]/[N] ('N' for Original): ");
                        string premiumWaffle;
                        bool validResponse = false;

                        while (!validResponse)
                        {
                            try
                            {
                                premiumWaffle = Console.ReadLine();

                                if (premiumWaffle.ToLower() == "y")
                                {
                                    Console.WriteLine("Flavoured Waffle: Red Velvet, Charcoal, Pandan");
                                    Console.Write("\nEnter a Premium Waffle Flavour: ");
                                    string waffleFlavour = Console.ReadLine();

                                    if (waffleFlavour.ToLower() == "red velvet")
                                    {
                                        iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Red Velvet");
                                        validResponse = true;
                                    }
                                    else if (waffleFlavour.ToLower() == "charcoal")
                                    {
                                        iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Charcoal");
                                        validResponse = true;
                                    }
                                    else if (waffleFlavour.ToLower() == "pandan")
                                    {
                                        iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Pandan");
                                        validResponse = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("***********************************************************************");
                                        Console.WriteLine("*** Invalid Waffle Flavour! Please re-enter a valid Waffle Flavour! ***");
                                        Console.WriteLine("***********************************************************************");
                                        Console.WriteLine();
                                        continue;
                                    }
                                }
                                else if (premiumWaffle.ToLower() == "n")
                                {
                                    iceCreamToModify = new Waffle("Waffle", iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, "Original");
                                    validResponse = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine();
                                    continue;
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            catch (Exception ex) // A general catch-all for any other types of exceptions
                            {
                                Console.WriteLine("\nAn unexpected error occurred: {0}\n", ex.Message);
                            }
                        }
                        if (validResponse)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Success! Ice Cream Flavour has been modified! ");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("************************************************************************");
                        Console.WriteLine("*** Error! Flavoured Waffle is only applicable for Waffle Ice Cream! ***");
                        Console.WriteLine("************************************************************************");
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
                Console.WriteLine("Invalid Index entered!");
            }

        }

        public double CalculateTotal() // need to see the birthday
        {
            // calculate total amount of the order ( final amount to Pay )

            double totalamount = 0;

            foreach (var icecream in IceCreamList)
            {
                totalamount += icecream.CalculatePrice();
            }
            return totalamount;


        }

        public override string ToString()
        {
            string TimeFulfilledText = "";
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