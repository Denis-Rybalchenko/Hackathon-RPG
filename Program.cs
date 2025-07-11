using System;
using System.Collections.Generic;
using System.Data;

class Program
{
    // tady jsou ruzne proměnné ktere se používají v programu
    static int xPlayerCoordinate = 0;
    static int yPlayerCoordinate = 0;
    static int[] coordinatesP = { xPlayerCoordinate, yPlayerCoordinate }; //souřadnice hráče
    static List<string> Menu = new List<string> { "Movement", "Stats", "Inventory", "Exit game" }; //Listy
    static List<string> MovementMenu = new List<string> { "Up", "Down", "Left", "Right", "-" };
    static List<string> statNames = new List<string> { "HP", "ATK", "DEF", "EXP", "LV" }; //staty hráče
    static List<int> Stats = new List<int> { 20, 1, 1, 0, 1 };
    static List<string> DiscoveredRooms = new List<string>($"(0; 0)"); //seznam objevených místností



    static void Main(string[] args) //zahajeni
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Command Line RPG.");
        Console.WriteLine();

        MenuBoot();
    }

    static void MenuBoot() //menu metoda
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
                    return; //ukončení hry
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

        Console.WriteLine($"Current coordinates: ({coordinatesP[0]}, {coordinatesP[1]})");
        Console.WriteLine();

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

            NewDiscoveredRoomCheck(xPlayerCoordinate, yPlayerCoordinate); // Check if a new room is discovered at the new coordinates

            MovementMenuBoot(); // Return to movement menu after moving
        }
        else
        {
            Console.WriteLine("Invalid input, try again");
            MovementMenuBoot();
            return;
        }
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







static void NewDiscoveredRoomCheck(int xCoordinate, int yCoordinate)
    {
        string newRoom = $"({xCoordinate}, {yCoordinate})";
        if (!DiscoveredRooms.Contains(newRoom))
        {
            Console.WriteLine($"You are already in the room at coordinates: {newRoom}");

        }
        else
        {            // If the room is not already discovered, add it to the list
            DiscoveredRooms.Add(newRoom);
            Console.WriteLine($"New room discovered at coordinates: {newRoom}");
        }
    }
}

//static void BattleBoot() //battle metode
//    {
//        string[] options = { "rock", "paper", "scissors" };
//        Random random = new Random();
//
//        Console.WriteLine("An enemy has appeared!");
//        Console.Write("What will you choose (rock/paper/scissors)? ");
//        string userChoice = Console.ReadLine().ToLower();
//
//        // Ověření vstupu
//        if (!Array.Exists(options, option => option == userChoice))
//        {
//            Console.WriteLine("Invalid choice.");
//            return;
//        }
//
//        string enemyChoice = options[random.Next(options.Length)];
//        Console.WriteLine($"Enemy chose: {enemyChoice}");
//
//        // Určení výsledku
//        if (userChoice == enemyChoice)
//        {
//            Console.WriteLine("Tie!");
//        }
//        else if (
//            (userChoice == "rock" && enemyChoice == "scissors")
//            (userChoice == "paper" && enemyChoice == "rock")
//            (userChoice == "scissors" && enemyChoice == "paper")
//
//        )
//        {
//            Console.WriteLine("You won! You gain 10 EXP.");
//            Stats[3] += 10; // zisk zkušeností při výhře
//        }
//        else
//        {
//            Console.WriteLine("You lost!");
//            Stats[0] -= 5 / Stats[2]; // ztráta zdraví při prohře
//        }
//        Console.WriteLine();
//        MenuBoot();
//    }