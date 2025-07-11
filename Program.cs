using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Player position
    static int xPlayerCoordinate = 0;
    static int yPlayerCoordinate = 0;
    static int[] coordinatesP = { xPlayerCoordinate, yPlayerCoordinate };

    // Menus and stats
    static List<string> Menu = new List<string> { "Movement", "Stats", "Inventory", "Exit game" };
    static List<string> MovementMenu = new List<string> { "Up", "Down", "Left", "Right", "-" };
    static List<string> statNames = new List<string> { "HP", "ATK", "DEF", "EXP", "LV" };
    static List<int> Stats = new List<int> { 20, 1, 1, 0, 1 }; // HP, ATK, DEF, EXP, LV
    static List<string> DiscoveredRooms = new List<string> { "(0; 0)" };

    // Inventory
    static int FoodCount = 2;
    static int PotionCount = 1;

    // Move usage limits
    static int[] playerMoveUsage;
    static int[] enemyMoveUsage;
    const int MaxMoveUsage = 3;

    // Random generator
    static Random rng = new Random();

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
        for (int i = 0; i < Menu.Count; i++)
            Console.WriteLine($"[{i+1}] {Menu[i]}");

        Console.WriteLine();
        string choice = Console.ReadLine();
        if (int.TryParse(choice, out int action) && action >= 1 && action <= Menu.Count)
        {
            switch (action)
            {
                case 1:
                    MovementMenuBoot();
                    break;
                case 2:
                    ShowStats();
                    break;
                case 3:
                    InventoryMenu();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("Exiting game...");
                    Environment.Exit(0);
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input, try again.");
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
        for (int i = 0; i < MovementMenu.Count; i++)
            Console.WriteLine($"[{i+1}] {MovementMenu[i]}");

        string input = Console.ReadLine();
        if (int.TryParse(input, out int idx) && idx >= 1 && idx <= MovementMenu.Count)
        {
            string move = MovementMenu[idx - 1];
            Console.WriteLine($"You chose to move: {move}");

            switch (move)
            {
                case "Up":    yPlayerCoordinate++; break;
                case "Down":  yPlayerCoordinate--; break;
                case "Left":  xPlayerCoordinate--; break;
                case "Right": xPlayerCoordinate++; break;
                case "-":
                    Console.WriteLine("Returning to main menu");
                    Console.WriteLine();
                    MenuBoot();
                    return;
            }

            Console.WriteLine($"You moved successfully. New coordinates: ({xPlayerCoordinate}, {yPlayerCoordinate})");
            Console.WriteLine();
            NewDiscoveredRoomCheck(xPlayerCoordinate, yPlayerCoordinate);
            MovementMenuBoot();
        }
        else
        {
            Console.WriteLine("Invalid input, try again.");
            MovementMenuBoot();
        }
    }

    static void ShowStats()
    {
        Console.WriteLine();
        Console.WriteLine("Player Stats:");
        for (int i = 0; i < Stats.Count; i++)
            Console.WriteLine($"{statNames[i]}: {Stats[i]}");
        Console.WriteLine();
        MenuBoot();
    }

    static void InventoryMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Inventory:");
        Console.WriteLine($"[1] Food ({FoodCount}) - Heals 10 HP");
        Console.WriteLine($"[2] Potion ({PotionCount}) - Heals to full HP");
        Console.WriteLine("[3] Return to Main Menu");
        Console.WriteLine();

        string input = Console.ReadLine();
        if (!int.TryParse(input, out int choice)) { Console.WriteLine("Invalid input."); InventoryMenu(); return; }

        switch (choice)
        {
            case 1:
                if (FoodCount > 0)
                {
                    Stats[0] += 10;
                    FoodCount--;
                    Console.WriteLine("You ate food and restored 10 HP.");
                }
                else Console.WriteLine("You have no food.");
                break;
            case 2:
                if (PotionCount > 0)
                {
                    Stats[0] = 20 + Stats[4] * 5;
                    PotionCount--;
                    Console.WriteLine("You used a potion and fully healed!");
                }
                else Console.WriteLine("You have no potions.");
                break;
            case 3:
                Console.WriteLine();
                MenuBoot();
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
        InventoryMenu();
    }

    static void NewDiscoveredRoomCheck(int x, int y)
    {
        string room = $"({x}; {y})";
        if (!DiscoveredRooms.Contains(room))
        {
            DiscoveredRooms.Add(room);
            Console.WriteLine($"New room discovered at coordinates: {room}");
            BattleBoot();
        }
        else
        {
            Console.WriteLine($"You are already in the room at coordinates: {room}");
        }
    }

    // Battle system
    static void BattleBoot()
    {
        // Initialize move usage counters for this battle
        playerMoveUsage = new int[3];
        enemyMoveUsage = new int[3];

        Enemy enemy = GenerateEnemy();
        Console.WriteLine($"An enemy {enemy.Name} appeared! [HP: {enemy.HP}, ATK: {enemy.ATK}, DEF: {enemy.DEF}]");

        while (enemy.HP > 0 && Stats[0] > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Choose your move:");
            Console.WriteLine("[1] Strike (Speed: 2, Damage: 10 + ATK)");
            Console.WriteLine("[2] Block  (Speed: 1, Damage: 2 + ATK/2, Blocks: 5 + DEF)");
            Console.WriteLine("[3] Dodge  (Speed: 3, Avoids damage if faster)");
            Console.WriteLine("[4] Open Inventory");
            Console.WriteLine();
            Console.WriteLine("You can use each move up to 3 times per round. Moves will recharge after all are used.");

            // Reset usages if all moves used up
            if (playerMoveUsage.All(u => u >= MaxMoveUsage))
            {
                Array.Clear(playerMoveUsage, 0, playerMoveUsage.Length);
                Console.WriteLine("Your moves have recharged!");
            }
            if (enemyMoveUsage.All(u => u >= MaxMoveUsage))
            {
                Array.Clear(enemyMoveUsage, 0, enemyMoveUsage.Length);
                Console.WriteLine("Enemy's moves have recharged!");
            }

            int pChoice;
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "4") // Inventory option
                {
                    InventoryMenu();
                    return; // Return to inventory menu
                }
                if (!int.TryParse(input, out pChoice) || pChoice < 1 || pChoice > 3)
                {
                    Console.WriteLine("Invalid move. Choose 1, 2, or 3.");
                    continue;
                }
                if (playerMoveUsage[pChoice - 1] >= MaxMoveUsage)
                {
                    Console.WriteLine("You cannot use that move anymore this round. Choose another.");
                    continue;
                }
                playerMoveUsage[pChoice - 1]++;
                break;
            }

            // Enemy choice respecting usage limits
            int eChoice;
            var available = Enumerable.Range(1, 3).Where(i => enemyMoveUsage[i - 1] < MaxMoveUsage).ToList();
            if (!available.Any())
            {
                Array.Clear(enemyMoveUsage, 0, enemyMoveUsage.Length);
                Console.WriteLine("Enemy's moves have recharged!");
                available = Enumerable.Range(1, 3).ToList();
            }
            eChoice = available[rng.Next(available.Count)];
            enemyMoveUsage[eChoice - 1]++;

            Move playerMove = GetMove(pChoice, true, null);
            Move enemyMove  = GetMove(eChoice, false, enemy);

            Console.WriteLine($"You chose {playerMove.Name} (Speed: {playerMove.Speed})");
            Console.WriteLine($"Enemy chose {enemyMove.Name} (Speed: {enemyMove.Speed})");

            bool playerFirst = playerMove.Speed >= enemyMove.Speed;
            if (playerFirst)
            {
                ExecuteCombat(playerMove, enemyMove, enemy, true);
                if (enemy.HP <= 0) break;
                ExecuteCombat(enemyMove, playerMove, enemy, false);
            }
            else
            {
                ExecuteCombat(enemyMove, playerMove, enemy, false);
                if (Stats[0] <= 0) break;
                ExecuteCombat(playerMove, enemyMove, enemy, true);
            }

            Console.WriteLine($"Your HP: {Stats[0]} | Enemy HP: {enemy.HP}");
        }

        if (Stats[0] <= 0)
            Console.WriteLine("You've been defeated!");
        else if (enemy.HP <= 0)
        {
            Console.WriteLine($"You defeated the {enemy.Name}!");
            Console.WriteLine("You gain 20 EXP.");
            Stats[3] += 20;

            int drop = rng.Next(1, 4);
            if (drop == 1) { FoodCount++; Console.WriteLine("You found food!"); }
            else if (drop == 2) { PotionCount++; Console.WriteLine("You found a potion!"); }

            LevelUp();
        }

        Console.WriteLine();
        MenuBoot();
    }

    static Move GetMove(int choice, bool isPlayer, Enemy enemy)
    {
        if (isPlayer)
        {
            switch (choice)
            {
                case 1: return new Move("Strike", 2, 10 + Stats[1], 0);
                case 2: return new Move("Block", 1, 2 + Stats[1] / 2, 5 + Stats[2]);
                case 3: return new Move("Dodge", 3, 0, 0);
            }
        }
        else
        {
            switch (choice)
            {
                case 1: return new Move("Strike", 2, 8 + enemy.ATK, 0);
                case 2: return new Move("Block", 1, 2 + enemy.ATK / 2, 5 + enemy.DEF);
                case 3: return new Move("Dodge", 3, 0, 0);
            }
        }
        return new Move("Unknown", 0, 0, 0);
    }

    static void ExecuteCombat(Move attacker, Move defender, Enemy enemy, bool isPlayer)
    {
        string atker = isPlayer ? "You" : "Enemy";
        string defr = isPlayer ? "Enemy" : "You";

        if (defender.Name == "Dodge" && attacker.Speed < defender.Speed)
        {
            Console.WriteLine($"{defr} dodged the attack!");
            return;
        }

        int dmg = attacker.Damage;
        if (defender.Block > 0)
        {
            dmg = Math.Max(0, attacker.Damage - defender.Block);
            Console.WriteLine($"{defr} blocked, reducing damage to {dmg}.");
        }

        if (dmg > 0)
        {
            if (isPlayer)
            {
                enemy.HP -= dmg;
                Console.WriteLine($"You hit the enemy for {dmg} damage!");
            }
            else
            {
                Stats[0] -= dmg;
                Console.WriteLine($"Enemy hit you for {dmg} damage!");
            }
        }
        else
        {
            Console.WriteLine($"{atker}'s attack did no damage.");
        }
    }

    static Enemy GenerateEnemy()
    {
        int type = rng.Next(3);
        switch (type)
        {
            case 0: return new Enemy("Beast", 25 + Stats[4] * 3, 3 + Stats[4], 1 + Stats[4] / 2);
            case 1: return new Enemy("Werewolf", 30 + Stats[4] * 4, 5 + Stats[4] * 2, 2 + Stats[4]);
            case 2: return new Enemy("Undead", 20 + Stats[4] * 2, 2 + Stats[4], 4 + Stats[4] * 2);
            default: return new Enemy("Unknown", 20, 2, 1);
        }
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

class Enemy
{
    public string Name;
    public int HP;
    public int ATK;
    public int DEF;

    public Enemy(string name, int hp, int atk, int def)
    {
        Name = name;
        HP = hp;
        ATK =atk;
        DEF =def;
    }
}

class Move
{
    public string Name;
    public int Speed;
    public int Damage;
    public int Block;

    public Move(string name, int speed, int damage, int block)
    {
        Name = name;
        Speed = speed;
        Damage = damage;
        Block = block;
    }
}
