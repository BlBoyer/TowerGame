using UnityEngine;

//we may change this to loot health and use various health modifiers IDK
public class LootVial : LootItem
{
    [Range(0, 50)]
    public int maxAmount;
    protected override PlayerItem SelectLoot()
    {
        var randInt = Random.value * maxAmount;
        return new Vial() { Amount = 1, modifier = (int)randInt };
    }
}
