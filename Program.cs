List<string> Menu = new List<string> {"Movement","Stats", "Inventory", "Exit game"};

Console.WriteLine();
Console.WriteLine("Welcome to Command Line RPG.");
Console.WriteLine();

int i = 1;
   foreach (string action in Menu)
   {
       Console.WriteLine("[" + i + "]" + " " + action);
       i++;
   }