Console.WriteLine();
Console.WriteLine("Welcome to Command Line RPG.");
Console.WriteLine();

List<int> Stats = new List<int> {20, 1, 1, 0, 1};

//int HP = 20;
//int ATK = 1;
//int DEF = 1;
//int EXP = 0;
//int LV = 1;

static void MenuBoot()
{
    List<string> Menu = new List<string> {"Movement","Stats", "Inventory", "Exit game"};
    int i1 = 1;
    foreach (string action in Menu)
   {
       Console.WriteLine("[" + i1 + "]" + " " + action);
       i1++;
   }
Console.WriteLine();

string UserChoice = Console.ReadLine();

bool test = int.TryParse(UserChoice, out int actionChoice);

if (test == true)
{
switch (actionChoice)
{
    case 1:
        MovementMenuBoot();
        break;
    case 2:
        foreach (int stat in Stats)
        {
            Console.WriteLine(stat, ": ")
        }
        break;
    case 3:
        
        break;
    case 4:
        
        break;
    default:
        break;
}


}
else
{
    Console.WriteLine("Invalid input try again");
    MenuBoot();
}


}

static void MovementMenuBoot()
{
    List<string> MovementMenu = new List<string> {"Up", "Down", "Left", "Right"};
    Console.WriteLine();
    int i2 = 1;
    foreach (string Movement in MovementMenu)
   {
       Console.WriteLine("[" + i2 + "]" + " " + Movement);
       i2++;
   }
   string x = Console.ReadLine();
   Console.WriteLine();
   
   MenuBoot();
}

MenuBoot();



    //static int StatsMenuBoot(int HP,int ATK,int DEF,int LV)
    //{
    //    Console.WriteLine($"HP = {HP} ");
    //    Console.WriteLine($"ATK = {ATK}");
    //    Console.WriteLine($"DEF = {DEF}");
    //    Console.WriteLine($"LV = {LV}");
    //    MenuBoot();
    //    
    //}