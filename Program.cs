using System;
using System.Collections.Generic;

class Program
{
    // Proměnné
    static int xPlayerCoordinate = 0;
    static int yPlayerCoordinate = 0;
    static int[] coordinatesP = { xPlayerCoordinate, yPlayerCoordinate };
    static List<string> Menu = new List<string> { "Movement", "Stats", "Inventory", "Exit game" };
    static List<string> MovementMenu = new List<string> { "Up", "Down", "Left", "Right", "-" };
    static List<string> statNames = new List<string> { "HP", "ATK", "DEF", "EXP", "LV" };
    static List<int> Stats = new List<int> { 20, 1, 1, 0, 1 };
    static List<string> DiscoveredRooms = new List<string> { "(0; 0)" };
    static bool exitGame = false;

    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Command Line RPG.");
        Console.WriteLine();

        MenuBoot();
    }

    static void MenuBoot()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine();
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
                    break;
                case 3:
                    Console.WriteLine("Inventory is currently empty.");
                    Console.WriteLine();
                    MenuBoot();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("Exiting game...");
                    Console.WriteLine("Thank you for playing!");
                    bool exitGame = true;
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice. Try again.");
                    MenuBoot();
                    return;
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
        coordinatesP[0] = xPlayerCoordinate;
        coordinatesP[1] = yPlayerCoordinate;

        Console.WriteLine();
        Console.WriteLine("Movement Menu:");
        Console.WriteLine($"Current coordinates: ({coordinatesP[0]}; {coordinatesP[1]})");
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
                    yPlayerCoordinate++;
                    break;
                case "Down":
                    yPlayerCoordinate--;
                    break;
                case "Left":
                    xPlayerCoordinate--;
                    break;
                case "Right":
                    xPlayerCoordinate++;
                    break;
                case "-":
                    Console.WriteLine("Returning to main menu");
                    Console.WriteLine();
                    MenuBoot();
                    return;
                default:
                    Console.WriteLine("Invalid movement choice.");
                    MovementMenuBoot();
                    break;
            }

            Console.WriteLine("You moved successfully.");
            Console.WriteLine($"New coordinates: ({xPlayerCoordinate}, {yPlayerCoordinate})");
            Console.WriteLine();

            NewDiscoveredRoomCheck(xPlayerCoordinate, yPlayerCoordinate);

            
        }
        else
        {
            Console.WriteLine("Invalid input, try again");
            MovementMenuBoot();
            return;
        }
    }

    static void ShowStats()
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

    static void NewDiscoveredRoomCheck(int xCoordinate, int yCoordinate)
    {
        string newRoom = $"({xCoordinate}; {yCoordinate})";
        if (!DiscoveredRooms.Contains(newRoom))
        {
            DiscoveredRooms.Add(newRoom);
            Console.WriteLine($"New room discovered at coordinates: {newRoom}");
            BattleBoot(); // Battle occurs in new room
        }
        else if (exitGame)
        {
            return; // Exit game if exitGame is true
        }
        else
        {
            Console.WriteLine($"You were already in the room at coordinates: {newRoom}");
            MovementMenuBoot();
        }
    }

    static void BattleBoot()
    {
        string[] options = { "rock", "paper", "scissors" };
        Random random = new Random();

        Console.WriteLine("An enemy has appeared!");
        Console.Write("What will you choose (rock/paper/scissors)? ");
        string userChoice = Console.ReadLine().ToLower();

        if (!Array.Exists(options, option => option == userChoice))
        {
            Console.WriteLine("Invalid choice.");
            BattleBoot();
        }

        string enemyChoice = options[random.Next(options.Length)];
        Console.WriteLine($"Enemy chose: {enemyChoice}");

        if (userChoice == enemyChoice)
        {
            Console.WriteLine("Tie!");
        }
        else if (
            (userChoice == "rock" && enemyChoice == "scissors") ||
            (userChoice == "paper" && enemyChoice == "rock") ||
            (userChoice == "scissors" && enemyChoice == "paper")
        )
        {
            Console.WriteLine("You won! You gain 10 EXP.");
            Stats[3] += 10;
            LevelUp();
        }
        else
        {
            Console.WriteLine("You lost!");
            int damage = 5 / Math.Max(Stats[2], 1); // Avoid division by zero
            Stats[0] -= damage;
            Console.WriteLine($"You took {damage} damage.");
        }

        Console.WriteLine();
        MenuBoot();
    }

    static void LevelUp()
    {
        if (Stats[3] >= 100)
        {
            Stats[4]++;
            Stats[3] = 0;
            Stats[1] += 2;
            Stats[2] += 2;
            Console.WriteLine("Congratulations! You leveled up!");
            Console.WriteLine($"New Level: {Stats[4]}, ATK: {Stats[1]}, DEF: {Stats[2]}");
        }
    }
}