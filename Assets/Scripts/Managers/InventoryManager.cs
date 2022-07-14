using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    //we could just place the object in the scene in managers and not destroy it on onLoad
    //Construction
    private static InventoryManager _instance;
    public static InventoryManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = new GameObject("Inventory");
                manager.AddComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    //Declarations
    public ArrayList inventory = new();

    //Methods
    void Awake()
    {
        _instance = this;
        //persist the manager through scene changes if we want
        DontDestroyOnLoad(gameObject);
    }
    //this needs to be an item that is an InvItem
    //we need to display the list when opening inventory and call onInteract when selecting the entry which could be buttons or clickable fields
    /*void onInteract()
    {
        //call item method from item class/record
    }*/
    //called from interactable/private method of game, needs passed an Object(invisible)
    //if we render the object in inventory we prob could add a sprite to the record
    public void AddToInv(PlayerItem item)
    {
        inventory.Add(item);
        Debug.Assert(inventory.Contains(item), $"{item} couldn't be added to inventory");
        Debug.Log($"{item.Amount} {item.Name} added to your inventory!");
    }
}
