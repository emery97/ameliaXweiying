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
        //Basic feature 1's method
        static void InitCustomerList(List<Customer> customerList, Queue<Customer> goldQueue, Queue<Customer> normalQueue)
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
        // To list current customers
        static void ListCustomer()
        {
            Console.WriteLine("Our Current Customers: ");
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                string s = sr.ReadLine(); //header
                string[] header = s.Split(',');
                Console.WriteLine($"{header[0],-10} {header[1],-10} {header[2]}");
                while ( (s = sr.ReadLine()) != null)
                {
                    string[] data = s.Split(',');
                    Console.WriteLine($"{data[0],-10} {data[1],-10} {data[2]}");
                }
            }
        }
        //Basic feature 3's method 

        void RegisterCustomer(List<Customer> customerList)  //Parameter list since need this list later due to the fact that you need add this new registered customer to the customer list.
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

            //Appending..

            try  //If can append successfully
            {
                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    sw.WriteLine($"{newName},{newId},{newDob.ToString("dd/MM/yy")},{newTier},{newPoints},{newPC}");
                }
                Console.WriteLine("Customer registered successfully.");
                Console.WriteLine("");

            }

            catch (Exception ex)  //In case got any other error (Exception ex catch any and all types of exception so this is if eg User anyhow enter somethiing, then cannot be registered alrd..
            {
                Console.WriteLine("Customer could not be registered.");
                Console.WriteLine($"Reason: {ex.Message}");
                Console.WriteLine("");
            }

        }


        //Basic feature 4's method
        void CreateOrder(List<Order> OrderHistory, List<Customer> customerList) //Because need both of these for method
        {

            // Firstly, generate order ID for each order (for Order class parameter)
            Random random = new Random();  //Creating a object variable named 'random' of the random class type
            int orderID = random.Next(1000, 9999);


            //List customers from file
            ListCustomer();

            Console.Write("Select a customer ID: ");  //Better to key in user ID than name in case got people have same name
            int selectedCustomerID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter your ice cream option: ");
            string userOption = Console.ReadLine().ToLower();
            Console.Write("Enter your ice cream scoops amount in numbers '1', '2' or '3': ");
            int userScoops = Convert.ToInt32(Console.ReadLine());

            List<Flavour> userFlavourList = new List<Flavour>();
            for (int i = 1; i <= userScoops; i++)
            {
                Console.Write($"Enter the flavor for scoop {i}: ");
                string userType = Console.ReadLine().ToLower(); //Type as in the TYPE OF FLAVOUR

                // Determine if the flavor is premium 
                bool userPremium = false; //Aka non premium flavours
                if (userType == "Durian" || userType == "durian" || userType == "Ube" || userType == "ube" || userType == "Sea salt" || userType == "sea salt")
                {
                    userPremium = true;
                }

                //Flavour object with quantity = 1 since each scoop is just one quantity
                Flavour userFlavour = new Flavour(userType, userPremium, 1);
                userFlavourList.Add(userFlavour);
            }

            List<Topping> userToppingList = new List<Topping>();
            while (true)
            {
                Console.Write("Enter your chosen toppings, if desired. Otherwise, enter 'NO' and once done, enter 'DONE': ");
                string userToppingChoice = Console.ReadLine().ToUpper();

                if (userToppingChoice == "NO" || userToppingChoice == "DONE")
                {
                    break;
                }
                else
                {
                    Topping userTopping = new Topping(userToppingChoice);  //Need make topping of class topping first before adding into list since the list requires its objects to be of type Topping since diagram for class states List<TOPPINGS!!!>
                    userToppingList.Add(userTopping);
                }
            }

            IceCream userIceCream = null; //Initialize this first before sorting them accordingly to their subclasses like Cup, Cone etc

            if (userOption == "Cup" || userOption == "cup")
            {
                userIceCream = new Cup(userOption, userScoops, userFlavourList, userToppingList);
            }

            else if (userOption == "Cone" || userOption == "cone")
            {
                bool userDipped = false;
                Console.Write("Is the cone dipped in chocolate [Yes/No]: ");
                string userDippedAnswer = Console.ReadLine();
                if (userDippedAnswer == "Yes")
                {
                    userDipped = true;
                }
                userIceCream = new Cone(userOption, userScoops, userFlavourList, userToppingList, userDipped);
            }

            else if (userOption == "Waffle" || userOption == "waffle")
            {
                Console.Write("Enter waffle flavour [Original/Red velvet/Charcoal/Pandan]: ");
                string userWaffle = Console.ReadLine().ToLower();
                userIceCream = new Waffle(userOption, userScoops, userFlavourList, userToppingList, userWaffle);
            }

            else
            {
                Console.WriteLine("Please enter a valid option of Cup, Cone or Waffle.");
            }

            foreach (Customer customerData in customerList)
            {
                if (customerData.Memberid == selectedCustomerID)
                {
                    Console.WriteLine($"You have selected {customerData.Name}.");
                    Order selectedCustomerOrder = new Order(orderID, DateTime.Now); //Parameters shld be int id and datetime TimeReceived
                    selectedCustomerOrder.IceCreamList = new List<IceCream>();
                    selectedCustomerOrder.IceCreamList.Add(userIceCream);
                    OrderHistory.Add(selectedCustomerOrder);
                    break; //Exit loop once customer is found! 
                }
            }

            //CONTINUEFROM WHERE YOU HIGHLIGHTED AND  REORGANIZE CODE SUCH THAT IS MORE NEAT FOR THIS BASIC STEP 4 BC NOW IS Q CONFUSINF...

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
        static void GettingOrderHistory(Dictionary<Order, int> customerOrderHistory)
        {
            List<string> premiumFlavour = new List<string> { "durian", "ube", "sea salt" };

            using (StreamReader sr = new StreamReader("orders.csv"))
            {
                string header = sr.ReadLine(); // headers

                while (!sr.EndOfStream) //Ensures all data read
                {
                    List<Flavour> flavours = new List<Flavour>();
                    List<Topping> toppings = new List<Topping>();

                    bool dipped = false;

                    // flavours variable
                    string type = "";
                    int quantity = 1;

                    // toppings variable
                    string toppingType = "";
                    string s = sr.ReadLine()?.ToLower(); // Using ?. to handle null
                    bool premium = false;

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
                        Dictionary<string, int> flavourQuantity = new Dictionary<string, int>();

                        foreach (string f in flavoursData)
                        {
                            if (!string.IsNullOrEmpty(f))
                            {
                                type = f.Trim(); // Trim to remove leading/trailing spaces

                                // check if it's a premium flavour
                                premium = premiumFlavour.Contains(type);

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

                        // Now you have a dictionary with flavor types and their quantities
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
                                toppingType += t + " ";
                            }
                        }
                        Topping topping = new Topping(toppingType.Trim()); // Trim to remove leading/trailing spaces
                        toppings.Add(topping);

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
                        orderHist.TimeFulfilled = timefulfilled;
                        // tracks which customer has what order
                        customerOrderHistory.Add(orderHist, memberID);
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
        static void Main(string[] args)
        {
            while (true)
            {
                Queue<Customer> goldQueue = new Queue<Customer>();
                Queue<Customer> normalQueue = new Queue<Customer>();
                List<Customer> customerList = new List<Customer>();
                Dictionary<Order, int> customerOrderHistory = new Dictionary<Order, int>();

                // initialise customer list
                InitCustomerList(customerList,goldQueue,normalQueue);
                Console.WriteLine(new String('-', 10) + "MENU" + new string('-',10));
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
                    Console.WriteLine("Please come again next time!!");
                    break;
                }
                else if (userOption == "1")
                {
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
                    //RegisterCustomer(customerList);
                }
                else if (userOption == "4")
                {
                    ListCustomer();
                }
                else if (userOption == "5")
                {
                    ListCustomer();
                    GettingOrderHistory(customerOrderHistory);
                    OrderDetails(customerList, customerOrderHistory);
                }
                else if (userOption == "6")
                {
                    ListCustomer();
                    ModifyOrderDetails(customerList);
                }
            }
        }
    }
}
