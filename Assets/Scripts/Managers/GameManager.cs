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
    public static string savePath { get; set; }
    public static string gameSavePath { get; set; }
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
    }
    void Start()
    {
        //if we read both, the value gets set to 2 and than save data runs, which allows the data to be saved for next run, this only works with save data in the files.
        ReadPlayerData();
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
                //testable string
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
        await SaveGameData();
    }
    /**private keeps us from calling the savegame without saving the player data, in case switches are dependent on playerdata**/
    private async Task SaveGameData()
    {
        //serialize data, write to output, if Data is not null(ignored by serializer settings)
        if (gameDirty)
        {
            await Task.Run(() =>
            {
                Debug.Log("Game Save Ran.");
                string jsonString = JsonConvert.SerializeObject(GameData, _options);
                //re-write file
                File.WriteAllText(gameSavePath, jsonString);
            });
        } 
        else
        {
            Debug.Assert(playerDirty, "No game changes to Save!");
        }
    }
    //obtain saved info deserialize json data, THIS MUST BE MARKED ASYNC**, we could prob make this public and get Task.isCompleted bool instead
    private static async Task ReadPlayerData()
    {
            Debug.Log("player data reading from: " + savePath);
            PlayerData = JsonConvert.DeserializeObject<SortedList>(File.ReadAllText(@savePath), _options);
            Debug.Assert(PlayerData.Contains("Inventory") == true, "PlayerData doesn't contain inventory");
            //we're reading this key at the very end of everything still!
            Debug.Log("Inventory value from Game Manager Read method :"+PlayerData["Inventory"]);
            readStatus = 1;
            ReadGameData();
    }
    private static async Task ReadGameData()
    {
        //we should make sure the readfile is not null here
            Debug.Log("game date reading from: " + gameSavePath);
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
        gameDirty = true;
    }
    public void ReplaceData(string keyName, System.Object prop)
    {
        //add key, value to game list
        GameData[keyName] = prop;
        gameDirty = true;
    }
    //Get info from game data
    public System.Object GetGameInfo(string keyName)
    {
        Debug.Log("getting game info received key: " + keyName);
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
