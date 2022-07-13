using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is the script we will be using to load, modify, and save game data
public class GameManager : MonoBehaviour
{
    //reference all gameobjects and scripts with saveable variables, any variable that gets modified in-game ONLY, should be modified by individual scripts
    //i.e. the player, the current scene, stores, instantiated objects in current scene

    // Start - LoadGame is called before the first frame update
    void Start()
    {
        //initialize save data
        ReadGameData();
    }
    void SaveGameData() 
    {
        //save game data, we need all variables here, all gameobjects with scripts that contain modded fields need added to game manager
        //this should create the instance of InventoryManager, by calling InventoryManager.AddItem()
    }
    void ReadGameData() 
    {
        //make sure we readData, new game start-up should create the data file
    }

}
