using System.Collections.Generic;
using UnityEngine;

//we should definitely abstract the loot class and have our diff types to pass in LootItem
public class LootGoldOriginal : LootItem
{
    //protected override PlayerItem itemToAdd { get; set; }
    public List<Gold> lootList = new() {
        new Gold(){ Amount = 10},
        new Gold(){ Amount = 20},
        new Gold(){ Amount = 30},
        new Gold(){ Amount = 40},
        new Gold(){ Amount = 1}
    };
    protected override PlayerItem SelectLoot()
    {
        var randInd = Random.value * lootList.Count;
        return lootList[(int)randInd];
    }
}
