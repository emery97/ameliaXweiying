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

namespace S10258645_PRG2Assignment
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
                    string? s = sr.ReadLine();

                    if (s != null) // Aka what will b carried out if there is still data
                    {
                        string[] customerData = s.Split(","); //When spilt, become an array + rmb tht if want print data out, must use foreach loop since this is an array

                        string Name = customerData[0];
                        int Memberid = Convert.ToInt32(customerData[1]);
                        DateTime Dob = Convert.ToDateTime(customerData[2]);

                        Customer customerDetails = new Customer(Name, Memberid, Dob);  // This line is making object 
                        Console.WriteLine(customerDetails);
                    }
                }
            }
        }
        // option 2 method
        void ListOrder(List<Customer> customerList)
        {
            foreach(Customer c in customerList)
            {
                if (c.Tier)
            }
        }
        static void Main(string[] args)
        {
            Queue<Customer> goldQueue = new Queue<Customer>();
            Queue<Customer> normalQueue = new Queue<Customer>();
            List<Customer> customerList = new List<Customer>();
            while (true)
            {
                Console.WriteLine("[1] List all customers");
                Console.WriteLine("[2] List all current orders");
                Console.WriteLine("[3] Register a new customer");
                Console.WriteLine("[4] Create a customer's order");
                Console.WriteLine("[5] Display order details of a customer");
                Console.WriteLine("[6] Modify order details");
                Console.WriteLine("[0] Exit");

                Console.Write("Enter your option: ");
                string userOption = Console.ReadLine();

                if (userOption == "0")
                {
                    break;
                }
                if (userOption == "2")
                {

                }

            }
        }
    }
}
