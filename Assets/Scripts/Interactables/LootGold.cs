using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we should definitely abstract the loot class and have our diff types to pass in
public class LootGold : InteractableObject
{
    [System.NonSerialized] public Gold looted;
    //override method
    public record Gold()
    {
        public int amount { get; set; }
    };
    protected override void onInteract()
    {
        //stop rendering object
        this.gameObject.SetActive(false);
        //declare random loot variable that we might want to access somewhere else? like a notification UI?
        looted = selectRandomLoot();
        Debug.Log(looted.amount);
        //add to inventory
    }
    /*
     * NEEDS to figure out if our loot types are modifiers or gameobjects.
     * if modifiers, add array of modifier objects with props
     * if gameObjects, add serialized variable for objects to randomize
     * Maybe we want a generic array for any type, or chests that do only certian types
     * for now, we hard-code
    */

    private List<Gold> lootList = new List<Gold>() {
        new Gold(){ amount = 10},
        new Gold(){ amount = 20},
        new Gold(){ amount = 30},
        new Gold(){ amount = 40},
        new Gold(){ amount = 1},
    };
    //get random loot method from our loot class or loot array we can attach here that we add with all the types of loot we want in this map
    private Gold selectRandomLoot() 
    {
        //getRandom modify by list size
        var randInd = Random.value * lootList.Count;
        return lootList[(int)randInd];
    }
}
