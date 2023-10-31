namespace CoreLibrary;

public class Creature
{
    // Core
    public decimal HP { get; set; } = 100;
    public decimal MP { get; set; } = 100;
    public long Level { get; set; } = 1;
    public long XP { get; set; } = 0;

    // Attributes
    public long Strength { get; set; } = 5;
    public long Dexterity { get; set; } = 5;
    public long Intelligence { get; set; } = 5;
}
