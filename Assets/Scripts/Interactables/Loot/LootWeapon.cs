using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWeapon : LootItem
{
    public List<string> weapons;
    
    protected override PlayerItem SelectLoot() 
    {
        //we'll make sure this goes to the weapon part of inventory from there
        int randInd = UnityEngine.Random.Range(0, weapons.Count);
        Type subType = Type.GetType((string)weapons[randInd]);
        var playerItem = (PlayerItem)Activator.CreateInstance(subType);
        Debug.Assert(playerItem is Weapon, "This is not a weapon item!");
        return playerItem;
    }
}
