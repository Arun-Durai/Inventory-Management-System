﻿using InventoryManagement.Domain.General;
using InventoryManagement.Domain.OrderManagement;
using InventoryManagement.Domain.ProductManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Utilities
{
    private static List<Product> inventory = new List<Product>();
    private static List<Order> orders = new List<Order>();

    internal static void InitializeStock()//Mock implementation
    {
        Product p1 = new Product(1, "Sugar", "Lorem ipsum", new Price() { ItemPrice = 10, Currency = Currency.Euro }, UnitType.PerKg, 100);
        Product p2 = new Product(2, "Cake decorations", "Lorem ipsum", new Price() { ItemPrice = 8, Currency = Currency.Euro }, UnitType.PerItem, 20);
        Product p3 = new Product(3, "Strawberry", "Lorem ipsum", new Price() { ItemPrice = 3, Currency = Currency.Euro }, UnitType.PerBox, 10);
        inventory.Add(p1);
        inventory.Add(p2);
        inventory.Add(p3);
    }

    internal static void ShowMainMenu()
    {
        Console.ResetColor();
        Console.Clear();
        Console.WriteLine("********************");
        Console.WriteLine("* Select an action *");
        Console.WriteLine("********************");

        Console.WriteLine("1: Inventory management");
        Console.WriteLine("2: Order management");
        Console.WriteLine("3: Settings");
        Console.WriteLine("4: Save all data");
        Console.WriteLine("0: Close application");

        Console.Write("Your selection: ");

        string userSelection = Console.ReadLine();
        switch (userSelection)
        {
            case "1":
                ShowInventoryManagementMenu();
                break;
            case "2":
                ShowOrderManagementMenu();
                break;
            case "3":
                ShowSettingsMenu();
                break;
            case "4":
                //SaveAllData();
                break;
            case "0":
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
    }

    private static void ShowInventoryManagementMenu()
    {
        string userSelection;

        do
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Inventory management *");
            Console.WriteLine("************************");

            ShowAllProductsOverview();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("What do you want to do?");
            Console.ResetColor();

            Console.WriteLine("1: View details of product");
            Console.WriteLine("2: Add new product");
            Console.WriteLine("3: Clone product");
            Console.WriteLine("4: View products with low stock");
            Console.WriteLine("0: Back to main menu");

            Console.Write("Your selection: ");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ShowDetailsAndUseProduct();
                    break;

                case "2":
                    //ShowCreateNewProduct();
                    break;

                case "3":
                    //ShowCloneExistingProduct();
                    break;

                case "4":
                    ShowProductsLowOnStock();
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "0");
        ShowMainMenu();
    }


    private static void ShowAllProductsOverview()
    {
        foreach (var product in inventory)
        {
            Console.WriteLine(product.DisplayDetailsShort());
            Console.WriteLine();
        }
    }

    private static void ShowDetailsAndUseProduct()
    {
        string userSelection = string.Empty;

        Console.Write("Enter the ID of product: ");
        string selectedProductId = Console.ReadLine();

        if (selectedProductId != null)
        {
            Product selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();

            if (selectedProduct != null)
            {

                Console.WriteLine(selectedProduct.DisplayDetailsFull());

                Console.WriteLine("\nWhat do you want to do?");
                Console.WriteLine("1: Use product");
                Console.WriteLine("0: Back to inventory overview");

                Console.Write("Your selection: ");
                userSelection = Console.ReadLine();

                if (userSelection == "1")
                {
                    Console.WriteLine("How many products do you want to use?");
                    int amount = int.Parse(Console.ReadLine() ?? "0");

                    selectedProduct.UseProduct(amount);

                    Console.ReadLine();
                }
            }
        }
        else
        {
            Console.WriteLine("Non-existing product selected. Please try again.");
        }
    }

    private static void ShowProductsLowOnStock()
    {
        List<Product> lowOnStockProducts = inventory.Where(p => p.IsBelowStockThreshold).ToList();

        if (lowOnStockProducts.Count > 0)
        {
            Console.WriteLine("The following items are low on stock, order more soon!");

            foreach (var product in lowOnStockProducts)
            {
                Console.WriteLine(product.DisplayDetailsShort());
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No items are currently low on stock!");
        }

        Console.ReadLine();
    }

    private static void ShowOrderManagementMenu()
    {
        string userSelection = string.Empty;

        do
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");

            Console.WriteLine("1: Open order overview");
            Console.WriteLine("2: Add new order");
            Console.WriteLine("0: Back to main menu");

            Console.Write("Your selection: ");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ShowOpenOrderOverview();
                    break;
                case "2":
                    ShowAddNewOrder();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "0");
        ShowMainMenu();
    }

    private static void ShowOpenOrderOverview()
    {
        //check to handle fulfilled orders
        ShowFulfilledOrders();

        if (orders.Count > 0)
        {
            Console.WriteLine("Open orders:");

            foreach (var order in orders)
            {
                Console.WriteLine(order.ShowOrderDetails());
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("There are no open orders");
        }

        Console.ReadLine();
    }

    private static void ShowFulfilledOrders()
    {
        Console.WriteLine("Checking fulfilled orders.");
        foreach (var order in orders)
        {
            if (!order.Fulfilled && order.OrderFulfilmentDate < DateTime.Now) // fulfil the order
            {
                foreach (var orderItem in order.OrderItems)
                {
                    Product selectedProduct = inventory.Where(p => p.Id == orderItem.ProductId).FirstOrDefault();
                    if (selectedProduct != null)
                        selectedProduct.IncreaseStock(orderItem.AmountOrdered);
                }
                order.Fulfilled = true;
            }
        }

        orders.RemoveAll(o => o.Fulfilled);

        Console.WriteLine("Fulfilled orders checked");
    }

    private static void ShowAddNewOrder()
    {
        Order newOrder = new Order();
        string selectedProductId = string.Empty;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Creating new order");
        Console.ResetColor();

        do
        {
            ShowAllProductsOverview();

            Console.WriteLine("Which product do you want to order? (enter 0 to stop adding new products to the order)");

            Console.Write("Enter the ID of product: ");
            selectedProductId = Console.ReadLine();

            if (selectedProductId != "0")
            {
                Product selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();

                if (selectedProduct != null)
                {
                    Console.Write("How many do you want to order?: ");
                    int amountOrdered = int.Parse(Console.ReadLine() ?? "0");

                    OrderItem orderItem = new OrderItem
                    {
                        ProductId = selectedProduct.Id,
                        ProductName = selectedProduct.Name,
                        AmountOrdered = amountOrdered
                    };

                    //OrderItem orderItem = new OrderItem();
                    //orderItem.ProductId = selectedProduct.Id;
                    //orderItem.ProductName = selectedProduct.Name;
                    //orderItem.AmountOrdered = amountOrdered;

                    newOrder.OrderItems.Add(orderItem);
                }
            }

        } while (selectedProductId != "0");

        Console.WriteLine("Creating order...");

        orders.Add(newOrder);

        Console.WriteLine("Order now created.");
        Console.ReadLine();
    }

    private static void ShowSettingsMenu()
    {
        string userSelection;

        do
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("************");
            Console.WriteLine("* Settings *");
            Console.WriteLine("************");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("What do you want to do?");
            Console.ResetColor();

            Console.WriteLine("1: Change stock threshold");
            Console.WriteLine("0: Back to main menu");

            Console.Write("Your selection: ");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ShowChangeStockThreshold();
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "0");
        ShowMainMenu();
    }

    private static void ShowChangeStockThreshold()
    {
        Console.WriteLine($"Enter the new stock threshold (current value: {Product.StockThreshold}). This applies to all products!");
        Console.Write("New value: ");
        int newValue = int.Parse(Console.ReadLine() ?? "0");
        Product.StockThreshold = newValue;
        Console.WriteLine($"New stock threshold set to {Product.StockThreshold}");

        foreach (var product in inventory)
        {
            product.UpdateLowStock();
        }

        Console.ReadLine();
    }
}
