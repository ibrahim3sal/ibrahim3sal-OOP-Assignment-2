namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; } = "";
    public int Damage { get; set; }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; } = "";
    public int Health { get; set; }
    public Weapon Weapon { get; set; } = new Weapon();
    public List<string> Abilities { get; set; } = new();

    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);

        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    protected Enemy(Enemy original)
    {
        _modelData = original._modelData;

        Name = original.Name;
        Health = original.Health;

        Weapon = new Weapon
        {
            Name = original.Weapon.Name,
            Damage = original.Weapon.Damage
        };

        Abilities = new List<string>(original.Abilities);
    }

    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon
        {
            Name = "Axe",
            Damage = 25
        };

        Abilities.Add("Rage");
    }

    private Orc(Orc original) : base(original)
    {
    }

    public override Enemy Clone()
    {
        return new Orc(this);
    }
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon
        {
            Name = "Bow",
            Damage = 18
        };

        Abilities.Add("Stealth");
    }

    private Elf(Elf original) : base(original)
    {
    }

    public override Enemy Clone()
    {
        return new Elf(this);
    }
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy enemy)
    {
        return enemy.Clone();
    }
}