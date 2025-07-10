List<string> Menu = new List<string> {"Movement","Stats", "Inventory", "Exit game"};

Console.WriteLine();
Console.WriteLine("Welcome to Command Line RPG.");
Console.WriteLine();

bool menuBoot = true;


while (menuBoot == true)
{
    int i = 1;
    foreach (string action in Menu)
   {
       Console.WriteLine("[" + i + "]" + " " + action);
       i++;
   }

menuBoot = false;
Console.WriteLine();


string UserChoice = Console.ReadLine();

bool test = int.TryParse(UserChoice, out int actionChoice);

if (test == true)
{
switch (actionChoice)
{
    case 1:
        break;
    case 2:
        break;
    case 3:
        break;
    default:
        break;
}


}
else
{
    Console.WriteLine("Invalid input try again");
    menuBoot = true;
}
}