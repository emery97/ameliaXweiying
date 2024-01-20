//==========================================================
// Student Number : S10258645
// Student Name : Lee Wei Ying
// Partner Name : Amelia Goh Jia Xuan
//==========================================================



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class Program
    {
        // main program
        static void Main(string[] args)
        {
            while (true)
            {
                Queue<Customer> goldQueue = new Queue<Customer>();
                Queue<Customer> normalQueue = new Queue<Customer>();
                List<Customer> customerList = new List<Customer>();
                Dictionary<Order, int> customerOrderHistory = new Dictionary<Order, int>();
                List<Order> orderHistory = new List<Order>();
                Queue<Order> goldQueueOrderClass = new Queue<Order>();  //This is for basic 4, for me to append the orders into queues
                Queue<Order> regularQueueOrderClass = new Queue<Order>();

                InitCustomerList(customerList, goldQueue, normalQueue,customerOrderHistory);
                Console.WriteLine(new String('-', 10) + "MENU" + new string('-', 10));
                Console.WriteLine("[1] List all customers");
                Console.WriteLine("[2] List all current orders");
                Console.WriteLine("[3] Register a new customer");
                Console.WriteLine("[4] Create a customer's order");
                Console.WriteLine("[5] Display order details of a customer");
                Console.WriteLine("[6] Modify order details");
                Console.WriteLine("[0] Exit");
                Console.WriteLine(new string('-', 23));

                Console.Write("Enter your option: ");
                string userOption = Console.ReadLine();

                if (userOption == "0")
                {
                    break;
                }
                else if (userOption == "1")
                {
                    GettingOrderHistory(customerOrderHistory, customerList);
                    foreach (Customer c in customerList)
                    {
                        Console.WriteLine(c.ToString());
                    }
                }
                else if (userOption == "2")
                {
                    ListOrder(customerList, goldQueue, normalQueue);
                }
                else if (userOption == "3")
                {
                    RegisterCustomer(customerList);
                }
                else if (userOption == "4")
                {
                    ListCustomer();
                    MakingOrderQueues(customerList, goldQueueOrderClass, regularQueueOrderClass);
                    AddIceCreamOrder(orderHistory, customerList, goldQueueOrderClass, regularQueueOrderClass);
                }
                else if (userOption == "5")
                {
                    ListCustomer();
                    GettingOrderHistory(customerOrderHistory,customerList);
                    OrderDetails(customerList, customerOrderHistory);
                }
                else if (userOption == "6")
                {
                    ListCustomer();
                    ModifyOrderDetails(customerList);
                }
                else
                {
                    Console.WriteLine("Please input a number from 0 to 6 thank you.");
                }
            }
        }
        // To list current customers
        static void ListCustomer()
        {
            Console.WriteLine("Our Current Customers: ");
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                string s = sr.ReadLine(); //header
                string[] header = s.Split(',');
                Console.WriteLine($"{header[0],-10} {header[1],-10} {header[2]}");
                while ((s = sr.ReadLine()) != null)
                {
                    string[] data = s.Split(',');
                    Console.WriteLine($"{data[0],-10} {data[1],-10} {data[2]}");
                }
            }
        }

        //Basic feature 1's method
        static void InitCustomerList(List<Customer> customerList, Queue<Customer> goldQueue, Queue<Customer> normalQueue, Dictionary<Order, int> customerOrderHistory)
        {
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                string header = sr.ReadLine(); //Ignore headers

                while (!sr.EndOfStream) //Ensures that all data read 
                {
                    string s = sr.ReadLine();

                    if (s != null) // Aka what will b carried out if there is still data
                    {
                        string[] customerData = s.Split(','); //When spilt, become an array + rmb tht if want print data out, must use foreach loop since this is an array  
                        string Name = customerData[0];
                        int Memberid = Convert.ToInt32(customerData[1]);
                        DateTime Dob = Convert.ToDateTime(customerData[2]);
                        string Tier = customerData[3];
                        int Points = Convert.ToInt32(customerData[4]);
                        int PunchCard = Convert.ToInt32(customerData[5]);


                        Customer c = new Customer(Name, Memberid, Dob);  //Need make customer obj first so can access class Pointcard by accessing rewards first since rewards is of class Pointcard but can only be used by a customer object
                        c.Rewards = new PointCard(Points, PunchCard);
                        c.Rewards.Tier = Tier;

                        customerList.Add(c);
                        // add to respective queues
                        if (c.Rewards.Tier == "Gold")
                        {
                            goldQueue.Enqueue(c);
                        }
                        else
                        {
                            normalQueue.Enqueue(c);
                        }
                    }


                }
            }
        }

        // option 2 method - display customer info in both queues
        static void ListOrder(List<Customer> customerList, Queue<Customer> goldQueue, Queue<Customer>normalQueue)
        {
            // making sure got people in queue
            if (goldQueue.Count() > 0)
            {
                Console.WriteLine("Customers in gold queue:");
                foreach(Customer c in goldQueue)
                {
                    if (c.CurrentOrder != null)
                    {
                        foreach (Order o in c.OrderHistory)
                        {
                            Console.Write(o + "\t");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{c.Name} has no current order");
                    }
                }
            }
            // if no one in queue
            else
            {
                Console.WriteLine("There is no one in the gold queue.");
            }

            // making sure got people in queue
            if (normalQueue.Count() > 0)
            {
                Console.WriteLine("\nCustomers in normal queue: ");
                foreach (Customer c in normalQueue)
                {
                    if (c.CurrentOrder != null)
                    {
                        foreach (Order o in c.OrderHistory)
                        {
                            Console.Write(o + "\t");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{c.Name} has no current order");
                    }
                }
            }
            // if no one in queue
            else
            {
                Console.WriteLine("There is no one in the regular queue");
            }
        }

        //Basic feature 3's method 
        static void RegisterCustomer(List<Customer> customerList)  //Parameter list since need this list later due to the fact that you need add this new registered customer to the customer list.
        {

            Console.Write("Enter customer's name: ");
            string newName = Console.ReadLine();
            Console.Write("Enter customer's id: ");
            int newId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter customer's date of birth: ");
            DateTime newDob = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter customer's tier: ");
            string newTier = Console.ReadLine();
            Console.Write("Enter customer's points: ");
            int newPoints = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter customer's punch card: ");
            int newPC = Convert.ToInt32(Console.ReadLine());


            Customer newCustomer = new Customer(newName, newId, newDob);
            customerList.Add(newCustomer);

            //Since PointCard constructor parameters are int, int which is for Points and PunchCard
            newCustomer.Rewards = new PointCard(0, 0); //Initialize first, before assigning newCustomer.Rewards = PointCard object aka newCustomerPC.
                                                        //^^ Btw, should always initialize in case that it is ever null value. Bc if is legit null value, then you will have error when trying to run the next code bc you wont be able to access the newCustomer.Rewards.Points due to the fact that it is NULL! it is a NullReferenceException
            PointCard newCustomerPC = new PointCard(newCustomer.Rewards.Points, newCustomer.Rewards.PunchCard); //Need .Rewards because Rewards in customer class and is also of type PointCard. (is like the example giving in slides with John.Addr.Shipping)

            newCustomer.Rewards = newCustomerPC; //Assigning of PointCard to customer is done in this code. (Rewards is of class type PointCard, but it is stored in the customer class) So thats why can equate pointcard object to another pointcard obj
            try 
            { 
                //Appending..
                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    sw.WriteLine($"{newName},{newId},{newDob.ToString("dd/MM/yy")},{newTier},{newPoints},{newPC}");
                }
                Console.WriteLine("Customer registered successfully.");
                Console.WriteLine("");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter your input in the correct format.");
            }
            catch (Exception ex)  //In case got any other error (Exception ex catch any and all types of exception so this is if eg User anyhow enter somethiing, then cannot be registered alrd..
            {
                Console.WriteLine("Customer could not be registered.");
                Console.WriteLine($"Reason: {ex.Message}");
                Console.WriteLine("");
            }
        }

        //Basic feature 4's method
        static void MakingOrderQueues(List<Customer> cList, Queue<Order> goldQueueOrderClass, Queue<Order> regularQueueOrderClass)
        {
            // Enqueue goldQueueOrderClass into goldQueueOrderClass
            while (goldQueueOrderClass.Count > 0)
            {
                Order goldOrderDetails = goldQueueOrderClass.Dequeue();
                goldQueueOrderClass.Enqueue(goldOrderDetails);
            }

            // Enqueue regularQueueOrderClass into regularQueueOrderClass
            while (regularQueueOrderClass.Count > 0)
            {
                Order regularOrderDetails = regularQueueOrderClass.Dequeue();
                regularQueueOrderClass.Enqueue(regularOrderDetails);

            }

        }

        static void MakingQueueOrderClass(string customersFilePath, string ordersFilePath, Queue<Order> goldQueueOrderClass, Queue<Order> regularQueueOrderClass)
        {
            // Dictionary to hold customers with their member ID as the key
            Dictionary<int, Customer> customerDict = new Dictionary<int, Customer>();
            //List for flavours n toppings initialized
            List<Flavour> flavourList = new List<Flavour>();
            List<Topping> toppingList = new List<Topping>();

            using (StreamReader customerSr = new StreamReader("customers.csv"))
            {
                string customerHeader = customerSr.ReadLine(); //Ignore customer headers
                while (!customerSr.EndOfStream) //Ensures that all data read 
                {
                    string customerCsvData = customerSr.ReadLine();

                    if (customerCsvData != null) // Aka what will b carried out if there is still data
                    {
                        string[] customerData = customerCsvData.Split(','); //When spilt, become an array + rmb tht if want print data out, must use foreach loop since this is an array  
                        string Name = customerData[0];
                        int Memberid = Convert.ToInt32(customerData[1]);
                        DateTime Dob = Convert.ToDateTime(customerData[2]);
                        string Tier = customerData[3];
                        int Points = Convert.ToInt32(customerData[4]);
                        int PunchCard = Convert.ToInt32(customerData[5]);

                        Customer c = new Customer(Name, Memberid, Dob);  //Need make customer obj first so can access class Pointcard by accessing rewards first since rewards is of class Pointcard but can only be used by a customer object
                        c.Rewards = new PointCard(Points, PunchCard);
                        c.Rewards.Tier = Tier;

                        customerDict[c.Memberid] = c; //Adding to dictionary the customer's kvp since customerDict key = int aka memberid then value is Customer type

                    }
                }
            }

            using (StreamReader orderSr = new StreamReader("orders.csv"))
            {
                string orderHeader = orderSr.ReadLine(); //Ignore order headers

                while (!orderSr.EndOfStream) //Ensures that all data read 
                {
                    string orderCsvData = orderSr.ReadLine();

                    if (orderCsvData != null) // Aka what will b carried out if there is still data
                    {
                        string[] orderData = orderCsvData.Split(',');  //Now become array since you split 
                        int orderId = Convert.ToInt32(orderData[0]);
                        int ordersMemberid = Convert.ToInt32(orderData[1]);
                        DateTime timeFulfilled = Convert.ToDateTime(orderData[2]);
                        string Option = orderData[4];
                        int Scoops = Convert.ToInt32(orderData[5]);
                        bool Dipped = Convert.ToBoolean(orderData[6]);
                        string WaffleFlavour = orderData[7];
                        string flavourType1 = orderData[8];
                        // Check for null values and handle them 
                        string flavourType2 = string.IsNullOrEmpty(orderData[9]) ? null : orderData[9]; //So if legit any of these is null, thenoutput is just null 
                        string flavourType3 = string.IsNullOrEmpty(orderData[10]) ? null : orderData[10];
                        string toppingType1 = string.IsNullOrEmpty(orderData[11]) ? null : orderData[11];
                        string toppingType2 = string.IsNullOrEmpty(orderData[12]) ? null : orderData[12];
                        string toppingType3 = string.IsNullOrEmpty(orderData[13]) ? null : orderData[13];

                        //Order 
                        Order orderCsv = new Order(orderId, timeFulfilled);

                        // Initialize the premium flag for each flavor type
                        bool isFlavourType1Premium = false;
                        bool isFlavourType2Premium = false;
                        bool isFlavourType3Premium = false;

                        if (flavourType1 == "Durian" || flavourType1 == "Sea salt" || flavourType1 == "Ube")
                        {
                            isFlavourType1Premium = true;
                        }
                        if (flavourType2 == "Durian" || flavourType2 == "Sea salt" || flavourType2 == "Ube")
                        {
                            isFlavourType2Premium = true;
                        }
                        if (flavourType3 == "Durian" || flavourType3 == "Sea salt" || flavourType3 == "Ube")
                        {
                            isFlavourType3Premium = true;
                        }

                        // use a dictionary to track flavors and their quantities
                        Dictionary<string, Flavour> flavourDictionary = new Dictionary<string, Flavour>();

                        // Method to add or update flavors in the dictionary
                        void AddOrUpdateFlavour(string flavourType, bool csvFilePremium)
                        {
                            if (string.IsNullOrEmpty(flavourType))
                            {
                                // Handle null or empty flavor type if necessary
                                return;
                            }

                            // If the flavor already exists, just update the quantity
                            if (flavourDictionary.TryGetValue(flavourType, out Flavour existingFlavour)) //Basically checkng in flavourDictionary based on flavourTYpe and then output is exisyingFlavour which is of flavour class. If yes, then code under it will be carried out
                            {
                                existingFlavour.Quantity += 1;
                            }
                            else
                            {
                                // If not, add a new flavor with the specified quantity
                                flavourDictionary[flavourType] = new Flavour(flavourType, csvFilePremium, 1); //Basically set the value of kvp pair in flavourdictionary (aka of flavour class) to be of new flavour instance 
                            }
                        }

                        // Add or update the flavors in the dictionary
                        AddOrUpdateFlavour(flavourType1, isFlavourType1Premium);
                        AddOrUpdateFlavour(flavourType2, isFlavourType2Premium);
                        AddOrUpdateFlavour(flavourType3, isFlavourType3Premium);


                        foreach (KeyValuePair<string, Flavour> kvp in flavourDictionary)
                        {
                            flavourList.Add(kvp.Value);
                        }

                        Topping topping1 = new Topping(toppingType1);
                        Topping topping2 = new Topping(toppingType2);
                        Topping topping3 = new Topping(toppingType3);

                        toppingList.Add(topping1);
                        toppingList.Add(topping2);
                        toppingList.Add(topping3);


                        if (Option == "Cup" || Option == "cup")
                        {
                            Cup csvFileOrders = new Cup(Option, Scoops, flavourList, toppingList);
                            orderCsv.IceCreamList.Add(csvFileOrders);
                        }


                        else if (Option == "Cone" || Option == "cone")
                        {
                            Cone csvFileOrders = new Cone(Option, Scoops, flavourList, toppingList, Dipped);
                            orderCsv.IceCreamList.Add(csvFileOrders);
                        }

                        else if (Option == "Waffle" || Option == "waffle")
                        {
                            Waffle csvFileOrders = new Waffle(Option, Scoops, flavourList, toppingList, WaffleFlavour);
                            orderCsv.IceCreamList.Add(csvFileOrders);

                        }

                        // Check if the corresponding customer is in the gold tier
                        if (customerDict.TryGetValue(ordersMemberid, out Customer c) && c.Rewards.Tier.Equals("Gold"))
                        {
                            // Enqueue to the goldQueue if the customer is gold tier
                            goldQueueOrderClass.Enqueue(orderCsv);
                        }
                        else
                        {
                            // Otherwise, enqueue to the regularQueue
                            regularQueueOrderClass.Enqueue(orderCsv);
                        }


                    }
                }
            }
        }

        static Customer FindingCustomerID(List<Customer> customerList, int selectedCustomerID) //Pass in list and the customer ID i want find. The customer ID i want find should be found in customer List.
        {
            //Initialize the customer selected
            Customer customerUserWants = null; //Initialize as null first in case t{he customer cannot be found, then js return null value which represents no customer found

            foreach (Customer findingCustomer in customerList)
            {
                if (findingCustomer.Memberid == selectedCustomerID)
                {
                    customerUserWants = findingCustomer;
                    break; //Break once found customer
                }
                else
                {
                    continue;
                }
            }

            if (customerUserWants != null)
            {
                return customerUserWants;
            }
            else
            {
                return null; //Bc is static CUSTOMER. So have to return smth of customer class. Eg static void means no return, static int means return integer
            }
        }

        static void AddIceCreamOrder(List<Order> OrderHistory, List<Customer> customerList, Queue<Order> goldQueueOrderClassMethod, Queue<Order> regularQueueOrderClassMethod) //Because need all of these for all of basic steps 4
        {

            Console.Write("Select a customer ID: ");  //Better to key in user ID than name in case got people have same name
            int customerIdToFind = Convert.ToInt32(Console.ReadLine());
            Customer customerFound = FindingCustomerID(customerList, customerIdToFind); //Aka the customerUserWants

            if (customerFound == null)
            {
                Console.WriteLine("Customer cannot be found.");
            }
            else
            {

                // Check if the customer already has an order history, if no, then create it
                if (customerFound.OrderHistory == null)
                {
                    customerFound.OrderHistory = new List<Order>();
                }

                //Create order object
                Order newestOrder = new Order();

                CreateOrder(newestOrder, customerFound);

                customerFound.OrderHistory.Add(newestOrder); //..???
                customerFound.CurrentOrder = newestOrder; // This sets the customer's current order to the one just created/modified

                while (true) //Will keep asking them if they want to add until they say No
                {
                    Console.Write("Do you want to add another ice cream? [Y/N]: ");
                    string userAddIceCream = Console.ReadLine();

                    if (userAddIceCream == "Y")
                    {
                        CreateOrder(newestOrder, customerFound);
                        customerFound.OrderHistory.Add(newestOrder);  // Add the new order to order history
                        customerFound.CurrentOrder = newestOrder; // This sets the customer's current order to the one just created/modified

                    }
                    else if (userAddIceCream == "N")
                    {
                        if (customerFound.Rewards.Tier == "Gold")
                        {
                            goldQueueOrderClassMethod.Enqueue(newestOrder);
                            Console.WriteLine("Order has been made successfully!");
                        }
                        else
                        {
                            regularQueueOrderClassMethod.Enqueue(newestOrder);
                            Console.WriteLine("Order has been made successfully!");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option.");
                        continue;
                    }
                }
            }

         }

        static void CreateOrder(Order newOrder, Customer customerFound)
        {
            List<Flavour> userFlavourList = new List<Flavour>();
            List<Topping> userToppingList = new List<Topping>();
            bool userPremium = false; //Set it to false first
            
            Dictionary<string, int> quantityCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase); // Case-insensitive comparison

            Console.Write("Enter your ice cream option: ");
            string userOption = Console.ReadLine();
            Console.Write("Enter your amount of ice cream scoops in numbers '1', '2' or '3': ");
            int userScoops = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= userScoops; i++)
            {
                Console.Write($"Enter the flavor for scoop {i}: ");
                string userType = Console.ReadLine(); //Type as in the TYPE OF FLAVOUR

                // Determine if the flavor is premium 
                userPremium = userType.Equals("Durian", StringComparison.OrdinalIgnoreCase) ||
                                   userType.Equals("Ube", StringComparison.OrdinalIgnoreCase) ||
                                   userType.Equals("Sea salt", StringComparison.OrdinalIgnoreCase);

                // Increase the quantity for flavor in dictionary if same flavour entered
                if (quantityCount.ContainsKey(userType))
                {
                    quantityCount[userType]++; //Will retrieve value in the dict kvp pair and valye is the quantitiy. Usertype aka flavour is the key
                }
                else
                {
                    quantityCount[userType] = 1; // First time this flavor is added
                }
            }

            foreach (KeyValuePair<string, int> kvp in quantityCount)
            {
                string typeFinal = kvp.Key; //Since flavour is the key
                bool isItPremiumFinal = typeFinal.Equals("Durian", StringComparison.OrdinalIgnoreCase) || typeFinal.Equals("Ube", StringComparison.OrdinalIgnoreCase) || typeFinal.Equals("Sea salt", StringComparison.OrdinalIgnoreCase);
                int quantityFinal = kvp.Value;
                Flavour flavourFinal = new Flavour(typeFinal, isItPremiumFinal, quantityFinal);
                userFlavourList.Add(flavourFinal);
            }
            // for toppings
            for (int i = 1; i < 5; i++)
            {
                Console.Write("Enter your chosen toppings (Enter 'N' to exit): ");
                string userToppingChoice = Console.ReadLine().ToUpper();

                if (userToppingChoice == "N")
                {
                    Console.WriteLine("You have exited the chosing toppings section.");
                    break;
                }
                else
                {
                    Topping userTopping = new Topping(userToppingChoice);  //Need make topping of class topping first before adding into list since the list requires its objects to be of type Topping since diagram for class states List<TOPPINGS!!!>
                    userToppingList.Add(userTopping);
                }
            }

            //Create ice cream object now
            if (userOption == "Cup" || userOption == "cup")
            {
                Cup newIceCreamOrder = new Cup(userOption, userScoops, userFlavourList, userToppingList);
                newOrder.AddIceCream(newIceCreamOrder);
            }

            else if (userOption == "Cone" || userOption == "cone")
            {
                bool userDipped = false;
                Console.Write("Is the cone dipped in chocolate [Y/N]: ");
                string userDippedAnswer = Console.ReadLine();
                if (userDippedAnswer == "Y")
                {
                    userDipped = true;
                }
                Cone newIceCreamOrder = new Cone(userOption, userScoops, userFlavourList, userToppingList, userDipped);
                newOrder.AddIceCream(newIceCreamOrder);
            }

            else if (userOption == "Waffle" || userOption == "waffle")
            {
                Console.Write("Enter waffle flavour [Original/Red velvet/Charcoal/Pandan]: ");
                string userWaffle = Console.ReadLine();
                Waffle newIceCreamOrder = new Waffle(userOption, userScoops, userFlavourList, userToppingList, userWaffle);
                newOrder.AddIceCream(newIceCreamOrder);

            }

        }


        // option 5 method - display order details
        static void OrderDetails(List<Customer> customerList, Dictionary<Order, int> customerOrderHistory)
        {
            string nameInCSV = "";
            Customer chosenCustomer = new Customer();
            int ID = 0;

            // checking if correct ID is entered
            while (true)
            {
                Console.Write("Enter a customer ID: ");
                ID = Convert.ToInt32(Console.ReadLine());
                // validation for id input
                bool brk = false;
                foreach (Customer c in customerList)
                {
                    if (c.Memberid == ID)
                    {
                        nameInCSV += c.Name;
                        chosenCustomer = c;
                        brk = true;
                    }
                }
                if (brk == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter the correct ID please.");
                }
            }

            // checking if customer has an order 
            if (chosenCustomer.CurrentOrder != null)
            {
                Console.WriteLine("Current order: ");
                Console.WriteLine(chosenCustomer.CurrentOrder.ToString());

                // ice cream details from current order
                Console.WriteLine("Ice cream details: ");
                foreach (IceCream ic in chosenCustomer.CurrentOrder.IceCreamList)
                {
                    Console.WriteLine(ic);
                }
            }
            else
            {
                Console.WriteLine($"{nameInCSV} has no current order.\n");
            }

            // checking if customer has an order
            bool hasOrderHistory = false;
            Console.WriteLine($"{nameInCSV} order history:");
            foreach (Order order in customerOrderHistory.Keys)
            {
                if (customerOrderHistory[order] == ID)
                {
                    Console.WriteLine(order.ToString());
                    hasOrderHistory = true;
                }
            }
            if (hasOrderHistory == false)
            {
                Console.WriteLine("NULL");
            }
        }

        // reading order.csv and making orders
        static void GettingOrderHistory(Dictionary<Order, int> customerOrderHistory, List<Customer> customerList)
        {
            List<string> premiumFlavour = new List<string> { "durian", "ube", "sea salt" };

            using (StreamReader sr = new StreamReader("orders.csv"))
            {
                string header = sr.ReadLine(); // headers

                while (!sr.EndOfStream) 
                {
                    List<Flavour> flavours = new List<Flavour>();
                    List<Topping> toppings = new List<Topping>();

                    bool dipped = false;

                    // flavours variable
                    string type = "";
                    int quantity = 1;

                    // toppings variable
                    string toppingType = "";
                    string s = sr.ReadLine()?.ToLower(); 

                    if (s != null)
                    {
                        string[] data = s.Split(',');

                        int orderID = Convert.ToInt32(data[0]);
                        int memberID = Convert.ToInt32(data[1]);
                        DateTime timeRecieved = Convert.ToDateTime(data[2]);
                        DateTime timefulfilled = Convert.ToDateTime(data[3]);
                        string option = data[4];
                        int scoops = Convert.ToInt32(data[5]);
                        // converting dipped to boolean
                        string dip = data[6];
                        if (dip == "true")
                        {
                            dipped = true;
                        }
                        string waffleFlavour = data[7];

                        // storing all the flavours 
                        string[] flavoursData = { data[8], data[9], data[10] };
                        // key = flavour, value = quantity
                        Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();

                        foreach (string f in flavoursData)
                        {
                            if (!string.IsNullOrEmpty(f))
                            {
                                type = f.Trim(); // Trim to remove leading/trailing spaces

                                // finding out flavour quantity
                                if (flavourQuantity.ContainsKey(type))
                                {
                                    // Update quantity if the key already exists
                                    flavourQuantity[type]++;
                                }
                                else
                                {
                                    // Add a new entry if the key doesn't exist
                                    flavourQuantity.Add(type, 1);
                                }
                            }
                        }

                        // now got dictionary with flavor types and their quantities
                        foreach (var kvp in flavourQuantity)
                        {
                            Flavour flavour = new Flavour(kvp.Key, premiumFlavour.Contains(kvp.Key), kvp.Value);
                            flavours.Add(flavour);
                        }

                        // storing all the toppings
                        string[] toppingsData = { data[11], data[12], data[13], data[14] };
                        foreach (string t in toppingsData)
                        {
                            if (!string.IsNullOrEmpty(t))
                            {
                                toppingType = t;
                                Topping topping = new Topping(toppingType.Trim()); // Trim to remove leading/trailing spaces
                                toppings.Add(topping);
                            }
                        }

                        Order orderHist = new Order(orderID, timeRecieved);

                        // cup option
                        if (option == "cup")
                        {
                            Cup cup = new Cup(option, scoops, flavours, toppings);
                            orderHist.AddIceCream(cup);
                        }
                        else if (option == "cone")
                        {
                            Cone cone = new Cone(option, scoops, flavours, toppings, dipped);
                            orderHist.AddIceCream(cone);
                        }
                        else if (option == "waffle")
                        {
                            Waffle waffle = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
                            orderHist.AddIceCream(waffle);
                        }
                        orderHist.TimeReceived = timeRecieved;
                        orderHist.TimeFulfilled = timefulfilled;
                        // tracks which customer has what order
                        customerOrderHistory.Add(orderHist, memberID);
                        foreach (Customer c in customerList)
                        {
                            if (c.Memberid == memberID)
                            {
                                c.OrderHistory.Add(orderHist);
                            }
                        }
                    }
                }
            }
        }

        // option 6 method - modify order details
        static void ModifyOrderDetails(List<Customer> customerList)
        {
            int index = 1;
            int option = 0;
            string nameInCSV = "";
            Customer chosenCustomer = new Customer();
            int ID = 0;

            // checking if correct ID is entered
            while (true)
            {
                Console.Write("Enter a customer ID: ");
                ID = Convert.ToInt32(Console.ReadLine());
                bool brk = false;
                foreach (Customer c in customerList)
                {
                    if (c.Memberid == ID)
                    {
                        nameInCSV += c.Name;
                        chosenCustomer = c;
                        brk = true;
                    }
                }
                if (brk == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter the correct ID please.");
                }
            }

            // checking if customer has a current order 
            if (chosenCustomer.CurrentOrder != null)
            {
                Console.WriteLine("Current order: ");
                Console.WriteLine(chosenCustomer.CurrentOrder.ToString());

                // ice cream details from current order
                Console.WriteLine("Ice cream details: ");
                foreach (IceCream ic in chosenCustomer.CurrentOrder.IceCreamList)
                {
                    Console.WriteLine($"[{index}] {ic}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine($"{nameInCSV} has no current order.\n");
            }

            // printing out option 6 menu
            Console.WriteLine("[1] choose an existing ice cream object to modify");
            Console.WriteLine("[2] add an entirely new ice cream object to the order");
            Console.WriteLine("[3] choose an existing ice cream object to delete from the order");
            while (true)
            {
                Console.Write("Enter your option: ");
                option = Convert.ToInt32(Console.ReadLine());
                if (option >= 1 && option <= 3)
                {
                    if (chosenCustomer.CurrentOrder != null && chosenCustomer.CurrentOrder.IceCreamList.Count > 0)
                    {
                        break;
                    }
                    else if (option == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You do not have any ice cream in your order. You can only choose option 2.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number from 1 to 3");
                }
            }

            if (option == 1)
            {
                Console.WriteLine("Choose an ice cream from the list above: ");
                int iceCreamIndex = Convert.ToInt32(Console.ReadLine());
                // finding out which ice cream user wants to edit 
                IceCream iceCream = chosenCustomer.CurrentOrder.IceCreamList[iceCreamIndex - 1];
                chosenCustomer.CurrentOrder.ModifyIceCream(iceCreamIndex - 1);
            }
            else if (option == 2)
            {
                if (chosenCustomer.CurrentOrder == null)
                {
                    chosenCustomer.CurrentOrder = new Order();
                }

                while (true)
                {
                    int scoops = 0;
                    Console.Write("Enter ice cream option choose from Cup / Cone / Waffle: ");
                    string iceCreamOption = Console.ReadLine().ToLower();
                    List<Flavour> flavours = MakingFlavoursList();
                    List<Topping> toppings = MakingToppingsList();
                    while (true)
                    {
                        Console.Write("Please enter new number of scoops: ");
                        int scoopsEntered = Convert.ToInt32(Console.ReadLine());
                        if (scoops >= 1 && scoops <= 3)
                        {
                            scoops = scoopsEntered;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("please enter an integer from 1 to 3");
                        }
                    }
                    if (iceCreamOption == "cup")
                    {
                        Cup cup = new Cup(iceCreamOption, scoops, flavours, toppings);
                        chosenCustomer.CurrentOrder.AddIceCream(cup);
                        break;
                    }
                    else if (iceCreamOption == "cone")
                    {
                        bool dipped = false;
                        Console.Write("Do you want your cone to be dipped Y / N");
                        string dippedChoice = Console.ReadLine().ToLower();
                        if (dippedChoice == "y")
                        {
                            dipped = true;
                        }
                        Cone cone = new Cone(iceCreamOption, scoops, flavours, toppings, dipped);
                        chosenCustomer.CurrentOrder.AddIceCream(cone);
                        break;
                    }
                    else if (iceCreamOption == "waffle")
                    {
                        string waffleFlavour = "original";
                        Console.Write("Do you want to change your waffle flavour Y / N ");
                        string waffleFlavourOption = Console.ReadLine().ToLower();
                        if (waffleFlavourOption == "y")
                        {
                            Console.WriteLine("We've got Red Velvet / Charcoal / Pandan options: ");
                            waffleFlavour = Console.ReadLine().ToLower();
                        }
                        Waffle waffle = new Waffle(iceCreamOption, scoops, flavours, toppings, waffleFlavour);
                        chosenCustomer.CurrentOrder.AddIceCream(waffle);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please choose from the options above.");
                    }
                }
            }

            else
            {
                int indexing = 1;
                if (chosenCustomer.CurrentOrder == null || chosenCustomer.CurrentOrder.IceCreamList.Count() == 1)
                {
                    Console.WriteLine("Sorry you cannot have zero orders.");
                }
                else
                {
                    foreach (IceCream ic in chosenCustomer.CurrentOrder.IceCreamList)
                    {
                        Console.WriteLine($"[{indexing}]{ic}");
                        indexing++;
                    }
                    Console.WriteLine("Please choose an ice cream to delete, enter the number: ");
                    int chosenIndex = Convert.ToInt32(Console.ReadLine());
                    chosenCustomer.CurrentOrder.IceCreamList.RemoveAt(chosenIndex - 1);
                }
            }
            Console.WriteLine("Here is your updated order: ");
            if (chosenCustomer.CurrentOrder.IceCreamList.Count > 0)
            {
                Console.WriteLine("Ice cream details: ");
                foreach (IceCream ic in chosenCustomer.CurrentOrder.IceCreamList)
                {
                    Console.WriteLine(ic);
                }
            }
            else
            {
                Console.WriteLine("No ice cream in the order.");
            }


        }
        // making new flavoursList
        static List<Flavour> MakingFlavoursList()
        {
            List<Flavour> flavours = new List<Flavour>();
            bool premium = false;
            int quantity = 0;
            Console.WriteLine("For regular flavours we've got Vanilla / Chocolate / Strawberry options");
            Console.WriteLine("For premium flavours we've got Durian / Ube / Sea salt options");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Flavour choice? If you don't want to add anymore flavours enter N: ");
                string flavourType = Console.ReadLine().ToLower();
                if (flavourType == "n")
                {
                    break;
                }
                else
                {
                    quantity++;
                    if (flavourType == "durian" || flavourType == "ube" || flavourType == "sea salt")
                    {
                        premium = true;
                    }
                }
                Flavour flavour = new Flavour(flavourType, premium, quantity);
                flavours.Add(flavour);
            }
            return flavours;
        }
        // making new topping list
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
     
    }
}
