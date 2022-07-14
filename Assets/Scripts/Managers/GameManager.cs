using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;
#nullable enable
//This is the script we will be using to load, modify, and save game data, this should be instantiated in the main menu scene and dontDestroy
//if not a monobehavior, it shouldn't get destroyed by unity's garbage collector, IDKH to register the singleton
public class GameManager : MonoBehaviour
{
    //Construction
    //member variable to set when selecting game to load, or creating new file, defaults to myGame
    //maybe we make a method to set the instance variable
    public static string _gameName { get; set; } = "myGame"!;
    /*private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Game Manager is NULL");
                //add the manager under managers **
                GameObject manager = new GameObject("GameManager");
                manager.AddComponent<GameManager>();
            }
            return _instance;
        }
    }*/
    //ignore null objects when serializing data, use in full scope
    private static readonly JsonSerializerSettings _options = new() { NullValueHandling = NullValueHandling.Ignore };

    //Declarations
    /**need to change paths based on selected game instance**/
    private static string dir_Path = Assembly.GetExecutingAssembly().Location;
    private static string savePath = $"{dir_Path}/{_gameName}/save.JSON";
    private static string tempPath = $"{dir_Path}/{_gameName}/temp.JSON";
    private static string gameSavePath = $"{dir_Path}/{_gameName}/globals.JSON";
    private static SortedList Data = new();
    private static SortedList GameData = new(); //only access by key, order doesn't matter

    //Methods
    private void Awake()
    {
        //persist GameManager
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //initialize save data, deserialize json, set static object that we can access with newtonsoft
        //ReadPlayerData();
        //ReadGameData();
    }
    //write test to make sure tempData is correct, also test Data on Read, and make sure it's clear after save
    async Task OnSave(string path) 
    {
        //serialize data, write to output, if Data is not null(ignored by serializer settings)
        await Task.Run(() => 
        {
            //testable string, if it doesn't run in order make it a task<string> and wait for it to complete with result
            string jsonString = JsonConvert.SerializeObject(Data, _options);
            //delete file
            File.Delete(path);
            //re-write file
            File.WriteAllText(path, jsonString);
        });
    }
    //call method in game
    public async Task SaveGameData()
    {
        //create a temp save
        await OnSave(tempPath);
        //save game
        await OnSave(savePath);
        File.Delete(tempPath);

    }
    //obtain saved info deserialize json data
    public async Task ReadPlayerData()
    {
        await OnLoad();
        //make sure we readData, new game start-up should create the data file, with all necessary properties
        //set Data deserialized data, read from path
        Data = JsonConvert.DeserializeObject<SortedList>(File.ReadAllText(@savePath));
    }
    async Task OnLoad()
    {
        //attach temp save logic here, runs before load method executes
        //i.e. if save doesn't exist, loadTempSave
        await Task.Run(()=>Debug.Log("Check if save exists."));
        
    }
    public async Task ReadGameData() 
    {
        //Add selected gamefolder to path
        await Task.Run(()=>GameData = JsonConvert.DeserializeObject<SortedList>(File.ReadAllText(@savePath)));
    }

    //method that takes on object property for saving one piece of info at a time, we could also add that to the list and check if the list is dirty
    //this is only useful if we want to save a piece of info to the game, not update the player save file, like a door unlocked variable, or a boss exists variable
    //maybe we should have a second save file for game data, that doesn't get deleted and we cumulatively add keys as needed to the file,
    //this would make it possible to transfer the character out of the game instance, adn start a new game with upgraded stats
    public void ConvertInfo(Object info)
    {
        //serialiaze global switch object
        //call onSave
    }
    public void GetSwitchInfo(string prop) 
    {
        //check the gameSave static array for the key[prop], read it's value
    }
    //add game data, we won't remove data since this should be cumulative, I'm passing in  objects bc these might be arrays with multiple values
    //I.E. keys = [blue key, red key, green key]
    public void AddData(string keyName, System.Object prop)
    {
        //add key, value to game list
        GameData.Add(keyName, prop);
    }
    public System.Object GetData(string keyName) 
    {
        return GameData[keyName];
    }
    //REM: changes to this static object will reflect in save, bc we delete the save file before callling save
    //NEEDS, create a temp save file onSaving in case our async save function gets interrupted after del. Then, on loadGame, if a temp file exists, rename it to the save file
    public void AddProp(string keyName, System.Object? prop)
    {
        //add a new key to player list, set to null or value
        Data.Add(keyName, prop);
    }
    public void RemoveProp(string keyName) 
    {
        //remove a property from our save object, **maybe we need to remove from save data to handle memory
        Data.Remove(keyName);
    }

}
//NEED TO CREATE A STATIC OBJECT THAT REPRESENTS OUR SAVEGAME FILE, WE UPDATE THE KEYS WHEN WE SAVE INFO
//if the info object is dirty, we call save game/or we save on exiting the scene and from the menu, hitting save

/*	SortedList
 *	https://www.tutorialspoint.com/csharp/csharp_sortedlist.htm METHODS
 *	It uses a key as well as an index to access the items in a list.
 *	A sorted list is a combination of an array and a hash table. 
 *	It contains a list of items that can be accessed using a key or an index. 
 *	If you access items using an index, it is an ArrayList, and if you access items using a key , it is a Hashtable. 
 *	The collection of items is always sorted by the key value.
 */
/*{
	Player : {
		Name: ,
		Level: ,
		Exp: ,
		Position: ,
		Health: ,
	}
	Inventory: [invArray]
	Scene : {
		peristed scene objects idk
	}
    Stats : {
        EnemiesKilled: ,
        DungeonsExplored: ,
        BattlesPerVictory: ,
        SpecialItemsFound: []
    }
}*/
