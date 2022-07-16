using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //REM this is a new scene
    //Declarations
    [System.NonSerialized] public string gameName;
    [System.NonSerialized] public GameObject manager;
    [System.NonSerialized] public GameObject exitController;
    [System.NonSerialized] public GameObject inventory;
    void Start()
    {
        SelectGame();
    }
    void SelectGame() //the function we will run on interacting with the menu
    {
        //set game name prop of the manager class
        GameManager.gameName = "variableGameName";
        manager = new("GameManager") { tag = "Manager" };
        manager.AddComponent<GameManager>();
        //initialize save data, deserialize json, set static object that we can access with newtonsoft
        manager.GetComponent<GameManager>().ReadPlayerData();
        //we're not using this yet
        manager.GetComponent<GameManager>().ReadGameData();
        //create keys for player data, also realize GameData will be null until we create that.
        exitController = new("ExitManager") { tag = "ExitController" };
        exitController.AddComponent<ExitManager>();
        inventory = new("InventoryManager") { tag = "Inventory" };
        inventory.AddComponent<InventoryManager>();

    }
}