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
        static void InitCustomerList(List<Customer> customerList)
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

                        Customer customerDetails = new Customer(Name, Memberid, Dob);  // This line is making object 
                        customerList.Add(customerDetails); // to add the customers in the customerList
                        Console.WriteLine(customerDetails);
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
                Console.WriteLine("Customers in gold queue: ");
                foreach(Customer c in goldQueue)
                {
                    foreach (Order o in c.OrderHistory)
                    {
                        Console.Write(o+"\t");
                    }
                }
            }
            // if no one in queue
            else
            {
                Console.WriteLine("There is no one in the gold queue.");
            }
            if (normalQueue.Count() > 0)
            {
                Console.WriteLine("Customers in normal queue: ");
                foreach (Customer c in normalQueue)
                {
                    foreach (Order o in c.OrderHistory)
                    {
                        Console.Write(o + "\t");
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



        /*  !!!!***ERRORS WHEN RUNNING THIS STEP 3: Firstly, something wrong with the DateTime.... TRY USER INPUT AND YOULL SEE WHAT I MEAN */



        void RegisterCustomer(List<Customer> customerList)  //??? Is the parameter a list...?? Do I even need a list.... same for above's method ^^^..
        {
            Console.Write("Enter customer's Name: ");
            string newName = Console.ReadLine();
            Console.Write("Enter customer's Id: ");
            int newId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter customer's date of birth: ");
            DateTime newDob = Convert.ToDateTime(Console.ReadLine());

            Customer newCustomer = new Customer(newName, newId, newDob);
            customerList.Add(newCustomer);

            //Since PointCard constructor parameters are int, int which is for Points and PunchCard
            PointCard newCustomerPC = new PointCard(newCustomer.Rewards.Points, newCustomer.Rewards.PunchCard); //Need .Rewards because Rewards in customer class and is also of type PointCard. (is like the example giving in slides with John.Addr.Shipping)

            newCustomer.Rewards = newCustomerPC; //Assigning of PointCard to customer is done in this code. (Rewards is of class type PointCard, but it is stored in the customer class) So thats why can equate pointcard object to another pointcard obj

            //Appending..

            try  //If can append successfully
            {
                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    sw.WriteLine($"{newName},{newId},{newDob.ToString("dd/MM/yy")}");
                }
                Console.WriteLine("Customer registered successfully.");

            }

            catch (Exception ex)  //In case got any other error (Exception ex catch any and all types of exception so this is if eg User anyhow enter somethiing, then cannot be registered alrd..
            {
                Console.WriteLine("Customer could not be registered.");
                Console.WriteLine($"Reason: {ex.Message}");
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
            string userOption = Console.ReadLine();
            Console.Write("Enter your ice cream scoops amount in numbers '1', '2' or '3': ");
            int userScoops = Convert.ToInt32(Console.ReadLine());

            List<Flavour> userFlavourList = new List<Flavour>();
            for (int i = 1; i <= userScoops; i++)
            {
                Console.Write($"Enter the flavor for scoop {i}: ");
                string userType = Console.ReadLine(); //Type as in the TYPE OF FLAVOUR

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
                string userToppingChoice = Console.ReadLine();

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
                string userWaffle = Console.ReadLine();
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
        // DOUBLE CHECK THE OUTPUT, NEED ICE CREAM DETAILS 
        static void OrderDetails(List<Customer> customerList)
        {
            Console.Write("Please choose a customer: ");
            string name = Console.ReadLine();
            Customer chosenCustomer = new Customer(); 
            foreach(Customer c in customerList)
            {
                if (c.Name == name)
                {
                    chosenCustomer = c;
                }
            }
            Console.WriteLine("Current order: ");
            Console.WriteLine(chosenCustomer.CurrentOrder.ToString());
            // ice cream details from current order
            Console.WriteLine("Ice cream details: ");
            foreach (IceCream ic in chosenCustomer.CurrentOrder.IceCreamList)
            {
                Console.WriteLine(ic);
            }

            Console.WriteLine("\nPrevious order(s): ");
            List<Order> orderHist = chosenCustomer.OrderHistory;
            foreach(Order o in orderHist)
            {
                Console.WriteLine(o.ToString());
                Console.WriteLine("Ice cream details: ");
            }
        }
        static void Main(string[] args)
        {
            Queue<Customer> goldQueue = new Queue<Customer>();
            Queue<Customer> normalQueue = new Queue<Customer>();
            List<Customer> customerList = new List<Customer>();
            while (true)
            {
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
                    InitCustomerList(customerList);
                }
                else if (userOption == "2")
                {
                    ListOrder(customerList, goldQueue, normalQueue);
                }
                else if (userOption == "5")
                {
                    ListCustomer();
                    OrderDetails(customerList);
                }

            }
        }
    }
}
