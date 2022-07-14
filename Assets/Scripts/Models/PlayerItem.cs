public abstract record PlayerItem
{
    public abstract string Name { get; protected set; }
    public abstract string Description { get; protected set; }
    public int Amount;
}
//direct subclasses
public record Gold : PlayerItem
{
    public override string Name { get; protected set; } = "Gold"!;
    public override string Description { get; protected set; } = "The standard currency"!;
}
public record Weapon : PlayerItem 
{
    public override string Name { get; protected set; }
    public override string Description { get; protected set; }
}
public record Disc : Weapon
{
    public override string Name { get; protected set; } = "Disc"!;
    public override string Description { get; protected set; } = "A deadly throwing disc wiht razor sharp edges"!;
}