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

    public Creature Player { get; private set; } = new Creature() { Name = "Player", AttackSpeed = 0.66 };
    public Creature Monster { get; private set; } = new Creature();
    public bool Exiting { get; private set; } = false;
    public void MainLoop()
    {
        while (!Exiting)
        {
            if (Player.AttackReady())
            {
                Player.Attack(Monster);
            }
            if (!Monster.Alive)
            {
                Console.WriteLine("Monster defeated!");
                Player.HP = 100;
                Player.MP = 100;
                Monster = new Creature();
                Monster.NextAttack = Monster.CalculateNextAttack();
            }
            else
            {
                if (Monster.AttackReady())
                {
                    Monster.Attack(Player);
                }
            }
            if (!Player.Alive)
            {
                var xpLoss = (long)(Player.XP * 0.1);
                Console.WriteLine($"Player defeated! Losing {xpLoss} XP!");
                Player.XP -= xpLoss;
                Player.HP = 100;
                Player.MP = 100;
            }
            Thread.Sleep(15);
        }
    }
}
