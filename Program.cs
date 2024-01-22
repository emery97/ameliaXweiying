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
using static System.Formats.Asn1.AsnWriter;

namespace Code
{
    class Program
    {
        // main program
        static void Main(string[] args)
        {
            List<Customer> customerList = new List<Customer>();
            Dictionary<Order, int> customerOrderHistory = new Dictionary<Order, int>();
            Queue<Order> goldOrderqueue = new Queue<Order>();  //This is for basic 4, for me to append the orders into queues
            Queue<Order> regularOrderQueue = new Queue<Order>();
            while (true)
            {

                InitCustomerList(customerList, customerOrderHistory);
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
                        Console.WriteLine(c.ToString()); //Output will have some with and without order history because of csv file. Then some will have 2 diff order IDs in their order history bc they order more than 1 time and order ID changes EACH TIME they order. NOT EACH ICE CREAM THEY ORDER BUT EACH TIME THEY ORDER
                    }
                }
                else if (userOption == "2")
                {
                    ListOrder(customerList, goldOrderqueue, regularOrderQueue);
                }
                else if (userOption == "3")
                {
                    RegisterCustomer(customerList);
                }
                else if (userOption == "4")
                {
                    ListCustomer();
                    // choosing customer
                    int ID = 0;
                    int orderID = 0;
                    DateTime timeReceived = DateTime.Now;
                    Order newOrder = new Order(orderID, timeReceived);
                    Customer chosenCustomer = new Customer();
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
                                chosenCustomer = c;
                                brk = true;
                            }
                        }
                        if (brk == true)
                        {
                            newOrder = chosenCustomer.MakeOrder();
                            AppendingOrder(goldOrderqueue, regularOrderQueue, chosenCustomer);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter the correct ID please.");
                        }
                    }
                    while (true)
                    {
                        Console.Write("Would you like to add another ice cream Y / N: ");
                        string goAgain = Console.ReadLine().ToLower();
                        if (goAgain == "n")
                        {
                            break;
                        }
                        else
                        {
                            newOrder = chosenCustomer.MakeOrder();
                            AppendingOrder(goldOrderqueue, regularOrderQueue, chosenCustomer);
                        }
                    }

                }
                else if (userOption == "5")
                {
                    ListCustomer();
                    GettingOrderHistory(customerOrderHistory, customerList);
                    OrderDetails(customerList, customerOrderHistory);
                }
                else if (userOption == "6")
                {

                    ListCustomer();  // Assuming this lists all customers and their IDs.
                    Console.Write("Enter a customer ID: ");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    Customer chosenCustomer = FindCustomerById(customerList, customerId);

                    if (chosenCustomer != null)
                    {
                        ModifyOrderDetails(chosenCustomer);
                    }
                    else
                    {
                        Console.WriteLine("Customer not found.");
                    }

                    /*
                    ListCustomer();
                    Customer chosenCustomer = new Customer();
                    Order chosenCustomerCurrentOrder = new Order();
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
                                chosenCustomer = c;
                                chosenCustomerCurrentOrder = chosenCustomer.CurrentOrder;
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

                    //List ice cream objects contained inside current order. current order is of order type
                    Console.WriteLine(chosenCustomerCurrentOrder.ToString());

                    ModifyOrderDetails(customerList, chosenCustomer); */
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
        static void InitCustomerList(List<Customer> customerList, Dictionary<Order, int> customerOrderHistory)
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
                    }


                }
            }
        }

        // option 2 method - display customer info in both queues
        static void ListOrder(List<Customer> customerList, Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue)
        {
            // making sure got people in queue
            if (goldOrderQueue.Count() > 0)
            {
                Console.WriteLine("Customers in gold queue:");
                foreach (Order o in goldOrderQueue)
                {
                    if (o != null)
                    {
                        Console.WriteLine(o.ToString());
                    }
                }
            }
            // if no one in queue
            else
            {
                Console.WriteLine("There is no one in the gold queue.");
            }

            // making sure got people in queue
            if (regularOrderQueue.Count() > 0)
            {
                Console.WriteLine("\nCustomers in normal queue: ");
                foreach (Order o in regularOrderQueue)
                {
                    if (o != null)
                    {
                        Console.WriteLine(o.ToString());
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
        // appending order to the correct queue
        static void AppendingOrder(Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue, Customer chosenCustomer)
        {
            if (chosenCustomer.Rewards.Tier == "Gold")
            {
                goldOrderQueue.Enqueue(chosenCustomer.CurrentOrder);
                Console.WriteLine("Order has been made successfully to the gold queue!!");
            }
            else
            {
                regularOrderQueue.Enqueue(chosenCustomer.CurrentOrder);
                Console.WriteLine("Order has been made successfully to the regular queue!!");
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
        static void GettingOrderHistory(Dictionary<Order, int> customerOrderHistory, List<Customer> customerList) //Use Order as key in the customerOrderHistory and not int bc int would represent the id in the orders.csv class. But then the key would have repeated values bc have multiple data with id as 3. And dictionary cannot have key with same values
        {
            string[] premiumFlavour = { "durian", "ube", "sea salt" }; //This is array so later can see if the flavour user choose is premium

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
                        string[] flavoursData = { data[8], data[9], data[10] }; //This is array. I store all the flavour types in the csv file IN this array. So that later i can use loop and determine quantity
                        // key = flavour, value = quantity
                        Dictionary<string, int> flavourQuantity = new Dictionary<string, int>(); //So later can use function .ContainsKey and see if the flavour alrd exist in dict. If yes, then just add its quantity. If no, then i make quanitty = 1 first.

                        foreach (string f in flavoursData) //Loop through the array of csv flavour types so later can compare with exisitng flavours in dictionary called flavourQuantity
                        {
                            if (!string.IsNullOrEmpty(f)) 
                            {
                                type = f.Trim(); // Trim to remove leading/trailing spaces

                                // finding out flavour quantity
                                if (flavourQuantity.ContainsKey(type))
                                {
                                    // Update quantity if the key already exists
                                    flavourQuantity[type]++; //flavourQuantity[type] is dictionary[yourchosenkey] which will give you the value aka quantity 
                                }
                                else
                                {
                                    // Add a new entry  to dictionary if the key doesn't exist
                                    flavourQuantity.Add(type, 1); 
                                }
                            }
                        }

                        // now got dictionary with flavor types and their quantities
                        foreach (var kvp in flavourQuantity)
                        {
                            Flavour flavour = new Flavour(kvp.Key, premiumFlavour.Contains(kvp.Key), kvp.Value); //premiumFlavour.Contains(kvp.Key) is using premiumFlavours aka the array to see if the flavour type is inside the array which shows all premiumFlavours. 
                            flavours.Add(flavour); //Adding into list made earlier
                        }

                        // storing all the toppings
                        string[] toppingsData = { data[11], data[12], data[13], data[14] }; //Same concept as storing the flavours from csv file in an array jn
                        foreach (string t in toppingsData)
                        {
                            if (!string.IsNullOrEmpty(t))
                            {
                                toppingType = t;
                                Topping topping = new Topping(toppingType.Trim()); // Trim to remove leading/trailing spaces
                                toppings.Add(topping);
                            }
                        }

                        Order orderHist = new Order(orderID, timeRecieved); //make a order obj called orderHist, with parameters  on data in csv file.

                        // cup option
                        if (option == "cup")
                        {
                            Cup cup = new Cup(option, scoops, flavours, toppings);
                            orderHist.AddIceCream(cup);  //Only can access AddIceCream method when you have smth of order class. And the thing you add inside must be icecream class aka cup/cone/waffle
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

                        orderHist.TimeReceived = timeRecieved; //equating the order history made above to the timerecieved and timefulfilled from the csv file.
                        orderHist.TimeFulfilled = timefulfilled;
                        // tracks which customer has what order
                        customerOrderHistory.Add(orderHist, memberID); //Adding these data to dictionary called customerOrderHistory which is declared above. 
                        //^^Btw, this is inside the reading of csv files line by line, so the data would be stored line by line. Dont have to worry about data getting missed out bc all data is gone through line by line
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

        //Option 6 method 

        static Customer FindCustomerById(List<Customer> customerList, int customerId)
        {
            return customerList.FirstOrDefault(c => c.Memberid == customerId);
        }

        void ModifyOrderDetails(Customer customer)
        {
            if (customer.CurrentOrder == null || customer.CurrentOrder.IceCreamList.Count == 0)
            {
                Console.WriteLine("There are no ice creams in the order.");
                return;
            }

            Console.WriteLine("Current order: ");
            for (int i = 0; i < customer.CurrentOrder.IceCreamList.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {customer.CurrentOrder.IceCreamList[i]}");
            }

            Console.WriteLine("[1] Modify an existing ice cream");
            Console.WriteLine("[2] Add a new ice cream");
            Console.WriteLine("[3] Delete an existing ice cream");
            Console.Write("Enter your option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.Write("Select the number of the ice cream to modify: ");
                    int iceCreamIndex = Convert.ToInt32(Console.ReadLine()) - 1; // Adjusting for zero-based index
                    if (iceCreamIndex < 0 || iceCreamIndex >= customer.CurrentOrder.IceCreamList.Count)
                    {
                        Console.WriteLine("Invalid ice cream number.");
                    }
                    else
                    {
                        customer.CurrentOrder.ModifyIceCream(iceCreamIndex);
                    }
                    break;
                case 2:
                    AddIceCream(customer.CurrentOrder);
                    break;
                case 3:
                    DeleteIceCream(customer.CurrentOrder);
                    break;
                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }

            Console.WriteLine("Updated order: ");
            foreach (IceCream ic in customer.CurrentOrder.IceCreamList)
            {
                Console.WriteLine(ic);
            }
        }

        void AddNewIceCream(Order order)
        {
            // Prompt user for ice cream details and create a new IceCream object
            // Example:
            Console.Write("Enter ice cream option (Cup/Cone/Waffle): ");
            string option = Console.ReadLine();

            // Additional details like scoops, flavours, toppings etc.
            // ...

            IceCream newIceCream = new IceCream(option, scoops, flavours, toppings);
            order.AddIceCream(newIceCream);
        }

        void DeleteIceCream(Order order)
        {
            if (order.IceCreamList.Count == 0)
            {
                Console.WriteLine("No ice creams available to delete.");
                return;
            }

            Console.WriteLine("Select an ice cream to delete:");
            for (int i = 0; i < order.IceCreamList.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {order.IceCreamList[i]}");
            }

            int indexToDelete = Convert.ToInt32(Console.ReadLine()) - 1;
            if (indexToDelete < 0 || indexToDelete >= order.IceCreamList.Count)
            {
                Console.WriteLine("Invalid selection.");
            }
            else
            {
                order.DeleteIceCream(indexToDelete);
            }
        }


        // option 6 method - modify order details
        /*
        static void ModifyOrderDetails(List<Customer> customerList, Customer chosenCustomer)
        {
            
            int option = 0;
            // checking if customer has a current order 
            if (chosenCustomer.CurrentOrder != null)
            {
                int index = 0;
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
                    if (chosenCustomer.CurrentOrder != null) //Aka customers have order, can do option 1 and 3 since order exists.
                    {
                        break;
                    }
                    else if (option == 2) 
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You do not have any ice cream in your order. You can only choose option 2."); //If option is NOT 2 and they DONT HAVE order, then have error
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number from 1 to 3"); //If they anyhow input number like 10 or -2
                }
            }

            if (option == 1)
            {
                Console.WriteLine("Choose an ice cream from the list above: ");
                int iceCreamIndex = Convert.ToInt32(Console.ReadLine());
                // finding out which ice cream user wants to edit 
                IceCream iceCream = chosenCustomer.CurrentOrder.IceCreamList[iceCreamIndex - 1];
                chosenCustomer.CurrentOrder.ModifyIceCream(iceCreamIndex);
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
                    List<Flavour> flavours = MakingFlavoursList(scoops);
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
                    chosenCustomer.CurrentOrder.DeleteIceCream(chosenIndex - 1); // need to use method in class
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
        static List<Flavour> MakingFlavoursList(int scoops) //own method to use later
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
        static List<Topping> MakingToppingsList() //own method to use later
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
        } */

    }  
}  
