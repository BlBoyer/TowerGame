public abstract record PlayerItem
{
    public abstract string Name { get; protected set; }
    public abstract string Description { get; protected set; }
    public int Amount;
}
//subclasses
public record Gold : PlayerItem
{
    public override string Name { get; protected set; } = "Gold"!;
    public override string Description { get; protected set; } = "The standard currency"!;
}