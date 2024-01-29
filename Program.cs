//==========================================================
// Student Number : S10257856
// Student Name : Amelia Goh
// Partner Name : Lee Wei Ying
//==========================================================

using System;
using PairAssignment;
using System.Globalization;
using System.Xml.Linq;

class Program
{
    static List<Customer> customerList = new List<Customer>();
    static Queue<Order> goldMemberQueue = new Queue<Order>();
    static Queue<Order> regularMemberQueue = new Queue<Order>();
    static Queue<Order> allMemberQueue = new Queue<Order>();
    static int currentOrderId = 5; //from orders csv file, the last order id is 4. so hence start from 4
    //^^btw, the order id should only change when the order is fulfilled aka the time fulfilled is not "PENDING". so even though you keep pressing basic 4 eg 3 times, and each time, you say NO when they ask if want add another ice cream,
    //these 3 orders that you ordered separately should still have the same order ID (even tho order ID is meant to change with EVERY NEW SUCCESSFUL COMPLETED order you make. as in, not when u say yes to bassic 4 when they ask to add more ice cream, but as in you order the ice creams SEPARATELY.
    //But because those orders made when click basic 4 x3 times, it falls udner same order id bc those orders are not fulfilled yet [in short, it accumulates]. order ID changes with every NEW SUCCESSFUL COMPLETED order you make.
    static int lastOrderId = 0;
    static void Main(string[] args)
    {
        InitializeCustomerList(customerList);
        List<string> flavourAvail = FlavourOptionsAvail(); //initialize this for basic 6 later
        List<string> toppingAvail = ToppingOptionsAvail();
        while (true)
        {
            DisplayMenu();
            Console.Write("Enter your option: ");
            try
            {
                string option = Console.ReadLine();
                Console.WriteLine();
                if (option == "1")
                {
                    ListingAllCustomer(customerList);
                    Console.WriteLine();
                }
                else if (option == "2")
                {
                    ListingAllCurrentOrders(customerList, goldMemberQueue, regularMemberQueue);
                    Console.WriteLine();
                }
                else if (option == "3")
                {
                    RegisterNewCustomer(customerList);
                }
                else if (option == "4")
                {
                    ListingAllCustomer(customerList);
                    CreateCustomerOrder(customerList, goldMemberQueue, regularMemberQueue);
                }
                else if (option == "5")
                {
                    // List the customer 
                    ListingAllCustomer(customerList);
                    Customer selectedCustomer = SelectMember(customerList);
                    DisplayOrderDetailsOfCustomer(customerList, selectedCustomer);
                    // Now display current order and order history of the selected customer
                    DisplayCurrentOrder(selectedCustomer);
                    Console.WriteLine();
                    DisplayOrderHistory(selectedCustomer);
                    Console.WriteLine();
                }
                else if (option == "6")
                {
                    ModifyOrderDetails(customerList, flavourAvail, toppingAvail);
                }
                else if (option == "7")
                {
                    ProcessOrderAndCheckout(customerList, goldMemberQueue, regularMemberQueue);
                }
                else if (option == "8")
                {
                    //DisplayMonthlyAndTotalAmount(customerList);
                    DisplayAmountForTheYear(customerList);
                }
                else if (option == "0")
                {
                    foreach (Order c in allMemberQueue)
                    {
                        Console.WriteLine(c);
                    }
                    //APPEND EVERYTHING HERE
                    /*using (StreamWriter sw = new StreamWriter("order.csv",true))
                    {
                        foreach (Customer c in customerList)
                        {
                            string[] dataArray = { };
                            if (c.CurrentOrder != null)
                            {
                                //sw.Write(c.CurrentOrder.ToString());
                                dataArray[0] = c.CurrentOrder.Id.ToString();


                            }
                        }
                    }*/
                    /*try
                    {
                        // appending **** SHIFT TO THE TOP LATER! ******
                        using (StreamWriter sw = new StreamWriter("customers.csv", true))
                        {
                            sw.WriteLine("{0},{1},{2},{3},{4},{5}", name, memberID, dob, "Ordinary", customer.Rewards.Points, customer.Rewards.PunchCard); //"Ordinary" never use customer.Rewards.Points bc customer.Rewards is a pointcard object. Then pointcard obj have pointcard's constructor of Pointcard(int, int). The tier is not in its parameters. So only can access the int aka points and punchcard via customer.Rewards.Points etc and tier cannot. 
                            Console.WriteLine("Registration for {0} is done!", customer.Name);
                            Console.WriteLine();
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: The file where new data needs to be appended to, could not be found!");
                        Console.WriteLine();
                    }
                    catch (IOException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: The file is open in another program. Please close it and try again!");
                        Console.WriteLine();
                    }*/

                    Console.WriteLine("\nYou have successfully exited. Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter again!");
                    Console.WriteLine();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Please enter again!");
                Console.WriteLine();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Please enter again!");
                Console.WriteLine();
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Please enter again!");
                Console.WriteLine();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Please enter again!");
                Console.WriteLine();
            }
            catch (Exception ex) // General exception for other errors
            {
                Console.WriteLine();
                Console.WriteLine("An error has occurred: {0}", ex.Message);
                Console.WriteLine();
            }
        }


        // -------- MENU ---------------------------------------------------------------------------------- 
        static void DisplayMenu()
        {
            Console.WriteLine(new string('-', 40) + " M E N U " + new string('-', 40));
            Console.WriteLine("[1] List all customers");
            Console.WriteLine("[2] List all current orders");
            Console.WriteLine("[3] Register a new customer");
            Console.WriteLine("[4] Create a customer’s order");
            Console.WriteLine("[5] Display order details of a customer");
            Console.WriteLine("[6] Modify order details");
            Console.WriteLine("[7] Process an order & Checkout");
            Console.WriteLine("[8] Display monthly charged amounts breakdown and total charged amounts for the year");
            Console.WriteLine("[0] Exit");
            Console.WriteLine(new string('-', 88));
        }

        static Customer SelectMember(List<Customer> customerList)
        {
            Customer chosenCustomer = new Customer(); // initialize

            while (true)
            {
                bool found = false;  // Reset found to false at the beginning of each iteration

                try
                {
                    Console.Write("Please enter customer user ID ['0' to exit]: ");
                    string stringID = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(stringID))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Please enter member ID again!");
                        Console.WriteLine();
                        continue;
                    }
                    if (stringID == "0")
                    {
                        Console.WriteLine("You have successfully exited.");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        foreach (Customer c in customerList)
                        {
                            if (c.MemberId == Convert.ToInt32(stringID))
                            {
                                found = true;
                                chosenCustomer = c;
                                break; // Break out of the foreach loop, because we've found the customer.
                            }
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid Member ID! Member ID not found!");
                        Console.WriteLine();
                    }
                    if (found)
                    {
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input. Please enter a valid member ID again!");
                    Console.WriteLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input. The member ID is too large. Please enter a valid member ID.");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine();
                }
            }

            return chosenCustomer;
        }



        // -------- BASIC 1 ( Amelia ) -----------------------------------------------------------

        static void InitializeCustomerList(List<Customer> cList)
        {
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                // read header 
                string s = sr.ReadLine();
                string[] header = s.Split(',');

                // read others 
                while ((s = sr.ReadLine()) != null)
                {
                    string[] items = s.Split(',');
                    string name = items[0];
                    int memberId = Convert.ToInt32(items[1]);
                    DateTime dob = Convert.ToDateTime(items[2]);


                    string tier = Convert.ToString(items[3]);
                    int points = Convert.ToInt32(items[4]);
                    int punchCard = Convert.ToInt32(items[5]);

                    Customer customer = new Customer(name, memberId, dob);
                    PointCard customerPointCard = customer.Rewards;
                    customerPointCard.Points = points;
                    customerPointCard.PunchCard = punchCard;
                    customerPointCard.Tier = tier;
                    cList.Add(customer);
                }
            }
        }

        static void ListingAllCustomer(List<Customer> cList)
        {
            Console.WriteLine("{0,-14}{1,-17}{2,-21}{3,-16}{4,-14}{5}", "Name", "Member ID", "Date of Birth", "Tier", "Points", "Punch Card");
            Console.WriteLine(new string('-', 93));
            foreach (var customer in cList)
            {
                Console.WriteLine("{0,-14}{1,-17}{2,-21}{3,-16}{4,-14}{5}", customer.Name, customer.MemberId, customer.Dob.ToString("dd/MM/yyyy"), customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
                Console.WriteLine("");
            }

            Console.WriteLine();
        }



        // -------- BASIC 2 ( Weiying ) -------------------------------------------------------------------------------------------------------------------------------------- //
        static void ListingAllCurrentOrders(List<Customer> cList, Queue<Order> goldQueue, Queue<Order> regularQueue)
        {
            Console.WriteLine("{0,16}----------", "");
            Console.WriteLine("{0,16}Gold Queue", "");
            Console.WriteLine("{0,16}----------", "");
            if (goldQueue.Count() > 0)
            {
                foreach (Order goldQueueOrderDetails in goldQueue)
                {
                    Console.WriteLine(goldQueueOrderDetails.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("{0, 4}There are no gold members in the queue.", "");
                Console.WriteLine();
            }

            Console.WriteLine("{0,16}-------------", "");
            Console.WriteLine("{0,16}Regular Queue", "");
            Console.WriteLine("{0,16}-------------", "");
            if (regularQueue.Count() > 0)
            {
                foreach (Order regularQueueOrderDetails in regularQueue)
                {
                    Console.WriteLine(regularQueueOrderDetails.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("{0,4}There are no regular members in the queue.", "");
            }
        }

        // ------- BASIC 3 ( Amelia ) -------------------------------------------------------------------------------------------------------------------------------------- //
        static void RegisterNewCustomer(List<Customer> cList)
        {
            string name = "";
            while (true)
            {
                Console.Write("Enter new customer's name ('0' to exit): ");

                string nameInput = Console.ReadLine();
                if (nameInput == "0")
                {
                    Console.WriteLine();
                    Console.WriteLine("You have chosen to not register a new customer.");
                    Console.WriteLine();
                    return; // exit of the while loop
                }
                if (string.IsNullOrWhiteSpace(nameInput))
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Name cannot be empty.Please enter again!");
                    Console.WriteLine();
                    continue;
                }
                if (nameInput.All(char.IsLetter))
                {
                    name = char.ToUpper(nameInput[0]) + nameInput.Substring(1).ToLower();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Name can only contain letters. Please enter again!");
                    Console.WriteLine();
                }
            }

            int memberID;
            while (true)
            {
                Console.Write("Enter new customer's member ID: ");

                string stringMemberId = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringMemberId))
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Member ID cannot be empty. Please enter again!");
                    Console.WriteLine();
                    continue; // so that can ask user again
                }
                else
                {
                    // Check if the length of the input is exactly 6 digits 
                    if (stringMemberId.Length != 6 || !stringMemberId.All(char.IsDigit)) //!stringMemberId.All(char.IsDigit) is basically saying if the member id is not a number. (aka the member id has to be a number. not a legit number number, but a number just that it is in string format) So if user enters 'hello' or 11111111 aka exceed 6 digits, then error msg occurs.
                    {
                        Console.WriteLine();

                        if (stringMemberId.Length < 6 || !stringMemberId.All(char.IsDigit))
                        {
                            Console.WriteLine("Error: Member ID must be 6 digits long and contain only numeric characters. You entered fewer than 6 digits or included non-numeric characters.");
                        }
                        else
                        {
                            Console.WriteLine("Error: Member ID must be 6 digits long and contain only numeric characters. You entered more than 6 digits or included non-numeric characters.");
                        }

                        Console.WriteLine();
                        continue;
                    }
                    try //for validation,handle errors
                    {
                        memberID = Convert.ToInt32(stringMemberId);

                        bool doesMemberIdExist = false;

                        foreach (Customer customerMemberId in cList)
                        {
                            if (memberID == customerMemberId.MemberId) //Check if person enter a pre existing id that already is a member
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Error: This member ID '{memberID}' already belongs to an existing customer!");
                                Console.WriteLine();
                                doesMemberIdExist = true;
                                break;
                            }
                        }
                        if (!doesMemberIdExist)
                        {
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Member ID must be a valid integer. Please enter again!");
                        Console.WriteLine();
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Member ID must be within the range of a 32-bit integer. Please enter again!");
                        Console.WriteLine();
                    }
                }
            }
            DateTime dob;
            while (true)
            {
                Console.Write("Enter Customer's Date Of Birth (DD/MM/YYYY): ");
                string dobInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dobInput))
                {
                    Console.WriteLine("Error: Date Of Birth cannot be empty. Please enter again!");
                    Console.WriteLine();
                    continue; // ask the user again
                }
                try
                {
                    if (DateTime.TryParseExact(dobInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                    {
                        if (dob > DateTime.Now)
                        {
                            Console.WriteLine("Error: Date Of Birth has to be before today! Please enter again.");
                            Console.WriteLine();
                            continue; // ask again
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid date format. Please enter again in the format of (DD/MM/YYYY)!");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine();
                }
            }


            // Now make customer obj with whatever user just gave
            string dateOnly = dob.ToString("dd/MM/yyyy");
            Customer customer = new Customer(name, memberID, dob);
            cList.Add(customer);


            //Make pointcard object
            PointCard pointcard = new PointCard();

            //Match this new pointcard obj to the new customer
            customer.Rewards = pointcard; //points, tier etc would be 0 and ordinary since is a new customer [all these are declared in pointcard class since this 'pointcard' non paramterized constructor in point card class r points with values of 0, etc etc

            Console.WriteLine();
            Console.WriteLine($"Customer {name} has been successfully registered!");
            Console.WriteLine();

        }

        //------- BASIC 4 ( Amelia ) -------------------------------------------------------------------------------------------------------------------------------------- //

        static void CreateCustomerOrder(List<Customer> cList, Queue<Order> goldQueue, Queue<Order> regularQueue)
        {
            int customerMemberId;
            Customer selectedCustomer = SelectMember(customerList);
            string stringCustomerMemberId = Convert.ToString(selectedCustomer.MemberId); //Bc selectedCustomer is a customer object. So cannot just equate entire thing to string.
            //^^ need to specifically get what you want aka the member id. Not the whole thing. 
            //^^ Also put to string first and not int straight away in case of the error below!
            string stringCustomerMemberIdAgain = "";

            while (true)
            {
                if (string.IsNullOrWhiteSpace(stringCustomerMemberId)) //This error^^, where you check if string is null
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input! Please enter a valid member ID!");
                    Console.WriteLine();
                    continue;
                }
                customerMemberId = Convert.ToInt32(stringCustomerMemberId);

                if (customerMemberId == 0)
                {
                    return;
                }


                if (selectedCustomer == null) //diff from string.IsNullOrWhiteSpace on top as that checks string and this checks the variable of an object type.
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input! Please enter a valid member ID!");
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }

            if (selectedCustomer == null)
            {
                while (selectedCustomer == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input! Please enter a valid member ID!");
                    Console.WriteLine();

                    selectedCustomer = SelectMember(customerList);
                    stringCustomerMemberIdAgain = Convert.ToString(selectedCustomer.MemberId);


                    if (string.IsNullOrWhiteSpace(stringCustomerMemberIdAgain))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input! Please enter a valid member ID!");
                        Console.WriteLine();
                    }
                    customerMemberId = Convert.ToInt32(stringCustomerMemberIdAgain);

                    if (customerMemberId == 0)
                    {
                        return;
                    }

                    selectedCustomer = SelectMember(customerList);
                }
            }

            Order newOrder = new Order(); // Always create a new Order object
            bool orderCreated = false;
            bool addAgain = false;

            while (!orderCreated)
            {
                CreatingIceCream(newOrder);

                string orderAgain;
                do
                {
                    Console.WriteLine();
                    Console.Write("Would you like to add another ice cream? [ Y / N ] : ");
                    orderAgain = Console.ReadLine().ToLower();

                    if (orderAgain == "y" || orderAgain == "n")
                    {
                        break; // Break out of the do-while loop if input is valid
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Please try again!");
                    }
                } while (true); // This loop will continue until a valid input is received

                if (orderAgain == "n")
                {
                    if (newOrder.IceCreamList.Count > 0)
                    {
                        if (selectedCustomer.Rewards.Tier == "Gold")
                        {
                            if (goldQueue.Contains(newOrder))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Order success! It has been added to the gold queue!");
                                Console.WriteLine();
                            }
                            else
                            {
                                goldQueue.Enqueue(newOrder);
                                Console.WriteLine();
                                Console.WriteLine("Order success! It has been added to the gold queue!");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            if (regularQueue.Contains(newOrder))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Order success! It has been added to the regular queue!");
                                Console.WriteLine();
                            }
                            else
                            {
                                regularQueue.Enqueue(newOrder);
                                Console.WriteLine();
                                Console.WriteLine("Order success! It has been added to the regular queue!");
                                Console.WriteLine();
                            }
                        }

                        selectedCustomer.CurrentOrder = newOrder;
                        orderCreated = true;
                        currentOrderId++;
                    }
                    else
                    {
                        Console.WriteLine("No ice creams added. Order not created.");
                        Console.WriteLine();
                        orderCreated = true;
                    }


                }
                else if (orderAgain == "y")
                {
                    addAgain = true;
                }

                else
                {
                    Console.WriteLine("Error: Invalid input. Please try again!");
                    Console.WriteLine();
                }
            }
        }



        //To add the ice creams
        static void CreatingIceCream(Order order)
        {
            while (true)
            {
                List<string> options = new List<string> { "cup", "waffle", "cone" }; // for option checking
                List<Flavour> flavours = new List<Flavour>();
                Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();
                List<Topping> toppings = new List<Topping>();
                IceCream newIceCream = null;
                bool doesOptionExists = false; //check if icecream option person selected exists
                List<string> flavourOptionsAvail = FlavourOptionsAvail();
                List<string> toppingsOptionsAvail = ToppingOptionsAvail();

                Console.WriteLine();
                Console.WriteLine("We offer cup, cone and waffle.");
                Console.Write("Please enter which you would like: ");
                string iceCreamOption = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(iceCreamOption))
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input. Please try again!");
                    Console.WriteLine();
                    continue;
                }

                if (options.Contains(iceCreamOption))
                {
                    doesOptionExists = true;
                }

                if (!doesOptionExists)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input. Please enter your ice cream option again!");
                    Console.WriteLine();
                    continue;
                }

