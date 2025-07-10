using System;
using System.Collections.Generic;

class Program
{
    static int xCoordinate = 0;
    static int yCoordinate = 0;
    static int[] coordinates = { xCoordinate, yCoordinate };//souřadnice hráče
    static List<string> Menu = new List<string> { "Movement", "Stats", "Inventory", "Exit game" }; //Listy
    static List<string> MovementMenu = new List<string> { "Up", "Down", "Left", "Right", "-" };
    static List<string> statNames = new List<string> { "HP", "ATK", "DEF", "EXP", "LV" }; //staty hráče
    static List<int> Stats = new List<int> { 20, 1, 1, 0, 1 };

    

    static void Main(string[] args) //zahajeni
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Command Line RPG.");
        Console.WriteLine();

        MenuBoot();
    }

    static void MenuBoot() //menu metoda
    {
        int i1 = 1;
        foreach (string action in Menu)
        {
            Console.WriteLine("[" + i1 + "] " + action);
            i1++;
        }

        Console.WriteLine();
        string UserChoice = Console.ReadLine();

        bool test = int.TryParse(UserChoice, out int actionChoice);

        if (test)
        {
            switch (actionChoice) //vyber hrace v menu
            {
                case 1:
                    MovementMenuBoot();
                    break;
                case 2:
                    ShowStats();
                    break;
                case 3:
                    Console.WriteLine("Inventory is currently empty.");
                    Console.WriteLine();
                    MenuBoot();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("Exiting game...");
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice. Try again.");
                    MenuBoot();
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input, try again");
            MenuBoot();
        }
    }

static void MovementMenuBoot()
{
    Console.WriteLine();
    int i2 = 1;
    foreach (string Movement in MovementMenu)
    {
        Console.WriteLine("[" + i2 + "] " + Movement);
        i2++;
    }
    string x = Console.ReadLine();
    bool ok = int.TryParse(x, out int xcislo);

        if (ok && xcislo >= 1 && xcislo <= MovementMenu.Count)
        {
            Console.WriteLine();
            Console.WriteLine($"You chose to move: {MovementMenu[xcislo - 1]}");
            Console.WriteLine();

            switch (MovementMenu[xcislo - 1])
            {
                case "Up":
                    yCoordinate++;
                    break;
                case "Down":
                    yCoordinate--;
                    break;
                case "Left":
                    xCoordinate--;
                    break;
                case "Right":
                    xCoordinate++;
                    break;
                case "-":
                    Console.WriteLine("Returning to main menu...");
                    MenuBoot();
                    return;
                default:
                    Console.WriteLine("Invalid movement choice.");
                    MovementMenuBoot();
                    return;
            }

            coordinates[0] = xCoordinate;
            coordinates[1] = yCoordinate;

            Console.WriteLine($"Current coordinates: ({coordinates[0]}, {coordinates[1]})");
            Console.WriteLine();

            // Here you can add logic to handle encounters or events based on the new coordinates
            // For example, you could check if the player has reached a specific coordinate and trigger an event.
            Console.WriteLine("Returning to main menu...");
            MenuBoot();
    }
        else
        {
            Console.WriteLine("Invalid input, try again");
            MovementMenuBoot();
            return;
        }

    MenuBoot();
}

static void ShowStats() //show stats metoda
{
    Console.WriteLine();

    Console.WriteLine("Player Stats:");
        for (int i = 0; i < Stats.Count; i++)
        {
            Console.WriteLine($"{statNames[i]}: {Stats[i]}");
        }
    Console.WriteLine();
    MenuBoot();
}


}