public abstract record PlayerItem
{
    public abstract string Name { get; protected set; }
    public abstract string Description { get; protected set; }
    public int Amount;
    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}
//direct subclasses
public record Gold : PlayerItem
{
    public override string Name { get; protected set; } = "Gold"!;
    public override string Description { get; protected set; } = "The standard currency"!;
}
public record HealthItem : PlayerItem
{
    public override string Name { get; protected set; }
    public override string Description { get; protected set; }
    public int modifier;
}
//HealthItem subclasses
public record Vial : HealthItem
{
    public override string Name { get; protected set; } = "Vial"!;
    public override string Description { get; protected set; } = "A vial of health.";
    //use the modifier in code
}
public abstract record Weapon : PlayerItem
{
    public override string Name { get; protected set; }
    public override string Description { get; protected set; }
    public abstract string EffectKey { get; protected set; } // we can use a SortedList and get the value of the effect(i.e. a modifier function) by the string name
}
//weapon subclasses
public record Disc : Weapon
{
    public override string Name { get; protected set; } = "Disc"!;
    public override string Description { get; protected set; } = "A deadly throwing disc with razor sharp edges"!;
    public override string EffectKey { get; protected set; } = "Ranged";
}