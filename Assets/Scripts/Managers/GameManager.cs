using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
#nullable enable
//This is the script we will be using to load, modify, and save game data, this should be instantiated in the main menu scene and dontDestroy
//if not a monobehavior, it shouldn't get destroyed by unity's garbage collector, IDKH to register the singleton
public class GameManager : MonoBehaviour
{
    //Construction
    //member variable to set when selecting game to load, or creating new file, defaults to myGame, don't allow created games to share the same name
    //from main menu we limit ability to name games
    //ignore null objects when serializing data, use in full scope
    private static readonly JsonSerializerSettings _options = new() { NullValueHandling = NullValueHandling.Ignore };

    //Declarations
    /**need to change paths based on selected game instance**/
    public static string? gameName { get; set; }
    private static string dir_Path;
    private static string savePath;
    private static string gameSavePath;
    private static SortedList PlayerData = new();
    private static SortedList GameData = new(); //only access by key, order doesn't matter
    public static int readStatus = 0;
    //public static bool loadedComplete = false;
    private static bool playerDirty = false;
    private static bool gameDirty = false;

    //Methods
    private void Awake()
    {
        //persist GameManager
        DontDestroyOnLoad(gameObject);
        //get data path
        if (gameName == null)
        {
            gameName = "myGame";
        }
        dir_Path = $"{Application.dataPath}/{gameName}";
        savePath = $"{dir_Path}/save.JSON";
        gameSavePath = $"{dir_Path}/globals.JSON";

        //create game directory if it doesn't exist, this is a method for new game to create, so let's not do that here.
        //We'll fix this later
        //the save paths, should create themselves
        if (!Directory.Exists(dir_Path))
        {
            Directory.CreateDirectory(dir_Path);
            File.Create(savePath);
            Debug.Assert(File.Exists(savePath));
            File.Create(gameSavePath);
            Debug.Assert(File.Exists(gameSavePath));
            /*
             * generate new keys for playerData here, (will read data overwrite this? check)
             */
            PlayerData.Add("Inventory", new List<PlayerItem>());
            Debug.Assert(PlayerData.ContainsKey("Inventory"));
            playerDirty = true;
            GameData.Add("init", "none");
            Debug.Assert(GameData.ContainsKey("init"));
            gameDirty = true;
        }
    }
    void Start()
    {
        //we need to make this a task with a bool and get the bool to GetValues from other scripts
        ReadPlayerData();
        ReadGameData();
    }
    //Data IO
    //**if we want to wait for a message saying the game is saved, we will need to do something similar to the read, make sure dirties are false;
    public async void SavePlayerData()
    {
        if (playerDirty)
        {
            //save player
            //serialize data, write to output, if Data is not null(ignored by serializer settings)
            await Task.Run(() =>
            {
                Debug.Log("Player Save Ran.");
                //testable string, if it doesn't run in order make it a task<string> and wait for it to complete with result
                string jsonString = JsonConvert.SerializeObject(PlayerData, _options);
                //re-write file
                File.WriteAllText(savePath, jsonString);
            });
            playerDirty = false;
        }
        else
        {
            Debug.Assert(playerDirty, "No player changes to Save!");
        }
        //we need to save all data at the same time, so that problems don't happen, like losing the keys and still having global switch unlocked
        //save game data
        SaveGameData();
    }
    private async static void SaveGameData()
    {
        //serialize data, write to output, if Data is not null(ignored by serializer settings)
        if (gameDirty)
        {
            await Task.Run(() =>
            {
                Debug.Log("Game Save Ran.");
                string jsonString = JsonConvert.SerializeObject(GameData, _options);
                //re-write file
                File.AppendAllText(gameSavePath, jsonString);
            });
        } 
        else
        {
            Debug.Assert(playerDirty, "No game changes to Save!");
        }
    }
    private async void ReadAllData() 
    {
        await ReadPlayerData();
        await ReadGameData();
    }
    //obtain saved info deserialize json data, THIS MUST BE MARKED ASYNC**, we could prob make this public and get Task.isCompleted bool instead
    private static async Task ReadPlayerData()
    {
            Debug.Log("player data read");
            PlayerData = JsonConvert.DeserializeObject<SortedList>(File.ReadAllText(@savePath), _options);
            Debug.Assert(PlayerData.Contains("Inventory") == true, "PlayerData doesn't contain inventory");
            //we're reading this key at the very end of everything still!
            Debug.Log(PlayerData["Inventory"]);
            readStatus = 1;
    }
    private static async Task ReadGameData()
    {
        //we should make sure the readfile is not null here
            Debug.Log("game date read");
            GameData = JsonConvert.DeserializeObject<SortedList>(File.ReadAllText(@gameSavePath), _options);
            readStatus = 2;
    }
    //DTO management
    //Player data management
    public void AddProp(string keyName, System.Object? prop)
    {
        //add a new key to player list, set to null or value
        PlayerData.Add(keyName, prop);
        playerDirty = true;
    }
    public void Replace(string keyName, System.Object prop)
    {
        Debug.Log(PlayerData);
        PlayerData[keyName] = prop;
        playerDirty = true;
    }
    public void RemoveProp(string keyName)
    {
        //remove a property from our save object
        PlayerData.Remove(keyName);
        playerDirty = true;
    }
    //Get info from player data, needs to be async
    public System.Object GetPlayerInfo(string keyName)
    {
        Debug.Log("getting player info received key: " + keyName);
        return PlayerData[keyName];
    }
    //Game Data Management
    //method that takes on object property for saving one piece of info at a time, instead of re-writing file
    public void AddData(string keyName, System.Object prop)
    {
        //add key, value to game list
        GameData.Add(keyName, prop);
        //this keeps the game data from saving if nothing has changed during the session
        gameDirty = true;
    }
    //Get info from game data
    public System.Object GetGameInfo(string keyName)
    {
        return GameData[keyName];
    }
    public int getStatus() 
    {
        return readStatus;
    }
}
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
