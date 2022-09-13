using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we should definitely abstract the loot class and have our diff types to pass in LootItem
public class LootGold : LootItem
{
    [Range(0,300)]
    public int maxAmount;
    protected override PlayerItem SelectLoot()
    {
        var randInt = Random.value * maxAmount;
        return new Gold() { Amount = (int)randInt  };
    }
}
