using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    //Construction

    //Declarations
    //the UI of inventory needs to have sections of UI that we can add/remove from the editor to check functionality/layout easily
    //responsive UI would mean dynamically changing the sections visible
    //we create the ind. UIs by checking length of our Lists to render
    public List<PlayerItem> inventory = new();
    private GameManager manager;
    [System.NonSerialized] public bool completed = false;
    public bool dirty;
    //Methods
    private void Awake()
    {
        //persist the manager through scene changes if we want
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //get data from GameManager - PlayerItems key, KeyList, miscItemLisT
        manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
        //gameManager array needs to have keys created, the question is, can we do without this??
        Task.Run(() => 
        {
            int status = 3;
            while (manager.getStatus() < status)
            {
                Debug.Log("inv task ran "+ manager.getStatus());
                if (manager.getStatus() == 2)
                {
                    Debug.Log("getting inv");
                    GetInv();
                    status = 2;
                    completed = true;
                }
                if (completed) 
                {
                    Debug.Log("inv get data task is complete");
                    break;
                }
            }
        });
    }
    //Get saved data
    public async Task GetInv()
    {
        //to serialize arrays, we have to turn it into a list of JObjects and iterate over it to map our custom type
        Debug.Log("getting inv from manager called");
        //1st get player logs, get the type of returned data
        Debug.Log("keyValue from manager obtained" + manager.GetPlayerInfo("Inventory").GetType());
        List<JObject> invList;
        //second getPlayer logs, but doesn't get that value
        invList = JsonConvert.DeserializeObject<List<JObject>>(manager.GetPlayerInfo("Inventory").ToString());
        Debug.Log("inv variable stored");
        foreach (var obj in invList)
        {
            var item = JsonConvert.DeserializeObject<JObject>(obj.ToString());
            Type subType = Type.GetType((string)item["Name"]);
            var playerItem = (PlayerItem)Activator.CreateInstance(subType);
            //map properties to new object
            playerItem.Amount = (int)item["Amount"];
            inventory.Add(playerItem);
        }
        Debug.Log("inventory list follows: ");
        inventory.ForEach(item => Debug.Log(item));
    }
    public void AddToInv(PlayerItem item)
    {
        /*We're going to say, if an item isn't allowed more than one, the amount is set to zero*/
        /*we remove only the items that are set to zero on using them, so we need an interface for inventory item onInteract*/
        //Stackable items (one instance)
        if (item is Gold)
        {
            var updateItem = inventory.Single(entry => entry is Gold);
            updateItem.Amount += item.Amount;
            Debug.Log(updateItem);
        }
        else if (item.Amount > 0 || item.Amount < 1 && !(inventory.Contains(item)))
        {//Non-Stackable items, and single items
            inventory.Add(item);
        }
        Debug.Log($"{item.Amount} {item.Name} added to your inventory!");
        //update inventory key in game manager, AddProp sets playerDirty true
        manager.Replace("Inventory", (System.Object)inventory);
        Debug.Log("replaced Inventory key with new list");
        dirty = true;
    }
}
