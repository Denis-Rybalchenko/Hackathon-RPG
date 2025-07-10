using System;
using System.Collections.Generic;

class Program
{
    static List<string> Menu = new List<string> { "Movement", "Stats", "Inventory", "Exit game" }; //Listy
    static List<string> MovementMenu = new List<string> { "Up", "Down", "Left", "Right", "-" };
    static List<int> Stats = new List<int> {20, 1, 1, 0, 1};

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
            switch (actionChoice)
            {
                case 1:
                    MovementMenuBoot();
                    break;
                case 2:
                    ShowStats();
                    MenuBoot();
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

    string[] statNames = { "HP", "ATK", "DEF", "EXP", "LV" };
    for (int i = 0; i < Stats.Count; i++)
    {
        Console.WriteLine($"{statNames[i]}: {Stats[i]}");
    }
    Console.WriteLine();
}


}