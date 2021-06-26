using ShoppingAppBL;
using ShoppingAppCommon;
using System;
using System.Reflection;

namespace Quiz1_Shopping_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Online Shopping Application";

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        static void ShowProducts()
        {
            var productList = ShoppingAppService.GetAllProductsData();
            Console.WriteLine("Product ID\t\tName");
            foreach (var product in productList)
            {
                Console.WriteLine($"{product.ProductId}\t\t{product.Name}");
            }
        }

        static void ShowProductInfo(int productId)
        {
            var product = ShoppingAppService.GetProductData(productId);

            foreach (PropertyInfo prop in product.GetType().GetProperties())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{prop.Name}: ");
                Console.ResetColor();
                Console.Write($"{prop.GetValue(product, null)}\n");
            }
        }

        static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("Welcome to Shopping App");
            Console.WriteLine("Please Type:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("- \'q\' to quit");
            Console.WriteLine("- \'s\' to show products");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Input: ");
            Console.ResetColor();

            switch (Console.ReadLine().ToLower())
            {
                case "q":
                    return false;
                case "s":
                    bool showSecondMenu = true;
                    while (showSecondMenu)
                    {
                        showSecondMenu = ShowSecondMenu();
                    }
                    return true;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid option");
                    Console.ResetColor();
                    Console.ReadLine();
                    return true;
            }
        }

        static bool ShowSecondMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu -> Product Select");
            Console.WriteLine("Please select a product");
            ShowProducts();
            Console.WriteLine("Please Type:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("- \'q\' to go back");
            Console.WriteLine("- the product id to show product information");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Input: ");
            Console.ResetColor();

            switch (Console.ReadLine().ToLower())
            {
                case "q":
                    return false;
                case "1":
                    {
                        bool showThirdMenu = true;
                        while (showThirdMenu)
                        {
                            showThirdMenu = ShowThirdMenu(1);
                        }
                        return true;
                    }
                case "2":
                    {
                        bool showThirdMenu = true;
                        while (showThirdMenu)
                        {
                            showThirdMenu = ShowThirdMenu(2);
                        }
                        return true;
                    }
                case "3":
                    {
                        bool showThirdMenu = true;
                        while (showThirdMenu)
                        {
                            showThirdMenu = ShowThirdMenu(3);
                        }
                        return true;
                    }
                case "4":
                    {
                        bool showThirdMenu = true;
                        while (showThirdMenu)
                        {
                            showThirdMenu = ShowThirdMenu(4);
                        }
                        return true;
                    }
                case "5":
                    {
                        bool showThirdMenu = true;
                        while (showThirdMenu)
                        {
                            showThirdMenu = ShowThirdMenu(5);
                        }
                        return true;
                    }
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid option");
                    Console.ResetColor();
                    Console.ReadLine();
                    return true;
            }
        }

        static bool ShowThirdMenu(int productId)
        {
            Console.Clear();
            Console.WriteLine("Main Menu -> Product Select -> Product Info");
            Console.WriteLine("Product Information");
            ShowProductInfo(productId);
            Console.WriteLine("Please Type:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("- \'q\' to go back");
            Console.WriteLine("- \'b\' to buy");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Input: ");
            Console.ResetColor();

            switch (Console.ReadLine().ToLower())
            {
                case "q":
                    return false;
                case "b":
                    {
                        int totalPrice;

                        Console.Write("Enter amount to buy: ");
                        var userInput = Console.ReadLine();
                        if (int.TryParse(userInput, out int buyingQuantity))
                        {
                            totalPrice = ShoppingAppService.GetTotalPrice(buyingQuantity, productId);

                            if (totalPrice == -2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You entered more than the stock amount. Please try again.");
                                Console.ResetColor();
                                Console.ReadLine();
                            }
                            else if (totalPrice == -3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Sorry, there are no more stocks of this item.");
                                Console.ResetColor();
                                Console.ReadLine();
                            }
                            else if (totalPrice == -4)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please enter a valid quantity.");
                                Console.ResetColor();
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("The total price is " + totalPrice);
                                Console.WriteLine("Proceed to buy? Enter \'Y\' for yes or \'N\' for no");
                                switch (Console.ReadLine().ToLower())
                                {
                                    case "y":
                                        string result = ShoppingAppService.BuyProduct(buyingQuantity, productId);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine(result);
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        return true;
                                    case "n":
                                        return true;
                                }
                            }
                        } 
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter a valid option");
                            Console.ResetColor();
                            Console.ReadLine();
                        }
                        
                        return true;
                    }
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid option");
                    Console.ResetColor();
                    Console.ReadLine();
                    return true;
            }
        }
    }
}
