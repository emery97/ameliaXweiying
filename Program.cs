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
            Console.WriteLine("\nPrevious order(s): ");
            List<Order> orderHist = chosenCustomer.OrderHistory;
            foreach(Order o in orderHist)
            {
                Console.WriteLine(o.ToString());
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
