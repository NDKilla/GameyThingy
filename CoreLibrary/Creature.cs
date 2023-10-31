namespace CoreLibrary;

public class Creature
{
    // Core
    public decimal HP { get; set; } = 100;
    public decimal MP { get; set; } = 100;
    public string Name { get; set; } = "Monster";
    public long Level { get; set; } = 1;
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
                Strength++;
                Dexterity++;
                Intelligence++;
                Console.WriteLine($"Level reached: {Level}!");
            }
        }
    }
    public bool Alive => HP > 0;

    // Attributes
    public long Strength { get; set; } = 5;
    public long Dexterity { get; set; } = 5;
    public long Intelligence { get; set; } = 5;

    public decimal CritChance => (decimal)(Dexterity * 0.01);
    public decimal CritMultiplier => (decimal)((200 + Dexterity * 5) * 0.01);

    public void Attack(Creature target)
    {
        var damage = GetDamage();
        damage = GetCritDamage(damage);
        target.Defend(this, damage);
    }

    public void Defend(Creature target, decimal damage)
    {
        Console.WriteLine($"{target.Name} attacked {this.Name} for {damage}!");
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
        return 1 + r1 + r2;
    }

    private decimal GetCritDamage(decimal damage)
    {
        var roll = (decimal)GameController.Ref.Random.NextDouble();
        if (roll < CritChance)
        {
            Console.WriteLine($"{Name} scored a critical hit!");
            damage *= CritMultiplier;
        }
        return damage;
    }
}