                bool validScoopsAmt = false;
                int scoopsAmt = 0;
                while (!validScoopsAmt)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine("We offer single [1], double [2], and triple [3] scoops.");
                        Console.Write("Please enter your desired scoops amount in numbers: "); //-----------------------------------------------------------------------------------------
                        string stringScoops = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(stringScoops))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error: Invalid input. Please try again!");
                            Console.WriteLine();
                            continue;
                        }
                        scoopsAmt = Convert.ToInt32(stringScoops);

                        if (scoopsAmt >= 1 && scoopsAmt <= 3)
                        {
                            validScoopsAmt = true;
                        }
                        if (!validScoopsAmt)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error: Invalid input. Please try again!");
                            Console.WriteLine();
                            continue;
                        }
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Please try again!");
                        Console.WriteLine();
                        continue;
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: {0}", ex.Message);
                        Console.WriteLine();
                    }
                }

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
                    int quantity = 0;
                    foreach (KeyValuePair<string, int> kvp in flavourQuantity)
                    {
                        quantity = kvp.Value;
                        flavours.Add(new Flavour(kvp.Key, PremiumFlavours(kvp.Key), quantity));
                    }

                    Console.WriteLine();
                    Console.WriteLine("We offer toppings!");
                    Console.WriteLine("We have sprinkles, mochi, sago and oreos");

                    while (true)
                    {
                        bool toppingsExist = false;
                        Console.Write("Enter desired toppings or 'N' for no topping [Ensure that they are comma separated] : ");
                        string[] selectedToppings = Console.ReadLine().Split(','); //become array

                        if (selectedToppings[0].Trim().ToLower() == "n")
                        {
                            toppings.Add(new Topping("None"));
                            toppingsExist = true;
                        }

                        else
                        {
                            foreach (var topping in selectedToppings)
                            {
                                string toppingName = topping.Trim().ToLower();

                                if (toppingName == "sprinkles")
                                {
                                    toppings.Add(new Topping("sprinkles"));
                                    toppingsExist = true;
                                }

                                else if (toppingName == "mochi")
                                {
                                    toppings.Add(new Topping("mochi"));
                                    toppingsExist = true;
                                }

                                else if (toppingName == "sago")
                                {
                                    toppings.Add(new Topping("sago"));
                                    toppingsExist = true;
                                }

                                else if (toppingName == "oreos")
                                {
                                    toppings.Add(new Topping("oreos"));
                                    toppingsExist = true;
                                }
                            }
                        }

                        if (!toppingsExist)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input. Please enter toppings again!");
                            Console.WriteLine();
                        }

                        else
                        {
                            break;
                        }
                    }


                    if (iceCreamOption == "cup")
                    {
                        newIceCream = new Cup("cup", scoopsAmt, flavours, toppings);
                    }

                    else if (iceCreamOption == "cone")
                    {
                        bool dippedOrNot = false;

                        while (!dippedOrNot)
                        {
                            Console.Write("Would you like your cone dipped? [Y / N]: ");

                            string dipped = Console.ReadLine().ToLower();

                            if (dipped == "y")
                            {
                                newIceCream = new Cone("cone", scoopsAmt, flavours, toppings, true);
                                dippedOrNot = true;
                            }

                            else if (dipped == "n")
                            {
                                newIceCream = new Cone("cone", scoopsAmt, flavours, toppings, false);
                                dippedOrNot = true;
                            }

                            if (!dippedOrNot)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Error: Invalid input. Please enter again!");
                                Console.WriteLine();
                            }
                        }
                    }

                    if (iceCreamOption == "waffle")
                    {
                        bool waffleExists = false; //see if the waffle type exists
                        while (!waffleExists)
                        {
                            Console.WriteLine();
                            Console.WriteLine("We offer premium and original waffle flavours.");
                            Console.WriteLine("Premium Flavours: Red Velvet, Charcoal, Pandan");
                            Console.Write("Would you like it premium? [Y / N]: ");
                            string premiumOrNot = Console.ReadLine().ToLower();

                            if (premiumOrNot == "y")
                            {
                                Console.WriteLine();
                                Console.Write("Enter a Premium Waffle Flavour: ");
                                string waffleFlavour = Console.ReadLine().Trim().ToLower();

                                if (waffleFlavour == "red velvet")
                                {
                                    newIceCream = new Waffle("waffle", scoopsAmt, flavours, toppings, "red velvet");
                                    waffleExists = true;
                                }
                                else if (waffleFlavour == "charcoal")
                                {
                                    newIceCream = new Waffle("waffle", scoopsAmt, flavours, toppings, "charcoal");
                                    waffleExists = true;
                                }
                                else if (waffleFlavour == "pandan")
                                {
                                    newIceCream = new Waffle("waffle", scoopsAmt, flavours, toppings, "pandan");
                                    waffleExists = true;
                                }
                            }

                            else if (premiumOrNot == "n") //aka they dw premium, so stick with original
                            {
                                newIceCream = new Waffle("waffle", scoopsAmt, flavours, toppings, "original");
                                waffleExists = true;
                            }

                            if (!waffleExists)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Error: Invalid input. Please enter desired waffle flavour again!");
                                Console.WriteLine();
                            }
                        }
                    }

                    if (newIceCream != null)
                    {
                        order.AddIceCream(newIceCream);
                        order.Id = currentOrderId;
                        break;
                    }
                }
                if (newIceCream != null)
                {
                    break;
                }
            }
        }

        //------- BASIC 5 ( Weiying ) -------------------------------------------------------------------------------------------------------------------------------------- //
        static void DisplayOrderDetailsOfCustomer(List<Customer> cList, Customer selectedCustomer)
        {
            if (selectedCustomer.Name == null)
            {
                return;
            }

            if (selectedCustomer.OrderHistory == null) //So if null, then initiaize it?
            {
                selectedCustomer.OrderHistory = new List<Order>();
            }

            // Clear existing order history
            selectedCustomer.OrderHistory.Clear();
            try
            {
                using (StreamReader sr = new StreamReader("orders.csv"))
                {
                    string s;

                    sr.ReadLine(); // Skip the header

                    // Read each line representing an order
                    while ((s = sr.ReadLine()) != null)
                    {
                        Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();
                        string[] items = s.Split(',');

                        // Convert each element to lowercase and trim
                        for (int i = 0; i < items.Length; i++)
                        {
                            items[i] = items[i].ToLower().Trim();
                        }

                        int orderId = Convert.ToInt32(items[0]);
                        int memberId = Convert.ToInt32(items[1]);

                        // If the order memberId does not match the selectedCustomer memberId, skip it
                        if (selectedCustomer == null || memberId != selectedCustomer.MemberId)
                        {
                            continue;
                        }

                        DateTime timeReceived = Convert.ToDateTime(items[2]);
                        DateTime? timeFulfilled = items[3] != "null" ? Convert.ToDateTime(items[3]) : (DateTime?)null;
                        string option = items[4];
                        int scoops = Convert.ToInt32(items[5]);
                        bool dipped = items.Length > 6 && items[6].Trim().ToLower() == "true";
                        string waffleFlavour = items.Length > 7 ? items[7] : null;
                        lastOrderId = Math.Max(lastOrderId, orderId);

                        // FLAVOUR QUANTITY CHECK 
                        for (int i = 8; i <= 10; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(items[i]))
                            {
                                string flavourType = items[i].Trim();

                                // Check if the flavourType already exists in the dictionary
                                if (flavourQuantity.ContainsKey(flavourType))
                                {
                                    // Increment the count for this flavour type
                                    flavourQuantity[flavourType]++;
                                }
                                else
                                {
                                    // Add the flavour type with an initial count of 1
                                    flavourQuantity.Add(flavourType, 1);
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


                        List<Topping> toppings = new List<Topping>();
                        for (int i = 11; i <= 14; i++)
                        {
                            if (items[i] != null)
                            {
                                toppings.Add(new Topping(items[i]));
                            }
                        }
                        Order order = new Order(orderId, timeReceived);
                        order.TimeFulfilled = timeFulfilled;

                        // Make IceCream instance based on the option
                        if (option.Equals("cup", StringComparison.OrdinalIgnoreCase))
                        {
                            IceCream iceCream = new Cup(option, scoops, flavours, toppings);
                            order.AddIceCream(iceCream); // Add the created ice cream to the order
                        }
                        else if (option.Equals("cone", StringComparison.OrdinalIgnoreCase))
                        {
                            IceCream iceCream = new Cone(option, scoops, flavours, toppings, dipped);
                            order.AddIceCream(iceCream);
                        }
                        else if (option.Equals("waffle", StringComparison.OrdinalIgnoreCase))
                        {
                            IceCream iceCream = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
                            order.AddIceCream(iceCream);
                        }

                        // Add the order to the selected customer's order history only once
                        selectedCustomer.OrderHistory.Add(order);
                    }
                }
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: Orders file not found. Please make sure the 'orders.csv' file is available.");
                // Log the exception or handle it accordingly
            }

        }



        //------- BASIC 6 ( Weiying ) -------------------------------------------------------------------------------------------------------------------------------------- 
        static void ModifyOrderDetails(List<Customer> cList, List<string> flavoursAvailable, List<string> toppingAvailable)
        {
            ListingAllCustomer(cList); // List all customers
            int memberIdInput = 0;
            Customer selectedMemberId = null; // Initialize the selected customer to null

            while (true)
            {
                Console.Write("Enter Customer's Member ID (0 to exit): ");
                string MemberId = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(MemberId))
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid input! Please enter a valid Member ID.");
                    Console.WriteLine();
                    continue;
                }
                else if (MemberId.Trim() == "0")
                {
                    return; // Exit the method if 0 is entered
                }
                try
                {
                    memberIdInput = Convert.ToInt32(MemberId);
                    // Use the SelectMember method to get the selected customer
                    selectedMemberId = SelectMember(cList);

                    if (selectedMemberId == null || selectedMemberId.CurrentOrder == null || selectedMemberId.CurrentOrder.IceCreamList == null || selectedMemberId.CurrentOrder.IceCreamList.Count == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("***********************************************************************************************");
                        Console.WriteLine("*** Error! Ice Cream Modifications can only be made for Customers who have existing Orders! ***");
                        Console.WriteLine("***********************************************************************************************");
                        Console.WriteLine();
                        return; // Early return to exit the method
                    }

                    DisplayCurrentOrder(selectedMemberId);

                    int modifyOption = 0;
                    while (true)
                    {
                        Console.Write(
                            "[1] Modify Ice Cream Order\n" +
                            "[2] Add an Ice Cream to Order\n" +
                            "[3] Delete an existing Ice Cream\n" +
                            "[0] Exit to Main Menu\n" +
                            "Enter an option: "
                        );
                        string stringModifyOption = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(stringModifyOption) || stringModifyOption.Trim() != "1" && stringModifyOption.Trim() != "2" && stringModifyOption.Trim() != "3" && stringModifyOption.Trim() != "0")
                        {
                            Console.WriteLine();
                            Console.WriteLine("******************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter a valid option! ***");
                            Console.WriteLine("******************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        try
                        {
                            modifyOption = Convert.ToInt32(stringModifyOption);
                            break; // Continue with the next loop
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*****************************************************");
                            Console.WriteLine("*** Invalid format! Please enter a numeric value! ***");
                            Console.WriteLine("*****************************************************");
                            Console.WriteLine();
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("**********************************************************");
                            Console.WriteLine("*** The number entered is not valid! Please try again! ***");
                            Console.WriteLine("**********************************************************");
                            Console.WriteLine();
                        }
                    }

                    Order order = selectedMemberId.CurrentOrder;

                    while (true)
                    {
                        if (modifyOption == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Returning to Main Menu...");
                            Console.WriteLine();
                            break;
                        }
                        else if (modifyOption == 1)
                        {
                            DisplayIceCreamIndex(selectedMemberId);

                            Console.Write("Enter the index of the ice cream to modify (0 to exit): ");
                            string stringSelectedIndex = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(stringSelectedIndex))
                            {
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine("*** Invalid index. Please re-enter a valid Index! ***");
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            try
                            {
                                int selectedIndex = Convert.ToInt32(stringSelectedIndex) - 1; // Adjust for zero-based indexing
                                if (selectedIndex == -1) // this is for "0 to exit"
                                {
                                    Console.WriteLine("\nReturning to Main Menu...\n");
                                    return;
                                }
                                if (selectedIndex >= 0 && selectedIndex < order.IceCreamList.Count())
                                {
                                    Console.WriteLine();
                                    order.ModifyIceCream(selectedIndex, flavoursAvailable, toppingAvailable); //==================================================================
                                    DisplayCurrentOrder(selectedMemberId);
                                    Console.WriteLine("Returning to Main Menu...\n");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine("*** Invalid index! Please re-enter a valid Index! ***");
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine();
                                    continue;
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine("*** Invalid format! Please enter a numeric value! ***");
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine();
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("**********************************************************");
                                Console.WriteLine("*** The number entered is not valid! Please try again! ***");
                                Console.WriteLine("**********************************************************");
                                Console.WriteLine();
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine();
                                Console.WriteLine("**********************************************************************");
                                Console.WriteLine("*** The index entered is out of range! Please enter a valid index! ***");
                                Console.WriteLine("**********************************************************************");
                                Console.WriteLine();
                            }

                        }
                        else if (modifyOption == 2)
                        {
                            CreatingIceCream(order);
                            DisplayCurrentOrder(selectedMemberId);
                            //order.Id = currentOrderId; //-------------
                            Console.WriteLine();
                            Console.WriteLine("Sucess! Order has been added to cart!");
                            Console.WriteLine();
                            Console.WriteLine("Returning to Main Menu...\n");
                            break;
                        }
                        else //if (modifyOption == 3)
                        {
                            while (true)
                            {
                                DisplayIceCreamIndex(selectedMemberId);
                                Console.Write("Enter the index of the ice cream to Delete (0 to exit): ");
                                string stringSelectedIndex = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(stringSelectedIndex))
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine("*** Invalid index! Please re-enter a valid Index! ***");
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine();
                                    continue;
                                }
                                try
                                {
                                    int selectedIndex = Convert.ToInt32(stringSelectedIndex) - 1; // Adjust for zero-based indexing
                                    if (selectedIndex == -1)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Sucess! Order has been removed from cart!");
                                        Console.WriteLine("\nReturning to Main Menu...\n");
                                        return;
                                    }
                                    else if (selectedIndex >= 0 && selectedIndex < order.IceCreamList.Count())
                                    {
                                        if (order.IceCreamList.Count() > 1)
                                        {
                                            order.DeleteIceCream(selectedIndex);
                                            Console.WriteLine();
                                            Console.WriteLine("Success! Ice Cream Order at Index {0} has been Deleted!", selectedIndex + 1);
                                            Console.WriteLine("\nReturning to Main Menu...\n");
                                            DisplayCurrentOrder(selectedMemberId);
                                            Console.WriteLine("\nReturning to Main Menu...\n");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("***********************************************************");
                                            Console.WriteLine("*** Error! Ice Cream Order cannot have zero Ice Creams! ***");
                                            Console.WriteLine("***********************************************************");
                                            Console.WriteLine();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("*****************************************************");
                                        Console.WriteLine("*** Invalid index! Please re-enter a valid Index! ***");
                                        Console.WriteLine("*****************************************************");
                                        Console.WriteLine();
                                        continue;
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine("*** Invalid format! Please enter a numeric Index! ***");
                                    Console.WriteLine("******************************************************");
                                    Console.WriteLine();
                                }
                                catch (OverflowException)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("***************************************************************");
                                    Console.WriteLine("*** Number too large! Please enter a smaller numeric Index! ***");
                                    Console.WriteLine("****************************************************************");
                                    Console.WriteLine();
                                }
                            }
                        }

                    }
                }

                
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine("*** Invalid format! Please enter a numeric value! ***");
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine();
                    Console.WriteLine("**********************************************************");
                    Console.WriteLine("*** The number entered is not valid! Please try again! ***");
                    Console.WriteLine("**********************************************************");
                    Console.WriteLine();
                }
            }
        }


        static void DisplayIceCreamIndex(Customer customerId)
        {
            IceCream iceCreamOrder = null;
            int iceCreamIndex = 1;
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-70}Ice Cream Index", "");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (IceCream iceCream in customerId.CurrentOrder.IceCreamList)
            {
                iceCreamOrder = iceCream;
                Console.WriteLine("[{0}] {1}", iceCreamIndex, iceCreamOrder.ToString());
                //Console.WriteLine(iceCreamOrder.ToString());
                iceCreamIndex++;
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        


        // ------- ADVANCED 1 ( Amelia )--------------------------------------------------------------------------------------------------------------------------------------

        static void ProcessOrderAndCheckout(List<Customer> cList, Queue<Order> goldQueue, Queue<Order> regularQueue)
        {
            if (goldQueue.Count > 0)
            {
                //Dequeue the first order in the queue
                Order firstGoldOrder = goldQueue.Dequeue(); //this assign the first order (by using goldQueue.Dequeue) to be taken out to 'firstGoldOrder'
                Customer goldCustomer = LinkOrderToCustomer(cList, firstGoldOrder);

                if (goldCustomer != null)
                {
                    ProcessAnOrder(goldCustomer, firstGoldOrder);
                }
            }

            else if (regularQueue.Count > 0)
            {
                Order firstRegOrder = regularQueue.Dequeue(); //this assign the first order (by using goldQueue.Dequeue) to be taken out to 'firstGoldOrder'
                Customer regCustomer = LinkOrderToCustomer(cList, firstRegOrder);

                if (regCustomer != null)
                {
                    ProcessAnOrder(regCustomer, firstRegOrder);
                }
            }

            else
            {
                Console.WriteLine("The Queues are empty. There are no orders waiting to be processed!");
                Console.WriteLine();
            }
        }


        //These are needed for advanced 1
        static Customer LinkOrderToCustomer(List<Customer> cList, Order customerOrder) //**rmb need return smth of customer type
        {
            foreach (Customer c in cList)
            {
                if (c.CurrentOrder == customerOrder && c.CurrentOrder != null) //Rmb is compare c.CurrentOrder and not c.OrderHistory bc now, for advanced we want find price of those orders that have not completed yet. Orders in order history are orders that have final price alrd. That order is fulfilled alrady.
                {
                    return c; //info abt customer
                }
            }
            return null; //return null if no customers' current order ends up matching the customerOrder
        }

        static double ListAllIceCreamsAndFindTotalPrice(Order order)
        {
            int i = 1;
            double icPrice = 0;
            foreach (IceCream ic in order.IceCreamList)
            {
                string stringIceCreamData = ic.ToString();
                Console.WriteLine($"{i}. {stringIceCreamData} Price: ${ic.CalculatePrice()}");
                //Console.WriteLine($"Price: ${ic.CalculatePrice()}");
                i++;
                icPrice += ic.CalculatePrice();
            }
            return icPrice;
        }

        static void PunchIceCream(Order order, PointCard rewards, bool redeemPunchOrNo) //need include redeemPunchOrNo to see if user want redeem punch card
        {
            foreach (var iceCream in order.IceCreamList)
            {
                if (redeemPunchOrNo && rewards.PunchCard == 10)
                {
                    rewards.PunchCard = 0; //reset it to 0 once hit 10
                }
                rewards.Punch();
            }
        }


        static void ProcessAnOrder(Customer customerWithOrderToProcess, Order orderToProcess)
        {
            double pointsToMoney = 0.02;
            double findMostExpensiveIC = 0;
            int iceCreamCount = orderToProcess.IceCreamList.Count; // Track the number of ice creams purchased in this order
            bool completePC = customerWithOrderToProcess.Rewards.PunchCard == 10;

            //Need display all ice creams + price soo..
            double iceCreamPrice = ListAllIceCreamsAndFindTotalPrice(orderToProcess); //This method also calculates the price of all the ice creams 


            if (customerWithOrderToProcess.IsBirthday() == true) //is their birthday
            {
                Console.WriteLine();
                Console.WriteLine("Happy Birthday!");
                Console.WriteLine("As a gift, the most expensive ice cream in your order would be free of charge!");
                foreach (IceCream ic in orderToProcess.IceCreamList)
                {
                    if (ic.CalculatePrice() > findMostExpensiveIC)
                    {
                        findMostExpensiveIC = ic.CalculatePrice();
                    }
                }
                iceCreamPrice -= findMostExpensiveIC; //since above equate iceCreamPrice to ListAllIceCreamsAndFindTotalPrice method, and this method returns the total price, which iceCreamPrice is then equated to.
            }

            bool completePCCheck = customerWithOrderToProcess.CurrentOrder.IceCreamList.Count() + customerWithOrderToProcess.Rewards.PunchCard >= 10;
            bool redeemPunchOrNo = false; //need ask and see if user want redeem or nt
            if (completePCCheck)
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("You have completed your Punch Card!");
                    Console.WriteLine();
                    Console.Write("Would you like to redeem it [ Y / N ]? : ");
                    try
                    {
                        string userRedeemPC = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(userRedeemPC))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error: Invalid input! Please try again.");
                            Console.WriteLine();
                            continue;
                        }
                        if (userRedeemPC.Trim().ToLower() == "y")
                        {
                            double firstIceCreamPrice = orderToProcess.IceCreamList[0].CalculatePrice();  // returns the first element since qn says charge the first to be free

                            redeemPunchOrNo = true;
                            Console.WriteLine();
                            Console.WriteLine($"You have chosen to redeem your Punch Card.\n Your first Ice Cream will be free.\n Discount: ${firstIceCreamPrice}");
                            Console.WriteLine();

                            iceCreamPrice -= firstIceCreamPrice;

                            break;
                        }
                        else if (userRedeemPC.Trim().ToLower() == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error: Invalid input! Please try again.");
                            Console.WriteLine();
                            continue;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input! Please only enter [ Y / N ].");
                        Console.WriteLine();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input! Please try again.");
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                            Console.WriteLine();
                            Console.WriteLine("Error: Invalid input! Please try again.");
                            Console.WriteLine();
                    }
                }
            }



            if (customerWithOrderToProcess.Rewards.Points > 0 && customerWithOrderToProcess.Rewards.Tier != "Ordinary") //Only gold and sliver can redeem
            {
                Console.WriteLine("You are eligible for points redemption!");

                while (true)
                {
                    try
                    {
                        Console.WriteLine($"Amount of points available before payment: {customerWithOrderToProcess.Rewards.Points}");

                        int maxAmtOfPointsToRedeem = Convert.ToInt32(Math.Floor(iceCreamPrice / pointsToMoney)); //bc if dont do this, can customer might want to redeem all of their points. but then they buy a $1 thing only, yet they choose use all their points which is worth $10. just dont make sense
                                                                                                                    //^^ int bc points should be in int. and round down to nearest integer, NOT round up.

                        if (customerWithOrderToProcess.Rewards.Points <= maxAmtOfPointsToRedeem) //bc in case customer have 20 pts, but they can redeem up to 100 pts. the max amount of pts they eventually can redeem is 20 pts since they dh enough pts to redeem all 100 pts. the ELSE part for this would be like: but if they have 500 pts, can redeem 100 pts, the max amount they can redeem is just 100 pts. etc etc
                        {
                            maxAmtOfPointsToRedeem = customerWithOrderToProcess.Rewards.Points;
                        }

                        Console.WriteLine($"You can redeem up to a maximum of {maxAmtOfPointsToRedeem} points.");
                        Console.Write("Would you like to redeem your points [ Y / N ] ?: ");
                        string redeemOrNot = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(redeemOrNot))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error: Invalid input. Please enter again!");
                            Console.WriteLine();
                        }

                        if (redeemOrNot.ToLower() == "y")
                        {
                            Console.Write("Enter amount of points to redeem: ");
                            string stringRedeemPts = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(stringRedeemPts))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Error: Invalid input. Please enter again!");
                                Console.WriteLine();
                                continue;
                            }

                            int intRedeemPts = Convert.ToInt32(stringRedeemPts);

                            if (intRedeemPts > maxAmtOfPointsToRedeem)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Error: You can only redeem up to a maximum of {maxAmtOfPointsToRedeem} points!");
                                Console.WriteLine();
                                continue;

                            }
                            iceCreamPrice -= intRedeemPts * pointsToMoney;
                            customerWithOrderToProcess.Rewards.RedeemPoints(intRedeemPts); //RedeemPoints is method in class. Will basically take the total points that customer currently have - amt of points they redeemed
                            Console.WriteLine();
                            Console.WriteLine($"You have successfully redeemed {intRedeemPts} points!");
                            Console.WriteLine();
                            break;
                        }
                        else if (redeemOrNot.ToLower() == "n")
                        {
                            Console.WriteLine();
                            Console.WriteLine("You have chosen not to redeem any points.");
                            Console.WriteLine();
                            break; //bc they do not want redeem
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Error: Invalid input. Please only enter [ Y / N ]!");
                            Console.WriteLine();

                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
                        Console.WriteLine();
                        // Additional handling if needed
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Please enter a valid value.");
                        Console.WriteLine();
                        // Additional handling if needed
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Please enter a valid value.");
                        Console.WriteLine();
                        // Additional handling if needed
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Invalid input. Please enter a valid value.");
                        Console.WriteLine();
                        // Additional handling if needed
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You are not eligible for points redemption.");
            }

            //Now display all new info like final total bill
            Console.WriteLine();
            Console.WriteLine($"Your Final Total Bill: ${iceCreamPrice}");
            //Console.WriteLine($"Your Points before payment: {customerWithOrderToProcess.Rewards.Points}");
            Console.WriteLine($"Your Tier before payment: {customerWithOrderToProcess.Rewards.Tier}");
            Console.WriteLine($"Your Punch Card before payment: {customerWithOrderToProcess.Rewards.PunchCard}");
            Console.WriteLine();

            //Promot customer press any key makej payment
            Console.Write("Enter any key to make payment: ");
            Console.ReadKey();

            //When any key clicked, these code executes
            Console.WriteLine();


            orderToProcess.TimeFulfilled = DateTime.Now;

            Console.WriteLine($"Payment is successfully completed as of {orderToProcess.TimeFulfilled.ToString()}");
            Console.WriteLine();

            // Only when they successfully make payment, then they can earn the points
            customerWithOrderToProcess.Rewards.AddPoints(Convert.ToInt32(Math.Floor(iceCreamPrice)));

            PunchIceCream(orderToProcess, customerWithOrderToProcess.Rewards, redeemPunchOrNo);
            // The tier should be automatically updated within the Rewards class
            Console.WriteLine($"Your Points after payment: {customerWithOrderToProcess.Rewards.Points}");
            Console.WriteLine($"Your Tier after payment: {customerWithOrderToProcess.Rewards.Tier}");
            Console.WriteLine($"Your Punch Card after payment: {customerWithOrderToProcess.Rewards.PunchCard}");
            Console.WriteLine();



            //things to do once payment is completed
            iceCreamPrice = 0.0; //reset for new customer
            customerWithOrderToProcess.OrderHistory.Add(orderToProcess);
            customerWithOrderToProcess.CurrentOrder = null; //check out alrd, current order becomes empty
                                                            //^^ if want append data here, can make dictionary with key = customerWithOrderToProcess then value = orderToProcess???

            //continueagain FROM HERE. APPEND THE COMPLETED ORDER TO ORDER CSV AT THE EXIT 0 THERE. AND THEN TEST THIS ADV 1 OUT BC U COMPLETE BUT HAVENT TEST YETTT
        }



        // ------- ADVANCED 2 ( weiying )-------------------------------------------------------------------------------------------------------------------------------------
      
        static void DisplayAmountForTheYear(List<Customer> cList)
        {
            List<int> amountList = new List<int>();
            int year;
            double[] monthlyPrice = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<string> months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            while (true)
            {
                try
                {
                    Console.Write("Enter the year: ");
                    year = Convert.ToInt32(Console.ReadLine());
                    if (year > 3000 || year < 2000)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter a year in the 2000s");
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Please enter your desired year. Thank you.");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Something went wrong. Please enter again.");
                    Console.WriteLine();
                }
            }
            // calculating amount for all current orders
            foreach (Customer c in cList)
            {
                Order currentOrder = c.CurrentOrder;
                if (currentOrder != null)
                {
                    int CurrentOrderYear = currentOrder.TimeReceived.Year;
                    if (CurrentOrderYear == year)
                    {
                        int currentOrderMonth = currentOrder.TimeReceived.Month;
                        monthlyPrice[currentOrderMonth - 1] += currentOrder.CalculateTotal();
                    }
                }
            }
            // calculating amount for order history
            foreach (Customer c in cList)
            {
                Customer selectedCustomer = c;
                DisplayOrderDetailsOfCustomer(customerList, selectedCustomer);
                List<Order> orderHistory = c.OrderHistory;
                foreach (Order o in orderHistory)
                {
                    int orderHistoryYear = o.TimeReceived.Year;
                    if (orderHistoryYear == year)
                    {
                        int orderHistoryMonth = o.TimeReceived.Month;
                        monthlyPrice[orderHistoryMonth - 1] += o.CalculateTotal();
                    }
                }
            }
            int index = 0;
            foreach (double p in monthlyPrice)
            {
                Console.WriteLine($"{months[index]} {year}:   ${p.ToString("0.00")}");
                index++;
            }
        }
        







        // ------------------ Additional methods -----------------------------------------------------------------------

        // RETURNS FLAVOUR OPTIONS LIST
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


        // RETURNS TOPPING OPTIONS LIST
        static List<string> ToppingOptionsAvail()
        {
            List<string> toppingOptions = new List<string>();
            using (StreamReader sr = new StreamReader("toppings.csv"))
            {
                // read header first
                string s = sr.ReadLine();

                while ((s = sr.ReadLine()) != null)
                {
                    string[] items = s.Split(',');
                    toppingOptions.Add(items[0].ToLower());

                }
            }
            return toppingOptions;
        }


        static bool PremiumWaffle(string flavourName)
        {
            List<string> premiumFlavours = new List<string> { "red velvet", "charcoal", "pandan" };
            return premiumFlavours.Contains(flavourName);
        }

        static bool PremiumFlavours(string flavourName)
        {
            List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
            return premiumFlavours.Contains(flavourName);
        }

        static bool DoesToppingExists(string toppingName)
        {
            List<string> toppingsOffered = ToppingOptionsAvail();
            if (!toppingsOffered.Contains(toppingName.ToLower()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool IsItDipped(string dipped)
        {
            if (dipped == "y")
            {
                return true;
            }
            return false;
        }

        static int ReturnLastOrderID()
        {
            //Customer selectedCustomer = SelectMember(customerList);
            /* try
             {*/
            using (StreamReader sr = new StreamReader("orders.csv"))
            {
                // read header first
                string s = sr.ReadLine();

                // read the rest 
                while ((s = sr.ReadLine()) != null)
                {
                    //try
                    //{
                    string[] data = s.Split(',');
                    int orderId = Convert.ToInt32(data[0]);
                    /*int memberId = Convert.ToInt32(data[1]);
                    DateTime timeReceived = Convert.ToDateTime(data[2]);
                    DateTime timeFulfilled = Convert.ToDateTime(data[3]);
                    string option = Convert.ToString(data[4]);
                    int scoops = Convert.ToInt32(data[5]);
                    bool dipped = false;
                    if (data[6] != null) // If not null means its a cone since got mention dip or not
                    {
                        string item6 = data[6].Trim().ToLower();
                        if (item6 == "true")
                        {
                            dipped = true;
                        }
                    }


                    //string waffleFlavour = Convert.ToString(data[7]); */
                    lastOrderId = Math.Max(lastOrderId, orderId);


                    /*List<Flavour> flavours = new List<Flavour>();
                    for (int i = 8; i <= 10; i++)
                    {
                        if (data[i] != null)
                        {
                            flavours.Add(new Flavour(data[i], PremiumFlavours(data[i]), 1)); // ----- NEED CHANGE QUANTITY HERE OR NO?? --------
                        }
                    }


                    List<Topping> toppings = new List<Topping>();
                    for (int i = 11; i <= 14; i++)
                    {
                        if (data[i] != null)
                        {
                            toppings.Add(new Topping(data[i]));
                        }
                    }


                    Order order = new Order(orderId, timeReceived);

                    order.TimeFulfilled = timeFulfilled;
                    IceCream iceCream = null;

                    if (option.ToLower() == "cup")
                    {
                        iceCream = new Cup(option, scoops, flavours, toppings);
                    }
                    else if (option.ToLower() == "cone")
                    {
                        iceCream = new Cone(option, scoops, flavours, toppings, dipped);
                    }
                    else if (option.ToLower() == "waffle")
                    {
                        iceCream = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
                    }

                    order.AddIceCream(iceCream);

                    if (selectedCustomer != null)
                    {
                        if (selectedCustomer.OrderHistory == null)
                        {
                            selectedCustomer.OrderHistory = new List<Order>(); //So if is null, then we initilaize it first
                        }
                        else
                        {
                            selectedCustomer.OrderHistory.Add(order);
                        }
                    }


                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.WriteLine();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error detected: {0}", ex.Message);
                    Console.WriteLine();
                }
                catch (Exception ex) //general errors
                {
                    Console.WriteLine();
                    Console.WriteLine("Error detected: {0}", ex.Message);
                    Console.WriteLine();
                }
            }
        }
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine();
        Console.WriteLine("Error detected. File could not be found!");
        Console.WriteLine();
    }
    catch (IOException)
    {
        Console.WriteLine();
        Console.WriteLine("Error detected. File could not be read!");
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine();
        Console.WriteLine("Error detected: {0}", ex.Message);
        Console.WriteLine();
    }*/
                }
            }
            return lastOrderId;
        }

        static void DisplayCurrentOrder(Customer customerId)
        {
            Console.WriteLine();
            Console.WriteLine("{0,10}-----------------------", "");
            Console.WriteLine("{0,11}Current Order Details ", "");
            Console.WriteLine("{0,10}-----------------------", "");
            if (customerId.CurrentOrder != null)
            {
                Console.WriteLine(customerId.CurrentOrder.ToString());
            }
            else
            {
                Console.WriteLine("Information regarding current order are not available.");
            }
        }

        static void DisplayOrderHistory(Customer customerId)
        {
            Console.WriteLine("{0,10}-----------------------", "");
            Console.WriteLine("{0,11}Order History Details ", "");
            Console.WriteLine("{0,10}-----------------------", "");
            if (customerId.OrderHistory.Count() > 0)
            {
                foreach (Order oh in customerId.OrderHistory)
                {
                    Console.WriteLine(oh.ToString());
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Information regarding order history are not available.");
                Console.WriteLine("You have exited the process of displaying order history.");
                Console.WriteLine();
            }
        }
    }
}


