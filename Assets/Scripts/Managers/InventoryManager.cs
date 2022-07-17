using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
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
    public List<GameObject> doorKeys = new();
    private GameManager manager;

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
                Debug.Log("inv task ran");
                if (manager.getStatus() == 2) 
                {
                    Debug.Log("getting inv");
                    GetInv();
                    status = 2;
                }
            }
        });
    }
    //we need to display the list when opening inventory and call onInteract when selecting the entry which could be buttons or clickable fields
    /*void onInteract()
    {
        //call item method from item class/record, if it has one(maybe we change from records to classes, or use the string effect of the record)
    }*/
    //called from interactable UI method of game, needs passed an Object(invisible)
    //if we render the object in inventory we prob could add a sprite to the record
    //need to not save this until later, but bc we're testing
    public void AddToInv(PlayerItem item)
    {
        inventory.Add(item);
        Debug.Assert(inventory.Contains(item), $"{item} couldn't be added to inventory");
        Debug.Log($"{item.Amount} {item.Name} added to your inventory!");
        //update inventory key in game manager, AddProp sets playerDirty true
        manager.Replace("Inventory", (System.Object)inventory);
        Debug.Log("replaced Inventory key with new list");
        //this calls save, which we won't do until save is called in our menu or wherever we add save funcitonality
        manager.SavePlayerData();
    }
    public async void GetInv()
    {
        //to serialize arrays, we have to turn it into a list of JObjects and iterate over it to map our custom type
        Debug.Log("getting inv from manager called");
        //1st get player logs, get the type of returned data
        Debug.Log("keyValue from manager obtained" + manager.GetPlayerInfo("Inventory").GetType());
        List<JObject> invList;
        await Task.Run(() =>
        {
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
        });
        Debug.Log("inventory list: ");
        inventory.ForEach(item => Debug.Log(item));
    }
}
