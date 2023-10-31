// See https://aka.ms/new-console-template for more information
using CoreLibrary;

//Random r = new();

Console.WriteLine("Hello, World!");

GameController.Ref.MainLoop();

//Creature c1 = new();
//Creature c2 = new();

//while (c1.HP > 0 && c2.HP > 0)
//{
//    var c2atk = r.Next((int)c2.Strength);
//    if (c2atk == c2.Strength-1)
//    {
//        c2atk *= 2;
//        Console.WriteLine($"C2 crits for {c2atk}!!");
//    }
//    if (c2atk > 0)
//    {
//        Console.WriteLine($"C2 attacks for {c2atk}");
//    }
//    else
//    {
//        Console.WriteLine("C2 missed!");
//    }
//    c1.HP -= c2atk;
//    if (c1.HP <= 0)
//    {
//        Console.WriteLine("C2 wins");
//    }

//    var c1atk = r.Next((int)c1.Strength);
//    if (c1atk == c1.Strength-1)
//    {
//        c1atk *= 2;
//        Console.WriteLine($"C1 crits for {c1atk}!!");
//    }
//    if (c1atk > 0)
//    {
//        Console.WriteLine($"C1 attacks for {c1atk}");
//    }
//    else
//    {
//        Console.WriteLine("C1 missed!");
//    }
//    c2.HP -= c1atk;
//    Console.WriteLine($"C2 attacks for {c1atk}");
//    if (c2.HP <= 0)
//    {
//        Console.WriteLine("C1 wins");
//    }

//    Thread.Sleep(100);
//}
