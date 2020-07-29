using System;

namespace Pub
{
    internal class Program
    {
        public const string OneBeer = "hansa";
        public const string OneCider = "grans";
        public const string AProperCider = "strongbow";
        public const string Gt = "gt";
        public const string BacardiSpecial = "bacardi_special";

        private static void Main()
        {
            var pubPrice = new PubPrice();
            string command = null;

            PrintMenu();

            while (string.IsNullOrEmpty(command) || command.Trim().ToLower() != "exit")
            {
                try
                {
                    command = Console.ReadLine();
                    var (drink, student, amount) = pubPrice.ParseOrder(command);
                    var cost = pubPrice.ComputeCost(drink, student, amount);
                    Console.WriteLine($"Your price: {cost}");
                }
                catch (PubOrderException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong. Contact the pub administrator");
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Please place your order with following command: [drink] [isStudent] [amount] (Example: hansa true 2)");
            Console.WriteLine("Type exit to close the program");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Menu");
            Console.WriteLine("********************************************************");
            Console.WriteLine(OneBeer);
            Console.WriteLine(OneCider);
            Console.WriteLine(AProperCider);
            Console.WriteLine(Gt);
            Console.WriteLine(BacardiSpecial);
            Console.WriteLine("********************************************************");
        }
    }
}
