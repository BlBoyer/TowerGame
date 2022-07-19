using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#nullable enable

public class MainMenu : MonoBehaviour
{
    //REM this is a new scene
    //Declarations
    public Button continueBtn;
    public Button newGameBtn;
    public Button createButton;
    public TMP_Dropdown gameList;
    public TMP_InputField nameSetter;
    private string nameInput;
    private string copy_dir;
    private string? dir_path;
    private string? savePath;
    private string? gameSavePath;
    [System.NonSerialized] public GameObject manager;
    [System.NonSerialized] public GameObject exitController;
    [System.NonSerialized] public GameObject inventory;
    private void Awake()
    {
        copy_dir = $"{Application.dataPath}/StartupFiles";
    }
    private void Start()
    {
        //show active games
       continueBtn.onClick.AddListener(() => 
        {
            nameSetter.gameObject.SetActive(false);
            gameList.gameObject.SetActive(true); 
        });
        //show input field
        newGameBtn.onClick.AddListener(() => {
            gameList.gameObject.SetActive(false);
            nameSetter.gameObject.SetActive(true);
            createButton.onClick.AddListener(() => CreateGame());
        });
        nameSetter.onValueChanged.AddListener((value) => 
        {
            nameInput = value;
            Debug.Log(value);
        });
    }
    private void SetPaths(string dir) 
    {
        dir_path = $"{Application.dataPath}/{dir}";
        savePath = $"{dir_path}/save.JSON";
        gameSavePath = $"{dir_path}/globals.JSON";
    }
    void LoadGame()
    {
        //set data paths
        GameManager.savePath = savePath;
        GameManager.gameSavePath = gameSavePath;
        //create managers
        manager = new("GameManager") { tag = "Manager" };
        manager.AddComponent<GameManager>();
        //wait for data to come up, then load scene, then load inventory
        exitController = new("ExitManager") { tag = "ExitController" };
        exitController.AddComponent<ExitManager>();
        inventory = new("InventoryManager") { tag = "Inventory" };
        inventory.AddComponent<InventoryManager>();
    }
    void CreateGame()
    {
        //set gameName to input
        SetPaths(nameInput);
        if (!Directory.Exists(dir_path))
        {
            Directory.CreateDirectory(dir_path);
            //we should copy an existing JSON to playerData
            File.Copy($"{copy_dir}/save.JSON", savePath);
            Debug.Assert(File.Exists(savePath));
            File.Copy($"{copy_dir}/globals.JSON", gameSavePath);
            Debug.Assert(File.Exists(gameSavePath));
            //copy files instead of creating empties or we'll need a whole section of create logic in game manager
        }
        else 
        {
            Debug.Log("This game already exists!");
        }
        LoadGame();
    }
    void SelectGame()
    {
        //set game name
        //get data path

    }
}