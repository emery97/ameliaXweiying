//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh
// Partner Name : Lee Wei Ying
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
        public void ModifyIceCream(int updateIcOrder, List<string> flavoursAvail, List<string> toppingsAvail)
        {
            int option = 0;
            IceCream iceCreamToModify = null; // Initialize iceCreamToModify outside of the if statement
            List<string> premiumIceCreamFlavours = new List<string> { "durian", "ube", "sea salt" }; //===================================================================================================
            List<string> premiumWaffleFlavours = new List<string> { "red velvet", "charcoal", "pandan" };

            bool modificationSuccess = false;
            while (true)
            {
                try
                {
                    if (updateIcOrder >= 0 && updateIcOrder < IceCreamList.Count())
                    {
                        iceCreamToModify = IceCreamList[updateIcOrder];

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Select a modification for your ice cream:");
                        Console.WriteLine("1. Ice Cream Option");
                        Console.WriteLine("2. Number of Scoops");
                        Console.WriteLine("3. Flavours");
                        Console.WriteLine("4. Toppings");
                        Console.WriteLine("5. Cone Dipped (Only for Cone)");
                        Console.WriteLine("6. Waffle Flavour (Only for Waffle)");
                        Console.WriteLine("0. Return to Main Menu");
                        Console.WriteLine("--------------------------------------------");
                        Console.Write("Enter choice: ");
                        string stringOption = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(stringOption))
                        {
                            Console.WriteLine();
                            Console.WriteLine("*****************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter a valid Input! ***");
                            Console.WriteLine("*****************************************************");
                            Console.WriteLine();
                            continue; // Restart the entire process
                        }
                        option = Convert.ToInt32(stringOption);
                        //iceCreamOption = ""; // Initialize iceCreamOption to empty here
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine("*** Invalid format! Please enter a numeric value! ***");
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine();
                    continue;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine();
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine("*** Invalid format! Please enter a valid value! ***");
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine();
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nAn unexpected error occurred: {0}", ex.Message);
                    continue;
                }


                bool modifyFlavourNext = false;
                if (option == 0)
                {
                    Console.WriteLine();
                    break; // Exit the outer loop when option is 0
                }

                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                else if (option == 1)
                {
                    List<string> iceCreamOptionAvailable = new List<string> { "Cup", "Cone", "Waffle" };
                    while (true)
                    {
                        Console.Write("Enter Ice Cream Option: ");
                        string iceCreamOption = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(iceCreamOption))
                        {
                            Console.WriteLine();
                            Console.WriteLine("*************************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter your Ice Cream option! ***");
                            Console.WriteLine("*************************************************************");
                            Console.WriteLine();
                            continue;
                        }

                        string formattedIceCreamOption = iceCreamOption.Trim().ToLower();
                        string formatIceCreamOptionAgain = char.ToUpper(iceCreamOption[0]) + iceCreamOption.Substring(1); // so when printed out it will look nicer, will start w caps
                        bool validOption = false;
                        foreach (var iceCreamOptionValid in iceCreamOptionAvailable)
                        {
                            if (iceCreamOptionValid.ToLower() == formattedIceCreamOption)
                            {
                                validOption = true;
                                break;
                            }
                        }

                        if (!validOption)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*************************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter your Ice Cream option! ***");
                            Console.WriteLine("*************************************************************");
                            Console.WriteLine();
                            continue;
                        }


                        if (iceCreamToModify.Option.ToLower() != formattedIceCreamOption)
                        {
                            try
                            {
                                if (formattedIceCreamOption == "cup")
                                {
                                    // if it was cone initially and it is dipped what happens? check later
                                    // if it was waffle initially and premium waffle selected what happens?
                                    iceCreamToModify = new Cup(formatIceCreamOptionAgain, iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList);
                                    IceCreamList[updateIcOrder] = iceCreamToModify;
                                    Console.WriteLine();
                                    Console.WriteLine("Success! Ice Cream Option has been modified!");
                                    Console.WriteLine();
                                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                    Console.WriteLine(iceCreamToModify.ToString());
                                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                    Console.WriteLine();
                                    Console.WriteLine("Returning to Modification Menu...");
                                    Console.WriteLine();
                                    modificationSuccess = true;
                                    break;
                                }
                                else if (formattedIceCreamOption == "cone")
                                {
                                    while (true)
                                    {
                                        Console.Write("Chocolate Dipped Cone [Y]/[N]: ");
                                        string dipped = Console.ReadLine();
                                        bool isDipped = false;
                                        if (string.IsNullOrWhiteSpace(dipped) || dipped.Trim().ToLower() != "y" && dipped.Trim().ToLower() != "n")
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("*****************************************************");
                                            Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                            Console.WriteLine("*****************************************************");
                                            continue;
                                        }
                                        if (dipped.Trim().ToLower() == "y")
                                        {
                                            isDipped = true;
                                        }
                                        iceCreamToModify = new Cone(formatIceCreamOptionAgain, iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, isDipped);
                                        IceCreamList[updateIcOrder] = iceCreamToModify;
                                        Console.WriteLine();
                                        Console.WriteLine("Success Ice Cream Option has been modified!");
                                        Console.WriteLine();
                                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                        Console.WriteLine(iceCreamToModify.ToString());
                                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                        Console.WriteLine();
                                        Console.WriteLine("Returning to Modification Menu...");
                                        Console.WriteLine();
                                        modificationSuccess = true;
                                        break;
                                    }
                                    break;
                                }
                                else if (formattedIceCreamOption == "waffle")
                                {
                                    string isPremiumWaffle = ""; // expected output is yes or no
                                    while (true)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Waffle Premium Flavours: Red Velvet, Charcoal, Pandan");
                                        Console.Write("Premium or Original Waffle Flavour? [Y]/[N] ('N' for Original): ");
                                        isPremiumWaffle = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(isPremiumWaffle) || isPremiumWaffle.Trim().ToLower() != "y" && isPremiumWaffle.Trim().ToLower() != "n")
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("*****************************************************");
                                            Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                            Console.WriteLine("*****************************************************");
                                            continue;
                                        }
                                        break;
                                    }

                                    string waffleFlavour = "";
                                    bool validWaffleFlavour = false;
                                    if (isPremiumWaffle.Trim().ToLower() == "y")
                                    {
                                        while (true)
                                        {
                                            Console.Write("Enter Premium Waffle option: ");
                                            waffleFlavour = Console.ReadLine();
                                            if (string.IsNullOrWhiteSpace(waffleFlavour))
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("******************************************************");
                                                Console.WriteLine("*** Invalid Input! Please re-enter a valid option! ***");
                                                Console.WriteLine("******************************************************");
                                                continue;
                                            }

                                            foreach (var waffleFlavourOption in premiumWaffleFlavours)  // if input is red velvet, durian, pandan (initialised at the top)
                                            {
                                                if (waffleFlavourOption.ToLower() == waffleFlavour.Trim().ToLower())
                                                {
                                                    validWaffleFlavour = true;
                                                    break;
                                                }
                                            }
                                            if (validWaffleFlavour)
                                            {
                                                break; // Break out of the loop if a valid flavour is chosen
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("***************************************************************");
                                                Console.WriteLine("*** Invalid Waffle Flavour! Please re-enter a valid option! ***");
                                                Console.WriteLine("***************************************************************");
                                                Console.WriteLine();
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        waffleFlavour = "Original";
                                    }

                                    string formatWaffleFlavour = char.ToUpper(waffleFlavour[0]) + waffleFlavour.Substring(1);
                                    iceCreamToModify = new Waffle(formatIceCreamOptionAgain, iceCreamToModify.Scoops, iceCreamToModify.FlavourList, iceCreamToModify.ToppingList, formatWaffleFlavour);
                                    // this line is to assign the modified iceCreamToModify back to the original
                                    IceCreamList[updateIcOrder] = iceCreamToModify;
                                    Console.WriteLine();
                                    Console.WriteLine("Success Ice Cream Option has been modified!");
                                    Console.WriteLine();
                                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                    Console.WriteLine(iceCreamToModify.ToString());
                                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                    Console.WriteLine();
                                    Console.WriteLine("Returning to Modification Menu...");
                                    Console.WriteLine();
                                    modificationSuccess = true;
                                    break;
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("******************************************************");
                                Console.WriteLine("*** Invalid Input! Please re-enter a valid option! ***");
                                Console.WriteLine("******************************************************");
                                Console.WriteLine();
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("******************************************************");
                                Console.WriteLine("*** Invalid Input! Please re-enter a valid option! ***");
                                Console.WriteLine("******************************************************");
                                Console.WriteLine();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine();
                                Console.WriteLine("**********************************************************************************************");
                                Console.WriteLine("*** An unexpected error occurred! Please try again or contact admin if the issue persists! ***");
                                Console.WriteLine("**********************************************************************************************");
                                Console.WriteLine();
                                continue;
                            }
                        }
                        else // if option is already what the user wanted
                        {
                            Console.WriteLine();
                            Console.WriteLine("Selection already matches the current Ice Cream option! No modification is necessary!");
                            Console.WriteLine();
                            break;
                        }
                    }
                }

                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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

                        try
                        {
                            int noOfScoops = Convert.ToInt32(stringNoOfScoops);

                            if (noOfScoops >= 1 && noOfScoops <= 3) // must have atleast 1 or at most 3 
                            {
                                if (noOfScoops != iceCreamToModify.Scoops)
                                {
                                    iceCreamToModify.Scoops = noOfScoops;
                                    Console.WriteLine("Success! Number of Scoops have been modified!");
                                    modifyFlavourNext = true;
                                    modificationSuccess = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Selection already matches the current Ice Cream Scoops! No modification is necessary!");
                                    Console.WriteLine();
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("*************************************************************************");
                                Console.WriteLine("*** Invalid number of Scoops! Please re-enter valid number of Scoops! ***");
                                Console.WriteLine("*************************************************************************");
                                Console.WriteLine();
                                continue;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("**************************************************************************");
                            Console.WriteLine("*** Invalid format! Please enter a numeric value for number of scoops! ***");
                            Console.WriteLine("**************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine("*** Invalid number of Scoops! Please re-enter valid number of Scoops! ***");
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine();
                            Console.WriteLine("**********************************************************************************************");
                            Console.WriteLine("*** An unexpected error occurred! Please try again or contact admin if the issue persists! ***");
                            Console.WriteLine("**********************************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                    }
                }

                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                if (option == 3 || modifyFlavourNext) // check wether flavours exists in the new passed in parameters (the 2 new list)
                {
                    while (true)
                    {
                        try
                        {
                            iceCreamToModify.FlavourList.Clear();
                            Console.WriteLine();
                            Console.WriteLine("Regular Flavours: Vanilla, Chocolate, Strawberry\nPremium Flavours: Durian, Ube, Sea salt");
                            Console.Write("Please reselect the Ice Cream Flavours (comma separated): ");
                            string stringFlavourInputs = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(stringFlavourInputs))
                            {
                                Console.WriteLine();
                                Console.WriteLine("**********************************************************************");
                                Console.WriteLine("*** Invalid Flavour Input! Please enter a valid Flavour selection! ***");
                                Console.WriteLine("**********************************************************************");
                                continue;
                            }

                            string[] flavourInputs = stringFlavourInputs.ToLower().Split(',');
                            if (flavourInputs.Length != iceCreamToModify.Scoops)
                            {
                                Console.WriteLine();
                                Console.WriteLine("**************************************************");
                                Console.WriteLine("*** Please re-enter only {0} Ice Cream Flavour! ***", iceCreamToModify.Scoops);
                                Console.WriteLine("**************************************************");
                                continue;
                            }

                            bool allFlavoursValid = true;
                            List<Flavour> newFlavours = new List<Flavour>();
                            foreach (var flavInput in flavourInputs)
                            {
                                string flavInputTrimmed = flavInput.Trim();
                                if (flavoursAvail.Contains(flavInputTrimmed)) // if the list of valid flavours consist of flavInput (user's flavour input)
                                {
                                    // if contain in the set of valid ice cream flavours, check if it is premium or not 
                                    bool isPremium = premiumIceCreamFlavours.Contains(flavInputTrimmed);
                                    string formatFlavour = char.ToUpper(flavInputTrimmed[0]) + flavInputTrimmed.Substring(1);
                                    newFlavours.Add(new Flavour(formatFlavour, isPremium, 1));
                                }
                                else
                                {
                                    allFlavoursValid = false;
                                    Console.WriteLine();
                                    Console.WriteLine("**********************************************************************");
                                    Console.WriteLine("*** Invalid Flavour Input! Please enter a valid Flavour selection! ***");
                                    Console.WriteLine("**********************************************************************");
                                    break;
                                }
                            }
                            if (allFlavoursValid)
                            {
                                iceCreamToModify.FlavourList = newFlavours; // if this block didnt continue from modifying the number of scoops
                                if (!modifyFlavourNext)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Success! Desired Flavour has been modified! ");
                                }
                                else   // if this block only got executed because user updated the number of scoops
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Success! Number of Scoops and the desired Flavour has been modified! ");
                                }
                                Console.WriteLine();
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine(iceCreamToModify.ToString());
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine("Returning to Modification Menu...");
                                Console.WriteLine();
                                modificationSuccess = true;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("************************************************************************");
                            Console.WriteLine("*** An error occurred: The selection is not valid! Please try again! ***");
                            Console.WriteLine("************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("************************************************************************");
                            Console.WriteLine("*** An error occurred: The operation is not valid. Please try again. ***");
                            Console.WriteLine("************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine();
                            Console.WriteLine("**********************************************************************************************");
                            Console.WriteLine("*** An unexpected error occurred! Please try again or contact admin if the issue persists! ***");
                            Console.WriteLine("**********************************************************************************************");
                            Console.WriteLine();
                            continue;
                        }

                    }
                }

                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                else if (option == 4) // check with the new parameters if input for toppings is within the given allowable set, also check max topping is 4 only 
                {
                    while (true)
                    {
                        try
                        {
                            iceCreamToModify.ToppingList.Clear();
                            Console.WriteLine("Toppings: Sprinkles, Mochi, Sago, Oreos");
                            Console.Write("Please reselect the ice cream toppings (comma separated): ");
                            string stringToppingInputs = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(stringToppingInputs))
                            {
                                Console.WriteLine();
                                Console.WriteLine("****************************************************************************************************");
                                Console.WriteLine("*** Invalid Topping input! Please re-enter Customer's Topping choice from the available options! ***");
                                Console.WriteLine("****************************************************************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            //iceCreamToModify.ToppingList.Clear();
                            string[] toppingsInputs = stringToppingInputs.Split(',');

                            if (toppingsInputs.Length > 4)
                            {
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************************************************");
                                Console.WriteLine("*** Invalid Topping input! Please re-enter maximum of 4 Topping choice per Ice Cream! ***");
                                Console.WriteLine("*****************************************************************************************");
                                Console.WriteLine();
                                continue;
                            }

                            bool validToppingInput = true;
                            foreach (var toppingInput in toppingsInputs)
                            {
                                string toppingName = toppingInput.Trim().ToLower();

                                if (toppingsAvail.Contains(toppingName))
                                {
                                    string formatToppingName = char.ToUpper(toppingName[0]) + toppingName.Substring(1);
                                    iceCreamToModify.ToppingList.Add(new Topping(formatToppingName));
                                }
                                else
                                {
                                    validToppingInput = false;
                                    Console.WriteLine();
                                    Console.WriteLine("**********************************************************************************************");
                                    Console.WriteLine("*** Invalid Topping! Please re-enter Customer's Topping choice from the available options! ***");
                                    Console.WriteLine("**********************************************************************************************");
                                    Console.WriteLine();
                                    break;
                                }
                            }
                            if (validToppingInput)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Success! Ice Cream Toppings have been modified!");
                                Console.WriteLine();
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine(iceCreamToModify.ToString());
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine("Returning to Modification Menu...");
                                Console.WriteLine();
                                modificationSuccess = true;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("************************************************************************");
                            Console.WriteLine("*** An error occurred: The operation is not valid. Please try again. ***");
                            Console.WriteLine("************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine();
                            Console.WriteLine("**********************************************************************************************");
                            Console.WriteLine("*** An unexpected error occurred! Please try again or contact admin if the issue persists! ***");
                            Console.WriteLine("**********************************************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                    }
                }

                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                else if (option == 5) // since this option is gauranteed a cone, we should downcast it so we dont have to create a new obj
                {
                    if (iceCreamToModify.Option.ToLower() == "cone")
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Write("Chocolate Dipped Cone [Y]/[N]: ");
                                string dipped = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(dipped) || dipped.Trim().ToLower() != "y" && dipped.Trim().ToLower() != "n")
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine("*** Invalid option! Please re-enter only [Y]/[N]! ***");
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine();
                                    continue;
                                }
                                bool isDipped = false;
                                if (dipped.Trim().ToLower() == "y")
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Success! The Ice Cream has been modified to include a chocolate dip!");
                                    isDipped = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Success! The Ice Cream has been modified to exclude a chocolate dip!");
                                }
                                Cone cone = (Cone)iceCreamToModify; // downcasting 
                                cone.Dipped = isDipped;
                                Console.WriteLine();
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine(iceCreamToModify.ToString());
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine("Returning to Modification Menu...");
                                Console.WriteLine();
                                modificationSuccess = true;
                                break;
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("************************************************************************");
                                Console.WriteLine("*** An error occurred: The operation is not valid. Please try again. ***");
                                Console.WriteLine("************************************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine();
                                Console.WriteLine("**********************************************************************************************");
                                Console.WriteLine("*** An unexpected error occurred! Please try again or contact admin if the issue persists! ***");
                                Console.WriteLine("**********************************************************************************************");
                                Console.WriteLine();
                                continue;
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

                // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                else if (option == 6)
                {
                    if (iceCreamToModify.Option.ToLower() == "waffle") // again gauranteed its a waffle, hence can use downcasting
                    {
                        bool validResponse = false;

                        while (true)
                        {
                            Console.WriteLine("Waffle Premium Flavours: Red Velvet, Charcoal, Pandan");
                            Console.Write("Premium or Original Waffle Flavour? [Y]/[N] ('N' for Original): ");

                            string stringIsPremiumWaffle = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(stringIsPremiumWaffle))
                            {
                                Console.WriteLine();
                                Console.WriteLine("***********************************************************************");
                                Console.WriteLine("*** Invalid Waffle Flavour! Please re-enter a valid Option [Y]/[N]! ***");
                                Console.WriteLine("***********************************************************************");
                                Console.WriteLine();
                                continue;
                            }

                            string isPremiumWaffle = stringIsPremiumWaffle.Trim().ToLower(); // premiumWaffle output should only be y or n

                            Waffle waffle = (Waffle)iceCreamToModify;
                            try
                            {
                                if (isPremiumWaffle == "y")
                                {
                                    Console.Write("Enter a Premium Waffle Flavour: ");
                                    string stringWaffleFlavour = Console.ReadLine();

                                    if (string.IsNullOrWhiteSpace(stringWaffleFlavour))
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("***********************************************************************");
                                        Console.WriteLine("*** Invalid Waffle Flavour! Please re-enter a valid Waffle Flavour! ***");
                                        Console.WriteLine("***********************************************************************");
                                        Console.WriteLine();
                                        continue;
                                    }

                                    string waffleFlavour = stringWaffleFlavour.Trim().ToLower();
                                    //premiumwaffleflavour is in small letters
                                    if (premiumWaffleFlavours.Contains(waffleFlavour)) // remember to format the value. ok done
                                    {
                                        string formatWaffleFlavour = char.ToUpper(waffleFlavour[0]) + waffleFlavour.Substring(1);
                                        waffle.WaffleFlavour = formatWaffleFlavour;
                                        validResponse = true;
                                    }
                                    else //-----------------------------------------------------
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("***********************************************************************");
                                        Console.WriteLine("*** Invalid Waffle Flavour! Please re-enter a valid Waffle Flavour! ***");
                                        Console.WriteLine("***********************************************************************");
                                        Console.WriteLine();
                                        continue;
                                    }
                                }
                                else if (isPremiumWaffle == "n")
                                {
                                    waffle.WaffleFlavour = "Original";
                                    validResponse = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("***********************************************************************");
                                    Console.WriteLine("*** Invalid Waffle Flavour! Please re-enter a valid Option [Y]/[N]! ***");
                                    Console.WriteLine("***********************************************************************");
                                    Console.WriteLine();
                                    continue;
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("***********************************************************");
                                Console.WriteLine("*** Invalid Input! Please enter a valid Option [Y]/[N]! ***");
                                Console.WriteLine("***********************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            if (validResponse)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Success! Waffle Flavour has been updated!");
                                Console.WriteLine();
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine(iceCreamToModify.ToString());
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine("Returning to Modification Menu...");
                                Console.WriteLine();
                                modificationSuccess = true;
                                break;
                            }
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
                if (modificationSuccess) // update time rcv after every modification // what if customer has issue with their order and need to cross check what time the order is placed
                {
                    TimeReceived = DateTime.Now;
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
            double totalAmt = 0;
            foreach (IceCream ic in IceCreamList)
            {
                totalAmt += ic.CalculatePrice();
            }
            return totalAmt;
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