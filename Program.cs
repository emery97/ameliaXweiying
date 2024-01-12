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
                string? header = sr.ReadLine(); //Ignore headers

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
                        customerList.Add(customerDetails);

                        Console.WriteLine(customerDetails);
                    }
                }
            }
        }
        //Basic feature 2's method
        void ListOrder(List<Customer> customerList)
        {
            foreach(Customer c in customerList)
            {
                if (c.Tier)
            }
        }

        //Basic feature 3's method
        static void RegisterCustomer(List<Customer> customerList)  //??? Is the parameter a list...?? Do I even need a list.... same for above's method ^^^..
        {
            Console.Write("Enter customer's details in the format of 'name, id, date of birth': ");
            string newCustomerInfo = Console.ReadLine();
            string[] customerInfoSplit = newCustomerInfo.Split(","); //Split -> so become array ! 

            string newName = customerInfoSplit[0];
            int newId = Convert.ToInt32(customerInfoSplit[1]);  //OG data type is string thus need convert
            DateTime newDob = Convert.ToDateTime(customerInfoSplit[2]);

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

        /*Basic feature 4's method
        static void CreateOrder(???)
        {
          CONTINUEFROM HERE... IDK WHAGTS PARAMETER TO PUT ASLO ETC ETC
        } AND REMEMBER TO COMMENT OUT ALL CODE WITH ICECREAM IF U WANNA RUN UR PRGM! OR ELSE WAIT FOR WEIYING SEND U HERCODE!
        */



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

                //Basic feature 1 
                if (userOption == "1")
                {
                    //Calling out the method
                    InitCustomerList(customerList);
                }


                if (userOption == "2")
                {

                }

                //Basic feature 3
                if (userOption == "3")
                {
                    //Call out 
                    RegisterCustomer(customerList);
                }

                //Basic feature 4
                if (userOption == "4")
                {
                    CreateOrder(??);
                }

            }
        }
    }
}
