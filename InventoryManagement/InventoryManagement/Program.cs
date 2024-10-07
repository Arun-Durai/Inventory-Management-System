using InventoryManagement.Domain.ProductManagement;
using InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Program
{
    private static void Main(string[] args)
    {
        //Product.StockThreshold = 8;

        //Product.ChangeStockThreshold(10);

        //Price samplePrice = new Price(10, Currency.Euro);
        ////Product p1 = new Product(1, "Sugar", "LoremIspum", samplePrice, UnitType.PerBox, 100);

        //Product p1 = new Product(1) { Name = "Sugar", Description= "LoremIspum", Price = samplePrice,UnitType = UnitType.PerKg };

        //p1.IncreaseStock(10);
        //p1.Description = "Sample Description";

        //var p2 = new Product(2)
        //{
        //    Name = "Cake Decorations",
        //    Description = "LoremIspum",
        //    Price = new Price() { ItemPrice = 8, Currency = Currency.Euro },
        //    UnitType = UnitType.PerItem
        //};

        //Product p3 = new Product(3) { Name = "StrawBerry", Description = "LoremIspum", Price = new Price()
        //{ ItemPrice = 3, Currency = Currency.Euro }, UnitType = UnitType.PerBox };


        PrintWelcome();

        Utilities.InitializeStock();

        Utilities.ShowMainMenu();

        Console.WriteLine("Application shutting down...");

        Console.ReadLine();
    }

    private static void PrintWelcome()
    {

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
    ()()()()()()   ____       _   _                       _       _____ _         _____ _                                        
    |\         |  |  _ \     | | | |                     ( )     |  __ (_)       / ____| |                                         ()()()()()()
    |.\. . . . |  | |_) | ___| |_| |__   __ _ _ __  _   _|/ ___  | |__) |  ___  | (___ | |__   ___  _ __                           |\         |
    \'.\       |  |  _ < / _ \ __| '_ \ / _` | '_ \| | | | / __| |  ___/ |/ _ \  \___ \| '_ \ / _ \| '_ \                          |.\. . . . |
     \.:\ . . .|  | |_) |  __/ |_| | | | (_| | | | | |_| | \__ \ | |   | |  __/  ____) | | | | (_) | |_) |                         \'.\       |
      \'o\     |  |____/ \___|\__|_| |_|\__,_|_| |_|\__, | |___/ |_|__ |_|\___| |_____/|_| |_|\___/| .__/                    _      \.:\ . . .|
       \.'\. . |  |_   _|                    | |     __/ |         |  \/  |                        | |                      | |      \'o\     |
        \'.\   |    | |  _ ____   _____ _ __ | |_ __|___/__ _   _  | \  / | __ _ _ __   __ _  __ _ |_|_ _ __ ___   ___ _ __ | |_      \.'\. . |
         \'`\ .|    | | | '_ \ \ / / _ \ '_ \| __/ _ \| '__| | | | | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '_ ` _ \ / _ \ '_ \| __|      \'.\   |
          \.'\ |   _| |_| | | \ V /  __/ | | | || (_) | |  | |_| | | |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_        \'`\ .|
           \__\|  |_____|_| |_|\_/ \___|_| |_|\__\___/|_|   \__, | |_|  |_|\__,_|_| |_|\__,_|\__, |\___|_| |_| |_|\___|_| |_|\__|        \.'\ |
                                                             __/ |                            __/ |                                       \__\|
                                                            |___/                            |___/                               
    ");

        Console.ResetColor();

        Console.WriteLine("Press enter key to start logging in!");

        //accepting enter here
        Console.ReadLine();

        Console.Clear();
    }
}