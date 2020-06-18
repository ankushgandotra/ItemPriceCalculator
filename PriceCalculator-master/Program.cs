using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PriceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ShowForm();
            }

            catch (Exception ex)
            { 
            // write exception to logger
            }
        }

        /// <summary>
        /// Function to show input form and output to user
        /// </summary>
       public static void ShowForm()
        {
            var myJsonString = File.ReadAllText("Items.json");
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(myJsonString);

            Console.WriteLine("Following items are available for purchase - \n" );
            foreach (Item item in items)
            {
                Console.WriteLine("Item Name: {0}- {1}{2} {3} ", item.Description, item.Price,item.PriceUnit, item.Size);

            }
            Console.WriteLine("Please enter the quantity of each item you would like to buy following by space. Enter 0 for item not needed. \n");
            Console.Write("Items- ");

            string[] quantities = Console.ReadLine().Trim().Split(' ');
            
            if (quantities.Count() == 0)
            {
                Console.WriteLine("Values entered for item quantity is not correct. Please enter again ");
                Console.Write("Items- ");
            }

            // chec if input is valid and calculate total price
            if (Helper.CheckInteger(quantities))
            {
                for(int i=0; i< items.Count; i ++)
                {
                    if (i< quantities.Length)
                    items[i].Quantiity = Convert.ToInt32(quantities[i]);
                }
                CalculatePrice calculateAmount = new CalculatePrice();
                PriceCalculation totoalAmount= calculateAmount.CalculateTotalPrice(items);

                Console.WriteLine("Subtotal: \u00A3{0}", Math.Round(totoalAmount.Subtotoal,2));

                foreach (ItemDiscount item in totoalAmount.ItemDiscount)
                {
                    if (!String.IsNullOrEmpty(item.DiscountPercentage.ToString()) && item.DiscountAmount > 0)
                    {
                        Console.WriteLine("{0}- {1}% off: -\u00A3{2}", item.Description, item.DiscountPercentage, Math.Round(item.DiscountAmount, 2));
                    }
                    
                }               
                Console.WriteLine("Total discount: \u00A3{0}", Math.Round(totoalAmount.TotalDiscount, 2));
                if (totoalAmount.TotalDiscount == 0)
                {
                    Console.WriteLine("(No offers available)");
                }
                Console.WriteLine("Total: \u00A3{0}", Math.Round((totoalAmount.Subtotoal-totoalAmount.TotalDiscount), 2));

                Console.WriteLine("\nPress any key to start again.");
            }
            // if input is not valid, ask user to enter again
            else 
            {
                Console.WriteLine("Values entered for item quantity is not correct. Please enter again ");
                Console.Write("Items- ");
            }
            ConsoleKey readKey = Console.ReadKey().Key;
            if (readKey == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else 
            {
                ShowForm();
            }

        }
    }
}
