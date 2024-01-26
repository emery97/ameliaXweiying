//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh
//==========================================================

using System;
using PairAssignment;
using System.Globalization;

class Program
{
    static List<Customer> customerList = new List<Customer>();
    static Queue<Order> goldMemberQueue = new Queue<Order>();
    static Queue<Order> regularMemberQueue = new Queue<Order>();
    static int currentOrderId = 0;
    static int lastOrderId = 0;

    static void Main(string[] args)
    {
        InitializeCustomerList(customerList);
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
                    DisplayOrderDetailsOfCustomer(customerList);
                    Console.WriteLine();
                }
                else if (option == "6")
                {
                    ModifyOrderDetails(customerList);
                }
                else if (option == "0")
                {
                    //APPEND EVERYTHING HERE
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
            Console.WriteLine(new string('-', 20) + " M E N U " + new string('-', 20));
            Console.WriteLine("[1] List all customers");
            Console.WriteLine("[2] List all current orders");
            Console.WriteLine("[3] Register a new customer");
            Console.WriteLine("[4] Create a customer’s order");
            Console.WriteLine("[5] Display order details of a customer");
            Console.WriteLine("[6] Modify order details");
            Console.WriteLine("[0] Exit");
            Console.WriteLine(new string('-', 48));
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
                    int ID = Convert.ToInt32(Console.ReadLine());

                    if (ID == 0)
                    {
                        Console.WriteLine("You have exited.");
                        break;
                    }

                    foreach (Customer c in customerList)
                    {
                        if (c.MemberId == ID)
                        {
                            found = true;
                            chosenCustomer = c;
                            break; // Break out of the foreach loop, because we've found the customer.
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine();
                        Console.WriteLine(new string('*', 60));
                        Console.WriteLine("Invalid Member ID! Member ID not found!");
                        Console.WriteLine(new string('*', 60));
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
                    Console.WriteLine(new string('*', 60));
                    Console.WriteLine("We only accept integers.");
                    Console.WriteLine("Invalid Input! Please enter a valid Member ID!");
                    Console.WriteLine(new string('*', 60));
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
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            foreach (var customer in cList)
            {
                Console.WriteLine("{0,-14}{1,-17}{2,-21}{3,-16}{4,-14}{5}", customer.Name, customer.MemberId, customer.Dob.ToString("dd/MM/yyyy"), customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
                Console.WriteLine("");
            }

            Console.WriteLine("");
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
                    Console.WriteLine("\nYou have chosen to not register a new customer.\n");
                    Console.WriteLine();
                    return;
                }
                if (string.IsNullOrWhiteSpace(nameInput))
                {
                    Console.WriteLine();
                    Console.WriteLine("There has been an error. Please enter again!");
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
                    Console.WriteLine("There has been an error. Please enter again!");
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
                    Console.WriteLine("There has been an error. Please enter again!");
                    Console.WriteLine();
                    continue; // so that can ask user again
                }
                else
                {
                    try //for validation,handle errors
                    {
                        memberID = Convert.ToInt32(stringMemberId);

                        bool doesMemberIdExist = false;

                        foreach (Customer customerMemberId in cList)
                        {
                            if (memberID == customerMemberId.MemberId) //Check if person enter a pre existing id that already is a member
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Invalid input! This member ID '{memberID}' already belongs to an existing customer!");
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
                        Console.WriteLine("Invalid input. Please enter again!");
                        Console.WriteLine();
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter again!");
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
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter again!");
                    Console.WriteLine();
                    continue; // ask the user again
                }
                try
                {
                    if (DateTime.TryParseExact(dobInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                    {
                        if (dob > DateTime.Now)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input. Date Of Birth has to be before today!");
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
                        Console.WriteLine();
                        Console.WriteLine("There has been a formatting error. Please enter again in format of (DD/MM/YYYY)!");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("There has been an error detected: {0}", ex.Message);
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
            customer.Rewards = pointcard; //points, tier etc would be 0 and ordinary since is a new customer

            try
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
                Console.WriteLine("Error detected. The file where new data needs to be appended to, could not be found!");
                Console.WriteLine();
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error detected. Do ensure that file is not open in another program.");
                Console.WriteLine("Once done, please try again!");
                Console.WriteLine();
            }

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
                    Console.WriteLine("Invalid input! Please enter a valid member ID!");
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
                    Console.WriteLine("Invalid input! Please enter a valid member ID!");
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
                    Console.WriteLine("Invalid input! Please enter a valid member ID!");
                    Console.WriteLine();

                    selectedCustomer = SelectMember(customerList);
                    stringCustomerMemberIdAgain = Convert.ToString(selectedCustomer.MemberId);


                    if (string.IsNullOrWhiteSpace(stringCustomerMemberIdAgain))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input! Please enter a valid member ID!");
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

            Order newOrder = selectedCustomer.CurrentOrder; //making sure the new order they r making is set to their currentorder
            if (newOrder == null)
            {
                newOrder = selectedCustomer.MakeOrder();
                selectedCustomer.CurrentOrder = newOrder;
            }

            bool orderCreated = false;
            while (!orderCreated)
            {
                CreatingIceCream(newOrder);
                bool addAgain = false;
                while (!addAgain)
                {
                    Console.Write("\nWould you like to add another ice cream? [ Y / N ] : ");
                    string orderAgain = Console.ReadLine().ToLower();

                    if (orderAgain == "n")
                    {
                        if (newOrder.IceCreamList.Count == 0)
                        {
                            selectedCustomer.CurrentOrder = null;
                        }

                        if (selectedCustomer.Rewards.Tier == "Gold")
                        {
                            goldQueue.Enqueue(newOrder);

                            Console.WriteLine();
                            Console.WriteLine("Order success! It has been added to the gold queue!");
                            Console.WriteLine();
                        }
                        else
                        {
                            regularQueue.Enqueue(newOrder);

                            Console.WriteLine();
                            Console.WriteLine("Order success! It has been added to the regular queue!");
                            Console.WriteLine();
                        }

                        addAgain = true;
                        orderCreated = true;
                    }

                    else if (orderAgain == "y")
                    {
                        addAgain = true;
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please try again!");
                        Console.WriteLine();
                    }
                }
            }
        }

        //To add the ice creams
        static void CreatingIceCream(Order order)
        {
            while (true)
            {
                List<string> options = new List<string> { "cup", "waffle", "cone" };
                List<string> flavourOptionsAvail = FlavourOptionsAvail();
                List<Flavour> flavours = new List<Flavour>();
                Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();
                List<Topping> toppings = new List<Topping>();
                IceCream newIceCream = null;
                bool doesOptionExists = false; //check if icecream option person selected exists

                Console.WriteLine();
                Console.WriteLine("We offer cup, cone and waffle.");
                Console.Write("Please enter which you would like: ");
                string iceCreamOption = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(iceCreamOption))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please try again!");
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
                    Console.WriteLine("Invalid input. Please enter your ice cream option again!");
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
                            Console.WriteLine("Invalid input. Please try again!");
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
                            Console.WriteLine("Invalid input. Please try again!");
                            Console.WriteLine();
                            continue;
                        }
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please try again!");
                        Console.WriteLine();
                        continue;
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine("There has been an error detected: {0}", ex.Message);
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

                    if (selectedFlavours.Length != scoopsAmt) //meaning they choose eg 3 scoops but write desired flavours as Ube, Strawberry only.
                    {
                        Console.WriteLine("\n" + new string('*', 60));
                        Console.WriteLine("Invalid input. The number of flavours should match the amount of scoops: {0}.", scoopsAmt);
                        Console.WriteLine("Please enter again!");
                        Console.WriteLine(new string('*', 60) + "\n");
                        continue;
                    }
                    else if (selectedFlavours.All(item => flavourOptionsAvail.Contains(item)) == false)
                    {
                        Console.WriteLine("\n" + new string('*', 60));
                        Console.WriteLine("Vanilla | Chocolate | Strawberry | Durian | Ube | Sea Salt");
                        Console.WriteLine("Please flavours that are listed above");
                        Console.WriteLine(new string('*', 60) + "\n");
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
                                    toppings.Add(new Topping("Sprinkles"));
                                    toppingsExist = true;
                                }

                                else if (toppingName == "mochi")
                                {
                                    toppings.Add(new Topping("Mochi"));
                                    toppingsExist = true;
                                }

                                else if (toppingName == "sago")
                                {
                                    toppings.Add(new Topping("Sago"));
                                    toppingsExist = true;
                                }

                                else if (toppingName == "oreos")
                                {
                                    toppings.Add(new Topping("Oreos"));
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
                        newIceCream = new Cup("Cup", scoopsAmt, flavours, toppings);
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
                                newIceCream = new Cone("Cone", scoopsAmt, flavours, toppings, true);
                                dippedOrNot = true;
                            }

                            else if (dipped == "n")
                            {
                                newIceCream = new Cone("Cone", scoopsAmt, flavours, toppings, false);
                                dippedOrNot = true;
                            }

                            if (!dippedOrNot)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid input. Please enter again!");
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
                                    newIceCream = new Waffle("Waffle", scoopsAmt, flavours, toppings, "Red Velvet");
                                    waffleExists = true;
                                }
                                else if (waffleFlavour == "charcoal")
                                {
                                    newIceCream = new Waffle("Waffle", scoopsAmt, flavours, toppings, "Charcoal");
                                    waffleExists = true;
                                }
                                else if (waffleFlavour == "pandan")
                                {
                                    newIceCream = new Waffle("Waffle", scoopsAmt, flavours, toppings, "Pandan");
                                    waffleExists = true;
                                }
                            }

                            else if (premiumOrNot == "n") //aka they dw premium, so stick with original
                            {
                                newIceCream = new Waffle("Waffle", scoopsAmt, flavours, toppings, "Original");
                                waffleExists = true;
                            }

                            if (!waffleExists)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid input. Please enter desired waffle flavour again!");
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
        static void DisplayOrderDetailsOfCustomer(List<Customer> cList)
        {
            // List the customer 
            ListingAllCustomer(cList);
            Customer selectedCustomer = SelectMember(cList);

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

            using (StreamReader sr = new StreamReader("orders.csv"))
            {
                string s;

                sr.ReadLine(); // Skip the header

                // Read each line representing an order
                while ((s = sr.ReadLine()) != null)
                {
                    Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();
                    string[] items = s.Split(',');
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

            // Now display current order and order history of the selected customer
            DisplayCurrentOrder(selectedCustomer);
            Console.WriteLine();
            DisplayOrderHistory(selectedCustomer);

        }


        //------- BASIC 6 ( Weiying ) -------------------------------------------------------------------------------------------------------------------------------------- //
        static void ModifyOrderDetails(List<Customer> cList)
        {
            if (cList == null || cList.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Error detected. No customers could be found!");
                Console.WriteLine();
                Console.WriteLine("Hence, you have exited the process of modifying order details.");
                Console.WriteLine();
                return; //so can exit
            }

            ListingAllCustomer(cList);

            int findCustomerMemberId = 0; //initialize first

            while (true)
            {
                Customer selectedCustomer = SelectMember(customerList);
                try
                {
                    string stringCustomerMemberId = Convert.ToString(selectedCustomer.MemberId);

                    if (string.IsNullOrWhiteSpace(stringCustomerMemberId))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter member ID again!");
                        Console.WriteLine();
                        continue;
                    }

                    findCustomerMemberId = Convert.ToInt32(stringCustomerMemberId);
                    if (findCustomerMemberId == 0)
                    {
                        return;
                    }

                    Console.WriteLine();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter member ID again!");
                    Console.WriteLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter member ID again!");
                    Console.WriteLine();
                }
            }

            Customer selectedMemberId = SelectMember(customerList);


            if (selectedMemberId == null || selectedMemberId.CurrentOrder == null || selectedMemberId.CurrentOrder.IceCreamList == null || selectedMemberId.CurrentOrder.IceCreamList.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Error detected. You can only modify ice cream if you have existsing orders!");
                Console.WriteLine();
                return; // exit
            }

            DisplayCurrentOrder(selectedMemberId);
            Console.WriteLine();


            int option = 0;
            bool modifyOption = false;
            while (!modifyOption)
            {
                Console.WriteLine("[1] Modify Ice Cream Order");
                Console.WriteLine("[2] Add an Ice Cream to Order");
                Console.WriteLine("[3] Delete an existing Ice Cream");
                Console.WriteLine("[0] Exit to the main menu");
                Console.Write("Enter your option: ");

                try
                {
                    string userOption = Console.ReadLine();
                    if (string.IsNullOrEmpty(userOption))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter again!");
                        Console.WriteLine();
                        continue;
                    }

                    option = Convert.ToInt32(userOption);
                    modifyOption = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter again!");
                    Console.WriteLine();
                }

            }

            if (option == 1 || option == 2 || option == 3)
            {

                Order order = selectedMemberId.CurrentOrder;

                if (option == 1)
                {
                    DisplayIndexOfIceCream(selectedMemberId);
                    while (true)
                    {
                        Console.Write("Enter index of the ice cream to modify ['0' to exit]: ");
                        try
                        {
                            string icIndexToModify = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(icIndexToModify))
                            {

                                Console.WriteLine();
                                Console.WriteLine("Invalid input. Please enter again!");
                                Console.WriteLine();
                                continue;
                            }
                            int selectedIC = Convert.ToInt32(icIndexToModify) - 1;  //selectedIC = selected ice cream

                            if (selectedIC == -1) //Aka user enter 0
                            {
                                Console.WriteLine();
                                Console.WriteLine("You have exited the process of modifying ice cream.");
                                Console.WriteLine();
                                return;
                            }
                            if (selectedIC >= 0 && selectedIC < selectedMemberId.CurrentOrder.IceCreamList.Count())
                            {
                                selectedMemberId.CurrentOrder.ModifyIceCream(selectedIC);
                                DisplayCurrentOrder(selectedMemberId);
                                Console.WriteLine();
                                Console.WriteLine("Success!");
                                Console.WriteLine();
                                break;
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
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input. Please enter again!");
                            Console.WriteLine();
                        }
                    }
                }
                else if (option == 2)
                {
                    CreatingIceCream(order);
                    DisplayCurrentOrder(selectedMemberId);
                    order.Id = currentOrderId;
                    Console.WriteLine("Success!");
                }
                else if (option == 3)
                {
                    while (true)
                    {
                        DisplayIndexOfIceCream(selectedMemberId);
                        Console.Write("Enter the index of ice cream to delete ['0' to exit]: ");
                        string icIndexToDelete = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(icIndexToDelete))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input. Please enter again!");
                            Console.WriteLine();
                            continue;
                        }
                        int selectedIC = Convert.ToInt32(icIndexToDelete) - 1;
                        if (selectedIC == -1) //aka 0
                        {
                            Console.WriteLine("You have exited the process of deleting ice cream.");
                            return;  //exit
                        }
                        else if (selectedIC >= 0 && selectedIC < selectedMemberId.CurrentOrder.IceCreamList.Count())
                        {
                            if (selectedMemberId.CurrentOrder.IceCreamList.Count() > 1)
                            {
                                selectedMemberId.CurrentOrder.DeleteIceCream(selectedIC);
                                Console.WriteLine("Ice cream with index {0} has successfully been deleted!", selectedIC + 1);
                                DisplayCurrentOrder(selectedMemberId);
                                break;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid input. Please enter again!");
                                Console.WriteLine();
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input. Please enter again!");
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Please enter again!");
                Console.WriteLine();
            }
        }

        //This is needed for basic 6
        static void DisplayIndexOfIceCream(Customer customerId) //for optiion 1 when user does basic 6
        {
            IceCream iceCreamOrder = null; //initialize
            int iceCreamIndex = 1;
            Console.WriteLine();
            Console.WriteLine("Ice Cream Index");
            Console.WriteLine("---------------");
            foreach (IceCream iceCream in customerId.CurrentOrder.IceCreamList)
            {
                iceCreamOrder = iceCream;
                Console.WriteLine("{0}. ", iceCreamIndex);
                Console.WriteLine(iceCreamOrder.ToString(), iceCreamIndex);
                iceCreamIndex++;
            }
        }


        //Additional methods

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

        static bool DoesFlavourExists(string flavourNames)
        {
            List<string> flavoursOffered = FlavourOptionsAvail();

            if (!flavoursOffered.Contains(flavourNames.ToLower()))
            {
                return false;
            }
            return true;
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


        static int ReadingCSVOrders(List<Customer> cList)
        {
            Customer selectedCustomer = SelectMember(customerList);

            try
            {
                using (StreamReader sr = new StreamReader("orders.csv"))
                {
                    // read header first
                    string s = sr.ReadLine();

                    // read the rest 
                    while ((s = sr.ReadLine()) != null)
                    {
                        try
                        {
                            string[] data = s.Split(',');
                            int orderId = Convert.ToInt32(data[0]);
                            int memberId = Convert.ToInt32(data[1]);
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


                            string waffleFlavour = Convert.ToString(data[7]);
                            lastOrderId = Math.Max(lastOrderId, orderId);


                            List<Flavour> flavours = new List<Flavour>();
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
                Console.WriteLine("You have exited the process of displaying order history.");
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


