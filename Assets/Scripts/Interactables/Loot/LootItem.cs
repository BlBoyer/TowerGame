using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class LootItem : InteractableObject
{
    //REMAKE TO ABSTRACT THE NULL REFERENCE ERROR IS ON THE INTERACTION MANAGER NOT THE GOLD OBJECT

    //protected PlayerItem itemToAdd { get; set; }
    //override method
    protected override void onInteract()
    {
        //stop rendering object
        this.gameObject.SetActive(false);
        //add to inventory with inventory manager service
        var itemToAdd = SelectLoot();
        Debug.Log(itemToAdd);
        //the inventory manager needs to get the type of item and group by type
        var manager = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        manager.AddToInv(itemToAdd);
        //send notification with player item details (member fields of an inventory class item like name and amount)
    }
    protected abstract PlayerItem SelectLoot();
}
