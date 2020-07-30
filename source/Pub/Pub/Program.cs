using System;
using Microsoft.Extensions.DependencyInjection;
using Pub.Repositories;
using Pub.Repositories.Contracts;
using Pub.Services;
using Pub.Services.Contracts;

namespace Pub
{
    internal class Program
    {
        private static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IBeverageRepository, BeverageHardCodedRepository>()
                .AddSingleton<IBeverageService, BeverageService>()
                .AddSingleton<IPubPrice, PubPrice>()
                .BuildServiceProvider();

            var beverageService = serviceProvider.GetService<IBeverageService>();
            var pubPrice = serviceProvider.GetService<IPubPrice>();

            string command = null;

            PrintMenu(beverageService);

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

        private static void PrintMenu(IBeverageService beverageService)
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Please place your order with following command: [drink] [isStudent] [amount] (Example: hansa true 2)");
            Console.WriteLine("Type exit to close the program");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Menu");
            Console.WriteLine("********************************************************");
            var drinks = beverageService.GetAllBeverages();
            foreach (var beverage in drinks)
            {
                Console.WriteLine(beverage.Name);
            }
            Console.WriteLine("********************************************************");
        }
    }
}
