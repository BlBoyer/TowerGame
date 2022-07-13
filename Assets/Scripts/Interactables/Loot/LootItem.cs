using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LootItem : InteractableObject
{
    //REMAKE TO ABSTRACT THE NULL REFERENCE ERROR IS ON THE INTERACTION MANAGER NOT THE GOLD OBJECT

    //protected PlayerItem itemToAdd { get; set; }
    public List<Gold> lootList = new() {
        new Gold(){ Amount = 10},
        new Gold(){ Amount = 20},
        new Gold(){ Amount = 30},
        new Gold(){ Amount = 40},
        new Gold(){ Amount = 1}
    };
    //override method
    protected override void onInteract()
    {
        //stop rendering object
        this.gameObject.SetActive(false);
        //add to inventory with inventory manager service
        var itemToAdd = SelectLoot();
        Debug.Log(itemToAdd);
        InventoryManager.instance.AddToInv(itemToAdd);
        //send notification with player item details (member fields of an inventory class item like name and amount)
        PlayerItem SelectLoot()
        {
            //get the list by attaching a second script to the object which has th object's record and an editable list on it, or keep figuring out
            var randInd = Random.value * lootList.Count;
            return lootList[(int)randInd];
        }
    }
}
