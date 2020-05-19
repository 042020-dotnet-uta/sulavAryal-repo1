using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GettingStartedClient.ServiceReference1;

namespace GettingStartedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step 1: Create an instance of the WCF proxy.
            FridgeClient client = new FridgeClient();


            var fridgeContent = client.FridgeContents();
            Console.WriteLine("Fridge intially has these fruits in it.");
            foreach (var item in fridgeContent)
            {
                Console.WriteLine(item);
            }

            int result = 0;
            result = client.GetFruitTotal();
            Console.WriteLine($"Total fruits in fridge is : {result}\n");



            string fruit = "Papaya";
            result = client.AddFruit("Papaya");
            Console.WriteLine($"{fruit} added in");

            fridgeContent = client.FridgeContents();
            Console.WriteLine("Fridge now has these fruits in it.");
            foreach (var item in fridgeContent)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Total fruits in fridge is : {result}\n");

            result = client.TakeFruit(fruit);
            Console.WriteLine($"\nI took out the {fruit} and ate it");

            fridgeContent = client.FridgeContents();
            Console.WriteLine("Fridge now has these fruits in it.");
            foreach (var item in fridgeContent) 
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Total fruits in fridge is : {result}\n");


            // Step 3: Close the client to gracefully close the connection and clean up resources.
            Console.WriteLine("\nPress <Enter> to terminate the client.");
            Console.ReadLine();
            client.Close();
        }
    }
}