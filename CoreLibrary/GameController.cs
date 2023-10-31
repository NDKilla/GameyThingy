using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary;
public class GameController
{
    public static GameController Ref { get; private set; } = new GameController();
    public Random Random { get; } = new Random();
    public Stopwatch GameTimer { get; } = Stopwatch.StartNew();

    public Creature Player { get; private set; } = new Creature() { Name = "Player" };
    public Creature Monster { get; private set; } = new Creature();
    public bool Exiting { get; private set; } = false;
    public void MainLoop()
    {
        while (!Exiting)
        {
            Player.Attack(Monster);
            Thread.Sleep(333);
            if (!Monster.Alive)
            {
                Console.WriteLine("Monster defeated!");
                Player.HP = 100;
                Player.MP = 100;
                Monster = new Creature();
            }
            else
            {
                Monster.Attack(Player);
                Thread.Sleep(333);
            }
            if (!Player.Alive)
            {
                var xpLoss = (long)(Player.XP * 0.1);
                Console.WriteLine($"Player defeated! Losing {xpLoss} XP!");
                Player.XP -= xpLoss;
                Player.HP = 100;
                Player.MP = 100;
            }
            Thread.Sleep(333);
        }
    }
}
