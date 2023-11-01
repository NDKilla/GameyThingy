using System.Diagnostics;

namespace CoreLibrary;

public class Creature
{
    public Creature()
    {
        if (GameController.Ref?.Player == null)
        {
            return;
        }

        Creature player = GameController.Ref.Player;

        Level = GameController.Ref.Random.Next(Math.Max(1, (int)player.Level - 5), (int)player.Level + 5);

        HPLevel = 100 + GameController.Ref.Random.Next(0, (int)Level / 50 * 10);
        MPLevel = 100 + GameController.Ref.Random.Next(0, (int)Level / 50 * 10);
        Strength = 5 + GameController.Ref.Random.Next(0, (int)Level / 10);
        Dexterity = 5 + GameController.Ref.Random.Next(0, (int)Level / 10);
        CritChanceLevel = 5 + GameController.Ref.Random.Next(0, (int)Level / 10);
        CritMultiplierLevel = 5 + GameController.Ref.Random.Next(0, (int)Level / 10);

        HP = MaxHP;
        MP = MaxMP;
    }

    // Core
    public decimal HP { get; set; } = 100;
    public decimal MaxHP => (decimal)(100 + Math.Pow(Level * 10, 1.001) + HPLevel * 10);
    public long HPLevel { get; set; } = 0;
    public void LevelHP()
    {
        if (SkillPoints > 0)
        {
            HPLevel++;
            SkillPoints--;
        }
    }
    public decimal MP { get; set; } = 100;
    public decimal MaxMP => (decimal)(100 + (Math.Pow(Level * 10, 1.0001)) + MPLevel * 10);
    public long MPLevel { get; set; } = 0;
    public void LevelMP()
    {
        if (SkillPoints > 0)
        {
            MPLevel++;
            SkillPoints--;
        }
    }
    public string Name { get; set; } = "Monster";
    public long Level { get; set; } = 1;
    public long SkillPoints { get; set; } = 0;

    private long xp = 0;
    public long XP
    {
        get { return xp; }
        set
        {
            xp = value;
            if (xp >= Level)
            {
                xp = 0;
                Level++;
                SkillPoints++;
                //Strength++;
                //Dexterity++;
                //Intelligence++;
                GameController.Ref.OnEventMessage($"{Name} reached level: {Level}!");
            }
        }
    }
    public bool Alive => HP > 0;

    // Attributes
    public long Strength { get; set; } = 5;
    public void LevelStrength()
    {
        if (SkillPoints > 0)
        {
            Strength++;
            SkillPoints--;
        }
    }
    public long Dexterity { get; set; } = 5;
    public void LevelDexterity()
    {
        if (SkillPoints > 0)
        {
            Dexterity++;
            SkillPoints--;
        }
    }
    public long Intelligence { get; set; } = 5;
    public void LevelIntelligence()
    {
        if (SkillPoints > 0)
        {
            Intelligence++;
            SkillPoints--;
        }
    }

    public decimal CritChance => (decimal)((5 + Dexterity * 0.025 + CritChanceLevel * 0.1) * 0.01);
    public long CritChanceLevel = 0;
    public void LevelCritChance()
    {
        if (SkillPoints > 0)
        {
            CritChanceLevel++;
            SkillPoints--;
        }
    }
    public decimal CritMultiplier => (decimal)((150 + Dexterity * 1 + CritMultiplierLevel * 5) * 0.01);
    public long CritMultiplierLevel = 0;
    public void LevelCritMultiplier()
    {
        if (SkillPoints > 0)
        {
            CritMultiplierLevel++;
            SkillPoints--;
        }
    }

    public double AttackSpeed => 0.2d + Dexterity * 0.05 + AttackSpeedLevel * 0.1; // Attacks per second.
    public double AttackSpeedLevel = 0;
    public void LevelAttackSpeed()
    {
        if (SkillPoints > 0)
        {
            AttackSpeedLevel++;
            SkillPoints--;
        }
    }
    public double NextAttack { get; set; }

    public bool AttackReady()
    {
        return GameController.Ref.GameTimer.Elapsed.TotalMilliseconds > NextAttack;
    }

    public double CalculateNextAttack()
    {
        // Inverse attack speed to get millisecond delay between attacks.
        double attackDelay = 1000 / AttackSpeed;
        //Debug.Print($"({Name}) attack delay: ({attackDelay}) ms");
        // Hard-cap between 0.25 attacks per second and the game tick rate.
        attackDelay = Math.Max(15, Math.Min(attackDelay, 4000));
        var nextAttack = GameController.Ref.GameTimer.Elapsed.TotalMilliseconds + attackDelay;
        if (Name == "Player")
        {
            Debug.Print($"Next player attack time: {nextAttack}");
        }
        return nextAttack;
    }

    public void Attack(Creature target)
    {
        var damage = GetDamage();
        damage = GetCritDamage(damage);
        target.Defend(this, damage);
        NextAttack = CalculateNextAttack();
    }

    public void Defend(Creature target, decimal damage)
    {
        GameController.Ref.OnEventMessage($"{target.Name} attacked {this.Name} for {damage}!");
        HP -= damage;
        HP = Math.Max(HP, 0);
        if (!Alive)
        {
            target.XP += Level;
        }
    }

    private decimal GetDamage()
    {
        // ToDo: bigger numbers
        var r1 = GameController.Ref.Random.Next((int)Strength);
        var r2 = GameController.Ref.Random.Next((int)Strength);
        return (int)Strength + r1;// + r2;
    }

    private decimal GetCritDamage(decimal damage)
    {
        var roll = (decimal)GameController.Ref.Random.NextDouble();
        if (roll < CritChance)
        {
            GameController.Ref.OnEventMessage($"{Name} scored a critical hit!");
            damage *= CritMultiplier;
        }
        return damage;
    }
}
