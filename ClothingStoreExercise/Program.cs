using System;
using System.Collections.Generic;

namespace ClothingStoreExercise
{
    class Program
    {
        public static MenuState state = MenuState.Menu;
        public static List<Garment> store = new List<Garment>();
        public static List<Garment> shoppingCart = new List<Garment>();
        public static int index = 0;
        static void Main(string[] args)
        {
            while (state != MenuState.Exit)
            {
                switch (state)
                {
                    case MenuState.Menu:
                        MainMenu();
                        break;
                    case MenuState.Admin:
                        AdminMethod();
                        break;
                    case MenuState.Shop:
                        if (store.Count < 5)
                        {
                            Console.WriteLine("We are currently updating our available products. Come back soon!");
                            Console.ReadKey(true);
                            state = MenuState.Menu;
                        }
                        else
                            ShopMethod();
                        break;
                    default:
                        state = MenuState.Exit;
                        break;
                }
            }
        }
        static int TryCatchInt()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    int returnValue = Convert.ToInt32(input);
                    if (returnValue < 0)
                        returnValue = 0;
                    return returnValue;
                }
                catch //(Exception e)
                {
                    //Console.WriteLine($"Exeption: {e}\n\n TRY AGAIN.");
                    Console.WriteLine("Wrong input. Try again:");
                }
            }
        }
        static void ShopMethod()
        {
            
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                
                Console.WriteLine("*OUR PRODUCTS*\nUse [A] and [Z] to see our products.\nPress [ENTER] to put product in shoppingcart" +
                                  "\nPress [C] for checkout and shoppingcart overview\n\n[Q] Quit to main menu");
                Console.Write($"\n{index + 1} " +
                              $"Product: {Enum.GetName(typeof(GarmentType), store[index].GarmentType)}, " +
                              $"Size: {Enum.GetName(typeof(Size), store[index].Size)}, " +
                              $"Color: {Enum.GetName(typeof(Color), store[index].Color)}, " +
                              $"Price: {store[index].Price}");
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A:
                        index++;
                        if (index == store.Count)
                            index = 0;
                        continue;

                    case ConsoleKey.Z:
                        index--;
                        if (index < 0)
                            index = store.Count - 1;
                        continue;

                    case ConsoleKey.Enter:
                        shoppingCart.Add(store[index]);
                        Console.WriteLine($"\n{Enum.GetName(typeof(GarmentType), store[index].GarmentType)} added to shoppingcart!");                                    
                        break;

                    case ConsoleKey.C:
                        if (shoppingCart.Count == 0)
                            Console.WriteLine("\nNo produtcs added to cart yet!");
                        else
                        {
                            int totalAmount = 0;
                            foreach (Garment item in shoppingCart)
                            {
                                Console.Write($"\n{Enum.GetName(typeof(GarmentType), item.GarmentType)}, {item.Price}");
                                totalAmount += item.Price;
                            }
                            Console.WriteLine($"\nTotal cost: {totalAmount} SEK");
                        }
                        break;

                    case ConsoleKey.Q:
                        state = MenuState.Menu;
                        Console.WriteLine("\nReturning to mainmenu");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\nWrong input, try again.");
                        Console.ReadKey(true);
                        continue;
                }
                Console.ReadKey(true);
            }
        }
        static void AdminMethod()
        {
            Console.Clear();
            Console.WriteLine("Register new product for sale\n");
            bool isRunning = true;
            while (isRunning)
            {
                Garment garment = default;
                Console.WriteLine("Enter type of garment: (use numbers on display to select)\n");
                int numberIndexOne = 0;
                foreach (string garmentName in Enum.GetNames(typeof(GarmentType)))
                {
                    Console.Write($"[{numberIndexOne}] {garmentName}\n");
                    numberIndexOne++;
                }
                int garmentType = TryCatchInt();
                Console.Clear();
                Console.WriteLine($"{Enum.GetName(typeof(GarmentType), garmentType)} selected.");
                garment.GarmentType = garmentType;

                Console.WriteLine("Enter size: (use numbers on display to select)\n");
                int numberIndexTwo = 0;
                foreach (string garmentSize in Enum.GetNames(typeof(Size)))
                {
                    Console.Write($"[{numberIndexTwo}] {garmentSize}\n");
                    numberIndexTwo++;
                }
                int size = TryCatchInt();
                Console.Clear();
                Console.WriteLine($"{Enum.GetName(typeof(Size), size)} selected.");
                garment.Size = size;

                Console.WriteLine("Enter color: (use numbers on display to select)\n");
                int numberIndexThree = 0;
                foreach (string garmentColor in Enum.GetNames(typeof(Color)))
                {
                    Console.Write($"[{numberIndexThree}] {garmentColor}\n");
                    numberIndexThree++;
                }
                int color = TryCatchInt();
                Console.Clear();
                Console.WriteLine($"{Enum.GetName(typeof(Color), color)} selected.");
                garment.Color = color;

                Console.WriteLine("Enter garment price (SEK):");
                int price = TryCatchInt();
                Console.Clear();
                Console.WriteLine($"{price} SEK entered.");
                garment.Price = price;

                store.Add(garment);
                Console.WriteLine("\nRegistration completed. The garment is now for sale. Press any key to continue.");
                Console.ReadKey(true);
                isRunning = false;
            }
            Console.Clear();
            state = MenuState.Menu;
        }
        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("*EXERCISE: CLOTHING STORE*\n\n[1]Main menu\n[2]Admin(register)\n[3]Shop\n[4]Exit");
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    state = MenuState.Menu;
                    break;
                case ConsoleKey.D2:
                    state = MenuState.Admin;
                    break;
                case ConsoleKey.D3:
                    state = MenuState.Shop;
                    break;
                case ConsoleKey.D4:
                    state = MenuState.Exit;
                    break;
                default:
                    Console.WriteLine("Wrong key pressed. Try again.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
    enum GarmentType
    {
        Sweater,
        Shirt,
        Tie,
        Socks,
        Underwear,
        Shoes,
        Coat
    };
    enum Size
    {
        XS,
        S,
        M,
        L,
        XL,
        XXL
    };
    enum Color
    {
        Black,
        White,
        Red,
        Blue,
        Brown,
        Yellow
    };
    enum MenuState
    {
        Menu,
        Admin,
        Shop,
        Exit
    };
}
