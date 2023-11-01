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
    public event EventHandler<double>? GameTick;
    protected virtual void OnGameTick()
    {
        EventHandler<double>? gameTickCopy = GameTick;
        
        if (gameTickCopy != null)
        {
            gameTickCopy(this, GameTimer.Elapsed.TotalMilliseconds);
        }
    }
    public event EventHandler<string>? EventMessage;
    internal virtual void OnEventMessage(string e)
    {
        EventHandler<string>? eventMessageCopy = EventMessage;

        if (eventMessageCopy != null)
        {
            eventMessageCopy(this, e);
        }
    }
    public Random Random { get; } = new Random();
    public Stopwatch GameTimer { get; } = Stopwatch.StartNew();

    public Creature Player { get; private set; } = new Creature() { Name = "Player" };
    public Creature Monster { get; private set; } = new Creature();
    public bool Exiting { get; set; } = false;
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
                OnEventMessage("Monster defeated!");
                Player.HP = Player.MaxHP;
                Player.MP = Player.MaxMP;
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
                OnEventMessage($"Player defeated! Losing {xpLoss} XP!");
                Player.XP -= xpLoss;
                Player.HP = Player.MaxHP;
                Player.MP = Player.MaxMP;
            }
            OnGameTick();
            Thread.Sleep(15);
        }
    }
}
