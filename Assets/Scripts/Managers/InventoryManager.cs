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
        //gameManager array needs to have keys created
        GetInv();
    }
    //we need to display the list when opening inventory and call onInteract when selecting the entry which could be buttons or clickable fields
    /*void onInteract()
    {
        //call item method from item class/record, if it has one(maybe we change from records to classes, or use the string effect of the record)
    }*/
    //called from interactable UI method of game, needs passed an Object(invisible)
    //if we render the object in inventory we prob could add a sprite to the record
    //need to not save this until later, but bc we're testing
    public async void AddToInv(PlayerItem item)
    {
        inventory.Add(item);
        Debug.Assert(inventory.Contains(item), $"{item} couldn't be added to inventory");
        Debug.Log($"{item.Amount} {item.Name} added to your inventory!");
        //update inventory key in game manager
        //this calls save
        await manager.SavePlayerData();
    }
    public void GetInv() 
    {
        //We may need to go with parsing the jsonarray first**
        //var inv = JArray.Parse(manager.GetPlayerInfo("Inventory").ToString());
        inventory = JsonConvert.DeserializeObject<List<PlayerItem>>(manager.GetPlayerInfo("Inventory").ToString(), new PlayerItemConverter());
        //var inv = JsonConvert.DeserializeObject<List<JObject>>(manager.GetPlayerInfo("Inventory").ToString());
        //need to say for each object, get object name field, use that as type argument for deserialization
        /*foreach (var obj in inv) 
        {
            var item = JsonConvert.DeserializeObject<PlayerItem>(obj.ToString());
            Debug.Log(item);
        }*/
        Debug.Log(inventory[0]);
        //var gold1 = JsonConvert.DeserializeObject<Gold>(inv[0].ToString());
        //Debug.Log(gold1);
        //inventory.Add(JsonConvert.DeserializeObject<PlayerItem>();
        //Debug.Log(inventory[0]);
        Debug.Assert(inventory.GetType() == typeof(List<PlayerItem>), "saved key is of wrong type.");
    }
}
