using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
            FileCheck();
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
            UpdateFile();
        }
        static int TryCatchInt(int maxValue)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    int returnValue = Convert.ToInt32(input);
                    if (returnValue < 0)
                        returnValue = 0;
                    if (returnValue > maxValue)
                    {
                        Console.WriteLine("Wrong input. Try again:");
                        continue;
                    }
                        
                    return returnValue;
                }
                catch //(Exception e)
                {
                    //Console.WriteLine($"Exeption: {e}\n\n TRY AGAIN.");
                    Console.WriteLine("Wrong input. Try again:");
                }
            }
        }
        private static void ShopMethod()
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
                            Console.WriteLine("\nNo products added to cart yet!");
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
        private static void AdminMethod()
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
                int garmentType = TryCatchInt(6);
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
                int size = TryCatchInt(5);
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
                int color = TryCatchInt(5);
                Console.Clear();
                Console.WriteLine($"{Enum.GetName(typeof(Color), color)} selected.");
                garment.Color = color;

                Console.WriteLine("Enter garment price (SEK):");
                int price = TryCatchInt(99999);
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
        private static void MainMenu()
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
        private static void FileCheck()
        {
            if (!File.Exists("clothes.txt"))
            {
                File.Create("clothes.txt").Close();
            }
            else
            {
                string[] readLines = File.ReadAllLines("clothes.txt");
                foreach (string lines in readLines)
                {

                    Garment garment = default;
                    string[] splitLinesOne = lines.Split(',');
                    string[] splitElementGarment = splitLinesOne[0].Split(':');
                    string[] splitElementSize = splitLinesOne[1].Split(':');
                    string[] splitElementColor = splitLinesOne[2].Split(':');
                    
                    garment.GarmentType = Convert.ToInt32(splitElementGarment[0]);
                    garment.Size = Convert.ToInt32(splitElementSize[0]);
                    garment.Color = Convert.ToInt32(splitElementColor[0]);
                    garment.Price = Convert.ToInt32(splitLinesOne[3]);
                    store.Add(garment);
                }
            }
        }
        private static void UpdateFile() //writes to file.
        {
            using (StreamWriter sw = new StreamWriter("clothes.txt", false)) //append set to false to avoid repetetive writing.
            {
                foreach (Garment stuff in store)
                {
                    sw.WriteLine($"{stuff.GarmentType}:{Enum.GetName(typeof(GarmentType),stuff.GarmentType)}," +
                                 $"{stuff.Size}:{Enum.GetName(typeof(Size), stuff.Size)}," +
                                 $"{stuff.Color}:{Enum.GetName(typeof(Color), stuff.Color)}," +
                                 $"{stuff.Price}");
                }
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
