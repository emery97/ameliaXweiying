
using PairAssignment;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

class Program
{
    static List<Customer> customerList = new List<Customer>();
    static Queue<Order> goldMemberQueue = new Queue<Order>();  // Utilizing the Order class because i think we are focusing on queue management???????
    static Queue<Order> regularMemberQueue = new Queue<Order>();
    static int currentOrderId = 0;
    static int lastOrderId = 0;

    // Judging from the option description, this program should be catering to otto's admin, make sure all the prompts are catered to admin

    // QUESTION 1 (Raeann Tai) -------------------------------------------------------------------------------------------------------------------------------------- //
    static void Main(string[] args)
    {
        // ENABLER -------------------------------------------------------------------------------------- //
        InitializeCustomerList(customerList);
        //Customer chosenCustomer = SelectMember(customerList);
        while (true)
        {
            DisplayMenu();
            // After the menu has been displayed...
            //currentOrderId = ReadOrderDetails(customerList);
            Console.Write("Enter an option: ");
            try
            {
                string option = Console.ReadLine();
                Console.WriteLine();
                if (option == "1")
                {
                    ListAllCustomer(customerList);
                    Console.WriteLine();
                }
                else if (option == "2")
                {
                    //ReadOrderDetails(customerList);
                    ListAllCurrentOrders(customerList, goldMemberQueue, regularMemberQueue);
                    Console.WriteLine();
                }
                else if (option == "3")
                {
                    RegisterANewCustomer(customerList);
                }
                else if (option == "4")
                {
                    ListAllCustomer(customerList);
                    CreateCustomerOrder(customerList, goldMemberQueue, regularMemberQueue);
                }
                else if (option == "5")
                {
                    DisplayOrderDetails(customerList);
                    Console.WriteLine();
                }
                else if (option == "6")
                {
                    ModifyOrderDetails(customerList);
                }
                else if (option == "0")
                {
                    //APPEND EVERYTHING HERE
                    Console.WriteLine("\nExiting...");
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine("*** Invalid Input! Please re-enter a valid Input! ***");
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("*****************************************************");
                Console.WriteLine("*** Invalid Input! Please re-enter a valid input! ***");
                Console.WriteLine("*****************************************************");
                Console.WriteLine();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine();
                Console.WriteLine("*****************************************************");
                Console.WriteLine("*** Invalid Input! Please re-enter a valid input! ***");
                Console.WriteLine("*****************************************************");
                Console.WriteLine();
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("*****************************************************");
                Console.WriteLine("*** Invalid Input! Please re-enter a valid input! ***");
                Console.WriteLine("*****************************************************");
                Console.WriteLine();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine();
                Console.WriteLine("*****************************************************");
                Console.WriteLine("*** Invalid Input! Please re-enter a valid input! ***");
                Console.WriteLine("*****************************************************");
                Console.WriteLine();
            }
            catch (Exception ex) // General exception catch block for other unforeseen errors
            {
                Console.WriteLine("An error occurred: {0}", ex.Message);
            }
        }


        // MAIN MENU ---------------------------------------------------------------------------------- //
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
                        chosenCustomer = c;
                        break; // Break out of the foreach loop, because we've found the customer.
                    }
                }
                if (chosenCustomer != null)
                {
                    break; // Break out of the while loop, because a customer has been chosen.
                }
                else
                {
                    Console.WriteLine("Please enter valid ID."); // Only display this if no customer was found.
                }
            }
            return chosenCustomer;
        }

        static void InitializeCustomerList(List<Customer> cList)
        {
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                // read header first
                string s = sr.ReadLine();
                string[] header = s.Split(',');
                // read the rest 
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

        static void ListAllCustomer(List<Customer> cList)
        {
            Console.WriteLine("{0,-12}{1,-15}{2,-19}{3,-14}{4,-12}{5}", "Name", "Member ID", "Date of Birth", "Tier", "Points", "Punch Card");
            foreach (var customer in cList)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------");
                Console.WriteLine("{0,-12}{1,-15}{2,-19}{3,-14}{4,-12}{5}", customer.Name, customer.MemberId, customer.Dob.ToString("dd/MM/yyyy"), customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
            }

            Console.WriteLine("-----------------------------------------------------------------------------------");
        }

        // QUESTION 2 (Ng Joe Yi) -------------------------------------------------------------------------------------------------------------------------------------- //
        static void ListAllCurrentOrders(List<Customer> cList, Queue<Order> goldQueue, Queue<Order> regularQueue)
        {
            // i dont need to queue anymore because question 4 has already queued them. i just need to display!
            // next we will need to display the Gold Member's queue order details 
            Console.WriteLine("{0,13}----------", "");
            Console.WriteLine("{0,13}Gold Queue", "");
            Console.WriteLine("{0,13}----------", "");
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
                Console.WriteLine("There are no Gold Members in line.");
                Console.WriteLine();
            }

            // lastly, we will need to display the Regular Member's queue order details 
            Console.WriteLine("{0,13}-------------", "");
            Console.WriteLine("{0,13}Regular Queue", "");
            Console.WriteLine("{0,13}-------------", "");
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
                Console.WriteLine("There are no Regular Members in line.");
            }
        }

        // QUESTION 3 (Raeann Tai) -------------------------------------------------------------------------------------------------------------------------------------- //
        static void RegisterANewCustomer(List<Customer> cList)
        {
            string name = "";
            while (true)
            {
                Console.Write("Enter Customer's Name (0 to exit): ");

                string nameInput = Console.ReadLine();
                if (nameInput == "0")
                {
                    Console.WriteLine("\nReturning to Main Menu...\n");
                    return;
                }
                if (string.IsNullOrWhiteSpace(nameInput))
                {
                    Console.WriteLine();
                    Console.WriteLine("********************************************");
                    Console.WriteLine("*** Error! Please re-enter a valid name! ***");
                    Console.WriteLine("********************************************");
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
                    // Handle the case where the input is empty
                    Console.WriteLine();
                    Console.WriteLine("********************************************");
                    Console.WriteLine("*** Error! Please re-enter a valid name! ***");
                    Console.WriteLine("********************************************");
                    Console.WriteLine();
                }
            }

            int memberID;
            while (true)
            {
                Console.Write("Enter Customer's Member ID: ");

                string stringMemberId = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringMemberId))
                {
                    Console.WriteLine();
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine("*** Invalid Input! Please re-enter a valid Member ID! ***");
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine();
                    continue; // Reprompt the user
                }
                else
                {
                    try
                    {
                        memberID = Convert.ToInt32(stringMemberId);

                        bool memberIdExist = false;

                        foreach (Customer customerMemberId in cList)
                        {
                            if (memberID == customerMemberId.MemberId)
                            {
                                Console.WriteLine();
                                Console.WriteLine("*********************************************************");
                                Console.WriteLine("*** Error! Member ID belongs to an existing Customer! ***");
                                Console.WriteLine("*********************************************************");
                                Console.WriteLine();
                                memberIdExist = true;
                                break;
                            }
                        }
                        if (!memberIdExist)
                        {
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine("*** Invalid Input! Please re-enter a valid Member ID! ***");
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine();
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine("*** Invalid Input! Please re-enter a valid Member ID! ***");
                        Console.WriteLine("*********************************************************");
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
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine("*** Invalid Date Time! Date Of Birth has to be before today ***");
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine();
                    continue; // Reprompt the user
                }
                try
                {
                    if (DateTime.TryParseExact(dobInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                    {
                        if (dob > DateTime.Now)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*****************************************************************");
                            Console.WriteLine("*** Invalid Date Time.! Date Of Birth has to be before today! ***");
                            Console.WriteLine("*****************************************************************");
                            Console.WriteLine();
                            continue; // Reprompt the user
                        }
                        else
                        {
                            break; // Valid date, exit the loop
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine("*** Invalid Format! Please enter in format DD/MM/YYYY ***");
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("An error occurred while processing your Order: {0}", ex.Message);
                    Console.WriteLine();
                }
            }
            // create a customer object with the information given
            string dateOnly = dob.ToString("dd/MM/yyyy");
            Customer customer = new Customer(name, memberID, dob);
            cList.Add(customer);

            // create a pointcard object
            PointCard pointcard = new PointCard();

            // assign point card object to customer 
            customer.Rewards = pointcard;

            try
            {
                // append the customer information to the customers.csv file
                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5}", name, memberID, dob, "Ordinary", customer.Rewards.Points, customer.Rewards.PunchCard); //"Ordinary" never use customer.Rewards.Points bc customer.Rewards is a pointcard object. Then pointcard obj have pointcard's constructor of Pointcard(int, int). The tier is not in its parameters. So only can access the int aka points and punchcard via customer.Rewards.Points etc and tier cannot. 
                    Console.WriteLine("Registration for {0} is successful!", customer.Name);
                    Console.WriteLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("*************************************************************************************");
                Console.WriteLine("*** Error: The database file is missing. Please contact a manager for assistance! ***");
                Console.WriteLine("*************************************************************************************");
                Console.WriteLine();
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("*****************************************************************************************");
                Console.WriteLine("*** Error! Please ensure the Excel file is not open in another program and try again! ***");
                Console.WriteLine("*****************************************************************************************");
                Console.WriteLine();
            }

        }

        // QUESTION 4 (Raeann Tai) -------------------------------------------------------------------------------------------------------------------------------------- //
        /*
        static Customer SelectMemberID(int FindCId)
        {
            Customer customerMemberId = null;
            foreach (Customer customer in customerList)
            {
                if (customer.MemberId == FindCId)
                {
                    customerMemberId = customer; // Return the customer immediately when found
                    break;
                }
            }

            if (customerMemberId == null)
            {
                return null;
            }
            else
            {
                return customerMemberId;
            }
        }*/
        static void CreateCustomerOrder(List<Customer> cList, Queue<Order> goldQueue, Queue<Order> regularQueue)
        {
            int customerMemberId;
            Customer selectedCustomer = SelectMember(customerList);

            while (true)
            {
                Console.Write("Enter Customer's Member ID (0 to exit): ");  // MAKE THIS PROMPT IF NULL ----------------------------------
                string stringCustomerMemberId = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringCustomerMemberId))
                {
                    Console.WriteLine();
                    Console.WriteLine("*************************************************************");
                    Console.WriteLine("*** Invalid Member ID! Please re-enter a valid Member ID! ***");
                    Console.WriteLine("*************************************************************");
                    Console.WriteLine();
                    continue;
                }
                customerMemberId = Convert.ToInt32(stringCustomerMemberId);

                if (customerMemberId == 0)
                {
                    Console.WriteLine("\nReturning to Main Menu...\n");
                    return;
                }

                //selectedCustomer = SelectMember(customerList);

                if (selectedCustomer == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("*************************************************************");
                    Console.WriteLine("*** Invalid Member ID! Please re-enter a valid Member ID! ***");
                    Console.WriteLine("*************************************************************");
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
                    Console.WriteLine("*************************************************************");
                    Console.WriteLine("*** Invalid Member ID! Please re-enter a valid Member ID! ***");
                    Console.WriteLine("*************************************************************");
                    Console.WriteLine();
                    Console.Write("Enter Customer's Member ID to be order (0 to exit): ");
                    string stringCustomerMemberId1 = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(stringCustomerMemberId1))
                    {
                        Console.WriteLine();
                        Console.WriteLine("*************************************************************");
                        Console.WriteLine("*** Invalid Member ID! Please re-enter a valid Member ID! ***");
                        Console.WriteLine("*************************************************************");
                        Console.WriteLine();
                    }
                    customerMemberId = Convert.ToInt32(stringCustomerMemberId1);
                    if (customerMemberId == 0)
                    {
                        Console.WriteLine("Returning to Main Menu...");
                        return;
                    }
                    selectedCustomer = SelectMember(customerList);
                }
            }

            Order newOrder = selectedCustomer.CurrentOrder;
            if (newOrder == null)
            {
                newOrder = selectedCustomer.MakeOrder();
                selectedCustomer.CurrentOrder = newOrder;
            }

            bool created = false;
            while (!created)
            {
                CreateIceCreamOrder(newOrder);

                bool added = false;
                while (!added)
                {
                    Console.Write("\nAdd another Ice Cream to Customer's cart? [Y]/[N] : ");
                    string orderAgain = Console.ReadLine().ToLower();

                    if (orderAgain == "n")
                    {
                        if (newOrder.IceCreamList.Count == 0)
                        {
                            // If no ice creams added, remove the empty order
                            selectedCustomer.CurrentOrder = null;
                        }

                        if (selectedCustomer.Rewards.Tier == "Gold")
                        {
                            goldQueue.Enqueue(newOrder);
                            Console.WriteLine();
                            Console.WriteLine("Success! Order has been added to the Gold Queue!");
                            Console.WriteLine();
                        }
                        else
                        {
                            regularQueue.Enqueue(newOrder);
                            Console.WriteLine();
                            Console.WriteLine("Success! Order has been added to the Regular Queue!");
                            Console.WriteLine();
                        }

                        added = true;
                        created = true;
                    }

                    else if (orderAgain == "y")
                    {
                        added = true;
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("******************************************************");
                        Console.WriteLine("*** Invalid Input! Please re-enter a valid option! ***");
                        Console.WriteLine("******************************************************");
                        Console.WriteLine();
                    }
                }
            }
        }

        static void CreateIceCreamOrder(Order order) // Method for Adding Ice Cream -------------------------------------------------------
        {
            while (true)
            {
                List<Flavour> flavours = new List<Flavour>();
                List<Topping> toppings = new List<Topping>();
                IceCream newIceCream = null;
                bool validIceCreamOption = false;

                Console.Write("\nEnter Ice Cream option (Cup, Cone, Waffle): ");
                string iceCreamOption = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(iceCreamOption))
                {
                    Console.WriteLine();
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine("*** Invalid Input! Please re-enter a valid Input! ***");
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine();
                    continue;
                }

                if (iceCreamOption == "cup" || iceCreamOption == "cone" || iceCreamOption == "waffle")
                {
                    validIceCreamOption = true;
                }

                if (!validIceCreamOption)
                {
                    Console.WriteLine();
                    Console.WriteLine("*******************************************************************");
                    Console.WriteLine("*** Invalid Input! Please re-enter Customer's Ice Cream option! ***");
                    Console.WriteLine("*******************************************************************");
                    Console.WriteLine();
                    continue;
                }

                bool validNoOfScoops = false;
                int noOfScoops = 0;
                while (!validNoOfScoops)
                {
                    try
                    {
                        Console.Write("\nEnter number of scoops (Single[1], Double[2], Triple[3]): "); //-----------------------------------------------------------------------------------------
                        string stringScoops = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(stringScoops))
                        {
                            Console.WriteLine();
                            Console.WriteLine("*****************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter a valid Input! ***");
                            Console.WriteLine("*****************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        noOfScoops = Convert.ToInt32(stringScoops);

                        if (noOfScoops >= 1 && noOfScoops <= 3)
                        {
                            validNoOfScoops = true;
                        }
                        if (!validNoOfScoops)
                        {
                            Console.WriteLine();
                            Console.WriteLine("************************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter the number of scoops! ***");
                            Console.WriteLine("************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("************************************************************");
                        Console.WriteLine("*** Invalid Input! Please re-enter the number of scoops! ***");
                        Console.WriteLine("************************************************************");
                        Console.WriteLine();
                        continue;
                    }
                }

                Console.WriteLine("\nRegular Flavours: Vanilla, Chocolate, Strawberry\nPremium Flavours: Durian, Ube, Sea salt");

                while (true)
                {
                    bool validFlavorsSelected = false;
                    Console.Write("\nEnter flavours (comma separated): ");
                    string[] flavourSelected = Console.ReadLine().Split(','); //--------------------------------------------------------------------------------------------------


                    if (flavourSelected.Length != noOfScoops)
                    {
                        Console.WriteLine();
                        Console.WriteLine("***************************************************************************************************");
                        Console.WriteLine("*** Invalid Input! Number of flavours should match the number of scoops ({0})! Please try again! ***", noOfScoops);
                        Console.WriteLine("***************************************************************************************************");
                        Console.WriteLine();
                        continue;
                    }

                    else
                    {
                        // False = Not Premium
                        foreach (var flavour in flavourSelected)
                        {
                            string flavourName = flavour.Trim().ToLower();

                            if (flavourName == "vanilla")
                            {
                                flavours.Add(new Flavour("Vanilla", false, 1));
                                validFlavorsSelected = true;
                            }

                            else if (flavourName == "chocolate")
                            {
                                flavours.Add(new Flavour("Chocolate", false, 1));
                                validFlavorsSelected = true;
                            }

                            else if (flavourName == "strawberry")
                            {
                                flavours.Add(new Flavour("Strawberry", false, 1));
                                validFlavorsSelected = true;
                            }

                            else if (flavourName == "durian")
                            {
                                flavours.Add(new Flavour("Durian", true, 1));
                                validFlavorsSelected = true;
                            }

                            else if (flavourName == "ube")
                            {
                                flavours.Add(new Flavour("Ube", true, 1));
                                validFlavorsSelected = true;
                            }

                            else if (flavourName == "sea salt")
                            {
                                flavours.Add(new Flavour("Sea salt", true, 1));
                                validFlavorsSelected = true;
                            }
                        }

                        if (!validFlavorsSelected)
                        {
                            Console.WriteLine();
                            Console.WriteLine("******************************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter Customer's desired flavour! ***");
                            Console.WriteLine("******************************************************************");
                            Console.WriteLine();
                        }

                        else
                        {
                            break;
                        }
                    }
                }

                Console.WriteLine("\nToppings Available: Sprinkles, Mochi, Sago, Oreos");

                while (true)
                {
                    bool validToppingSelected = false;
                    Console.Write("\nEnter toppings (comma separated or Enter [N] for NO Topping) : ");
                    string[] toppingSelected = Console.ReadLine().Split(',');

                    if (toppingSelected[0].Trim().ToLower() == "n")
                    {
                        toppings.Add(new Topping("None"));
                        validToppingSelected = true;
                    }

                    else
                    {
                        foreach (var topping in toppingSelected)
                        {
                            string toppingName = topping.Trim().ToLower();


                            if (toppingName == "sprinkles")
                            {
                                toppings.Add(new Topping("Sprinkles"));
                                validToppingSelected = true;
                            }

                            else if (toppingName == "mochi")
                            {
                                toppings.Add(new Topping("Mochi"));
                                validToppingSelected = true;
                            }

                            else if (toppingName == "sago")
                            {
                                toppings.Add(new Topping("Sago"));
                                validToppingSelected = true;
                            }

                            else if (toppingName == "oreos")
                            {
                                toppings.Add(new Topping("Oreos"));
                                validToppingSelected = true;
                            }
                        }
                    }
                    if (!validToppingSelected)
                    {
                        Console.WriteLine();
                        Console.WriteLine("******************************************************************");
                        Console.WriteLine("*** Invalid Input! Please re-enter Customer's desired topping! ***");
                        Console.WriteLine("******************************************************************");
                        Console.WriteLine();
                    }

                    else
                    {
                        break;
                    }
                }


                if (iceCreamOption == "cup")
                {
                    newIceCream = new Cup("Cup", noOfScoops, flavours, toppings);
                }

                else if (iceCreamOption == "cone")
                {
                    bool validDippedSelected = false;

                    while (!validDippedSelected)
                    {
                        Console.Write("Dipped Cone? [Y]/[N]: ");

                        string dipped = Console.ReadLine().ToLower();

                        if (dipped == "y")
                        {
                            newIceCream = new Cone("Cone", noOfScoops, flavours, toppings, true);
                            validDippedSelected = true;
                        }

                        else if (dipped == "n")
                        {
                            newIceCream = new Cone("Cone", noOfScoops, flavours, toppings, false);
                            validDippedSelected = true;
                        }

                        if (!validDippedSelected)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter Customer's desired topping choice! ***");
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine();
                        }
                    }
                }

                if (iceCreamOption == "waffle")
                {
                    bool validWaffleSelected = false;
                    while (!validWaffleSelected)
                    {
                        Console.Write("\nPremium or Original Waffle Flavour? [Y]/[N] ('N' for Original): ");
                        string premiumWaffleSelected = Console.ReadLine().ToLower();

                        if (premiumWaffleSelected == "y")
                        {
                            Console.WriteLine("\nWaffle Premium Flavours: Red Velvet, Charcoal, Pandan");

                            Console.Write("\nEnter a Premium Waffle Flavour: ");
                            string waffleFlavour = Console.ReadLine().Trim().ToLower();

                            if (waffleFlavour == "red velvet")
                            {
                                newIceCream = new Waffle("Waffle", noOfScoops, flavours, toppings, "Red Velvet");
                                validWaffleSelected = true;
                            }
                            else if (waffleFlavour == "charcoal")
                            {
                                newIceCream = new Waffle("Waffle", noOfScoops, flavours, toppings, "Charcoal");
                                validWaffleSelected = true;
                            }
                            else if (waffleFlavour == "pandan")
                            {
                                newIceCream = new Waffle("Waffle", noOfScoops, flavours, toppings, "Pandan");
                                validWaffleSelected = true;
                            }
                        }

                        else if (premiumWaffleSelected == "n")
                        {
                            newIceCream = new Waffle("Waffle", noOfScoops, flavours, toppings, "Original");
                            validWaffleSelected = true;
                        }

                        if (!validWaffleSelected)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter Customer's desired Waffle Flavour! ***");
                            Console.WriteLine("*************************************************************************");
                            Console.WriteLine();//----------------------------------------------------------------------------------------------------
                        }
                    }
                }

                if (newIceCream != null)
                {
                    order.AddIceCream(newIceCream);
                    order.Id = currentOrderId;
                    //order.TimeReceived = DateTime.Now;
                    break;
                }
            }
        }

        static List<string> ReadFlavoursFromCSV()
        {
            List<string> flavours = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader("flavours.csv"))
                {

                    // read header first
                    string s = sr.ReadLine();

                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] items = s.Split(',');
                        for (int i = 0; i < items.Length; i += 2)
                        {
                            flavours.Add(items[i].Trim().ToLower()); // Adding flavour names to the list
                        }
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("****************************************************************************************");
                Console.WriteLine("*** Error: A database issue has occurred! Please contact the manager for assistance! ***");
                Console.WriteLine("****************************************************************************************");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("An error occurred while processing your Order: {0}", ex.Message);
                Console.WriteLine();
            }

            return flavours;
        }

        static bool FlavourExists(string flavourNames)
        {
            List<string> availableFlavours = ReadFlavoursFromCSV();

            if (!availableFlavours.Contains(flavourNames.ToLower()))
            {
                return false;
            }
            return true;
        }

        static bool FlavourIsPremium(string flavourName)
        {
            List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
            return premiumFlavours.Contains(flavourName);
        }

        static bool WaffleIsPremium(string flavourName)
        {
            List<string> premiumFlavours = new List<string> { "red velvet", "charcoal", "pandan" };
            return premiumFlavours.Contains(flavourName);
        }

        static List<string> ReadToppingsFromCsv()
        {
            List<string> toppings = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader("toppings.csv"))
                {
                    // read header first
                    string s = sr.ReadLine();
                    string[] header = s.Split(',');
                    // read the rest 
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] items = s.Split(',');

                        for (int i = 0; i < items.Length; i++)
                        {
                            toppings.Add(items[i].ToLower());
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine();
                Console.WriteLine("****************************************************************************************");
                Console.WriteLine("*** Error! A database issue has occurred! Please contact the manager for assistance! ***");
                Console.WriteLine("****************************************************************************************");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("An error occurred while processing your Order: {0}", ex.Message);
                Console.WriteLine();
            }
            return toppings;
        }

        static bool ToppingExist(string toppingName)
        {
            List<string> availableToppings = ReadToppingsFromCsv();
            if (!availableToppings.Contains(toppingName.ToLower()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool IsDipped(string dipped)
        {
            if (dipped == "y")
            {
                return true;
            }
            return false;
        }

        // QUESTION 5 (Ng Joe Yi) -------------------------------------------------------------------------------------------------------------------------------------- //
        static int ReadOrderDetails(List<Customer> cList)
        {
            Customer selectedCustomer = SelectMember(customerList);
            //Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();
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
                            string[] items = s.Split(',');
                            int orderId = Convert.ToInt32(items[0]);
                            int memberId = Convert.ToInt32(items[1]);
                            DateTime timeReceived = Convert.ToDateTime(items[2]);
                            DateTime timeFulfilled = Convert.ToDateTime(items[3]);
                            string option = Convert.ToString(items[4]);
                            int scoops = Convert.ToInt32(items[5]);
                            bool dipped = false;
                            if (items[6] != null) // If not null means its a cone
                            {
                                string item6 = items[6].Trim().ToLower(); // Trim and convert to lower case
                                if (item6 == "true")
                                {
                                    dipped = true;
                                }
                            }
                            string waffleFlavour = Convert.ToString(items[7]);
                            lastOrderId = Math.Max(lastOrderId, orderId);
                            
                            // FLAVOUR QUANTITY CHECK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            /*
                            for (int i = 8; i <= 10; i++)
                            {
                                string flavourType = items[i].Trim();
                                foreach (KeyValuePair<string, int> kvp in flavourQuantity)
                                {
                                    if (kvp.Key == flavourType)
                                    {
                                        flavourQuantity[flavourType]++;
                                    }
                                    else
                                    {
                                        flavourQuantity.Add(flavourType, 1);
                                    }
                                }
                            }
                            List<Flavour> flavours = new List<Flavour>();
                            for (int i = 8; i <= 10; i++)
                            {
                                int quantity = 0;
                                foreach (KeyValuePair<string, int> kvp in flavourQuantity)
                                {
                                    if (items[i] != null)
                                    {
                                        if (items[i] == kvp.Key)
                                        {
                                            quantity = kvp.Value;
                                            flavours.Add(new Flavour(items[i], FlavourIsPremium(items[i]), quantity));
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                            } 
                            */
                            

                            
                            List<Flavour> flavours = new List<Flavour>();
                            for (int i = 8; i <= 10; i++)
                            {
                                if (items[i] != null)
                                {
                                    flavours.Add(new Flavour(items[i], FlavourIsPremium(items[i]), 1));
                                }
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
                                // Make sure OrderHistory is initialized before using it.
                                if (selectedCustomer.OrderHistory == null)
                                {
                                    selectedCustomer.OrderHistory = new List<Order>();
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
                            Console.WriteLine("******************************************************");
                            Console.WriteLine("*** Invalid Input! Please re-enter a valid Input! ***");
                            Console.WriteLine("******************************************************");
                            Console.WriteLine();
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine();
                            Console.WriteLine("An error occured while processing your Order: {0}", ex.Message);
                            Console.WriteLine();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine();
                            Console.WriteLine("An error occurred while processing your Order: {0}", ex.Message);
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("****************************************************************************************");
                Console.WriteLine("*** Error! A database issue has occurred! Please contact the manager for assistance! ***");
                Console.WriteLine("****************************************************************************************");
                Console.WriteLine();
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("*******************************************************************************************");
                Console.WriteLine("*** Error! A database IO issue has occurred! Please contact the manager for assistance! ***");
                Console.WriteLine("*******************************************************************************************");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("An error occurred while processing your Order: {0}", ex.Message);
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
                Console.WriteLine("*** Current order details are not available ***");
            }
        }

        static void DisplayOrderHistory(Customer customerId)
        {
            Console.WriteLine("{0,10}-----------------------", "");
            Console.WriteLine("{0,11}Order History Details ", "");
            Console.WriteLine("{0,10}-----------------------", "");
            if (customerId.OrderHistory.Count() > 0)
            {
                foreach (Order orderHistory in customerId.OrderHistory)
                {
                    Console.WriteLine(orderHistory.ToString());
                }
                Console.WriteLine("Returning to Main Menu...");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("*** Order history details are not available ***");
                Console.WriteLine("\nReturning to Main Menu...");
            }
        }


        static void DisplayOrderDetails(List<Customer> cList)
        {
            // List the customer 
            ListAllCustomer(cList);
            Customer selectedCustomer = SelectMember(cList); // Assuming this method returns the correct customer

            if (selectedCustomer.Name == null)
            {
                return;
            }

            Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();

            // Initialize OrderHistory if it hasn't been already
            if (selectedCustomer.OrderHistory == null)
            {
                selectedCustomer.OrderHistory = new List<Order>();
            }

            // Clear existing order history
            selectedCustomer.OrderHistory.Clear();

            using (StreamReader sr = new StreamReader("orders.csv"))
            {
                string s; // Declare 's' outside the loop for proper scope
                          // Skip the header
                sr.ReadLine();

                // Read each line representing an order
                while ((s = sr.ReadLine()) != null)
                {
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

                    /* 
                     List<Flavour> flavours = new List<Flavour>();
                     for (int i = 8; i <= 10; i++)
                     {
                         if (items[i] != null)
                         {
                             flavours.Add(new Flavour(items[i], FlavourIsPremium(items[i]), 1));
                         }
                     } */

                    // FLAVOUR QUANTITY CHECK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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
                    for (int i = 8; i <= 10; i++)
                    {
                        int quantity = 0;
                        foreach (KeyValuePair<string, int> kvp in flavourQuantity)
                        {
                            Console.WriteLine(kvp.Key);
                            if (items[i] != null)
                            {
                                if (items[i] == kvp.Key)
                                {
                                    quantity = kvp.Value;
                                    flavours.Add(new Flavour(items[i], FlavourIsPremium(items[i]), quantity));
                                    
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
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

                    // Create the correct IceCream instance based on the option
                    if (option.Equals("cup", StringComparison.OrdinalIgnoreCase))
                    {
                        IceCream iceCream = new Cup(option, scoops, flavours, toppings);
                        order.AddIceCream(iceCream); // Add the created ice cream to the order
                    }
                    else if (option.Equals("cone", StringComparison.OrdinalIgnoreCase))
                    {
                        IceCream iceCream = new Cone(option, scoops, flavours, toppings, dipped);
                        order.AddIceCream(iceCream); // Add the created ice cream to the order
                    }
                    else if (option.Equals("waffle", StringComparison.OrdinalIgnoreCase))
                    {
                        IceCream iceCream = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
                        order.AddIceCream(iceCream); // Add the created ice cream to the order
                    }

                    // Add the order to the selected customer's order history only once
                    selectedCustomer.OrderHistory.Add(order);
                }
            }

            // Now display current order and order history of the selected customer
            DisplayCurrentOrder(selectedCustomer); // This should display the current order
            Console.WriteLine();
            DisplayOrderHistory(selectedCustomer); // This should display the order history once
        }



        /*
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("*** Invalid Member ID! Member ID not found! ***");
                            Console.WriteLine("***********************************************");
                            Console.WriteLine();
                        }


                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("*******************************************************");
                        Console.WriteLine("*** Invalid Input! Please enter a valid Member ID! ***");
                        Console.WriteLine("*******************************************************");
                        Console.WriteLine();
                    }*/


        // QUESTION 6 (Ng Joe Yi) -------------------------------------------------------------------------------------------------------------------------------------- //
        static void DisplayIceCreamIndex(Customer customerId)
        {
            IceCream iceCreamOrder = null;
            int iceCreamIndex = 1;
            Console.WriteLine("-------------------------------------- Ice Cream Index --------------------------------------");
            foreach (IceCream iceCream in customerId.CurrentOrder.IceCreamList)
            {
                iceCreamOrder = iceCream;
                Console.WriteLine("{0}. ", iceCreamIndex);
                Console.WriteLine(iceCreamOrder.ToString(), iceCreamIndex);
                iceCreamIndex++;
            }
        }
        static void ModifyOrderDetails(List<Customer> cList)
        {
            if (cList == null || cList.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("*****************************************************");
                Console.WriteLine("*** Error! There are no Customers in our database ***");
                Console.WriteLine("*****************************************************");
                Console.WriteLine("\nReturning to Main Menu...\n");
                Console.WriteLine();
                return; // Early return to exit the method
            }

            ListAllCustomer(cList);

            int findCustomerMemberId = 0;

            while (true)
            {
                Console.Write("Enter Customer's Member ID (0 to exit): ");
                try
                {
                    string stringFindCustomerId = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(stringFindCustomerId))
                    {
                        Console.WriteLine();
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine("*** Invalid input! Please re-enter a valid Member ID! ***");
                        Console.WriteLine("*********************************************************");
                        Console.WriteLine();
                        continue;
                    }
                    findCustomerMemberId = Convert.ToInt32(stringFindCustomerId);
                    if (findCustomerMemberId == 0)
                    {
                        Console.WriteLine("\nReturning to Main Menu...\n");
                        return;
                    }
                    Console.WriteLine();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine("*** Invalid input! Please re-enter a valid Member ID! ***");
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine();
                    Console.WriteLine("*************************************************");
                    Console.WriteLine("*** Error! Please re-enter a valid Member ID! ***");
                    Console.WriteLine("*************************************************");
                    Console.WriteLine();
                }
            }

            Customer selectedMemberId = SelectMember(customerList);


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
            Console.WriteLine();

            // List all the ice cream objects contained in the order 

            int option = 0;
            bool modifyOption = false;
            while (!modifyOption)
            {
                Console.Write(
                                "[1] Modify Ice Cream Order\n" +
                                "[2] Add an Ice Cream to Order\n" +
                                "[3] Delete an existing Ice Cream\n" +
                                "[0] Exit to Main Menu\n" +
                                "Enter an option: "
                            );
                try
                {
                    string stringOption = Console.ReadLine();
                    if (string.IsNullOrEmpty(stringOption))
                    {
                        Console.WriteLine();
                        Console.WriteLine("******************************************************");
                        Console.WriteLine("*** Invalid Input! Please re-enter a valid option! ***");
                        Console.WriteLine("******************************************************");
                        Console.WriteLine();
                        continue;
                    }
                    option = Convert.ToInt32(stringOption);
                    modifyOption = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("******************************************************");
                    Console.WriteLine("*** Invalid Input! Please re-enter a valid option! ***");
                    Console.WriteLine("******************************************************");
                    Console.WriteLine();
                }

            }

            if (option == 1 || option == 2 || option == 3)
            {
                Order order = selectedMemberId.CurrentOrder;
                if (option == 1) // This dont need to change in regards to the new csv implementation 
                {
                    DisplayIceCreamIndex(selectedMemberId);
                    while (true)
                    {
                        Console.Write("Enter the index of the ice cream to modify (0 to exit): ");
                        try
                        {
                            string stringSelectedIndex = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(stringSelectedIndex))
                            {

                                Console.WriteLine("*****************************************************");
                                Console.WriteLine("*** Invalid index. Please re-enter a valid Index! ***");
                                Console.WriteLine("*****************************************************");
                                Console.WriteLine();
                                continue;
                            }
                            int selectedIndex = Convert.ToInt32(stringSelectedIndex) - 1; // Adjust for zero-based indexing
                            if (selectedIndex == -1)
                            {
                                Console.WriteLine("\nReturning to Main Menu...\n");
                                return;
                            }
                            if (selectedIndex >= 0 && selectedIndex < selectedMemberId.CurrentOrder.IceCreamList.Count())
                            {
                                selectedMemberId.CurrentOrder.ModifyIceCream(selectedIndex);
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
                            Console.WriteLine("**************************************************");
                            Console.WriteLine("*** Invalid Input! Please enter a valid Index! ***");
                            Console.WriteLine("***************************************************");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("*********************************************");
                            Console.WriteLine("*** Error! Please re-enter a valid index! ***");
                            Console.WriteLine("**********************************************");
                        }
                    }
                }
                else if (option == 2)
                {
                    CreateIceCreamOrder(order);
                    DisplayCurrentOrder(selectedMemberId);
                    order.Id = currentOrderId;
                    Console.WriteLine("Returning to Main Menu...\n");
                }
                else if (option == 3)
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
                        int selectedIndex = Convert.ToInt32(stringSelectedIndex) - 1; // Adjust for zero-based indexing
                        if (selectedIndex == -1)
                        {
                            Console.WriteLine("\nReturning to Main Menu...\n");
                            return;
                        }
                        else if (selectedIndex >= 0 && selectedIndex < selectedMemberId.CurrentOrder.IceCreamList.Count())
                        {
                            if (selectedMemberId.CurrentOrder.IceCreamList.Count() > 1)
                            {
                                selectedMemberId.CurrentOrder.DeleteIceCream(selectedIndex);
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
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("*******************************************************");
                Console.WriteLine("*** Invalid Option! Please re-enter a valid option! ***");
                Console.WriteLine("*******************************************************");
                Console.WriteLine();
            }
        }






    }
}

