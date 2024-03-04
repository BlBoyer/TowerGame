using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
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
    public Button startButton;
    public TMP_Dropdown gameList;
    public TMP_InputField nameSetter;
    public List<TMP_Dropdown.OptionData> optionDataList;
    public TMP_Text errorText;
    private string nameInput;
    private string copy_dir;
    private string? dir_path;
    private string? savePath;
    private string? gameSavePath;
    [System.NonSerialized] public GameObject manager;
    [System.NonSerialized] public GameObject exitController;
    [System.NonSerialized] public GameObject inventory;
    /**As a reminder, you can add sprites to dropdown options to display text and sprites as well, good to know for avatars, or inventory items**/
    private void Awake()
    {
        copy_dir = $"{Application.dataPath}/StartupFiles";
        dir_path = $"{Application.dataPath}/ActiveGames/";
        //check if directory is empty
        var games = Directory.GetDirectories(dir_path);
        if (games == null || games.Length == 0) 
        {
            Debug.Log("No present Games.");
        } 
        else 
        {
            foreach (var dir in games)
            {
                var dirName = new DirectoryInfo(dir).Name;
                optionDataList.Add(new TMP_Dropdown.OptionData(dirName));
            }
            //populate game list, list of optiondata so we need new OptionData for each in games directory
            gameList.AddOptions(optionDataList);
        }
}
    private void Start()
    {
        //show active games
       continueBtn.onClick.AddListener(() => 
        {
            nameSetter.gameObject.SetActive(false);
            createButton.gameObject.SetActive(false);
            gameList.gameObject.SetActive(true);
            startButton.onClick.AddListener(() => 
            {
                var ind = gameList.value;
                SetPaths(gameList.options[ind].text);
                LoadGame();
            });
        });
        //show input field
        newGameBtn.onClick.AddListener(() => {
            gameList.gameObject.SetActive(false);
            nameSetter.gameObject.SetActive(true);
            createButton.onClick.AddListener(() =>
            {
                CreateGame();
            });
            nameSetter.onValueChanged.AddListener((value) =>
            {
                //validatation regex for folder name
                var conform = new Regex("^[A-Za-z0-9]*$");
                //check if name is in the gameList
                if (optionDataList.Any(option => option.text == value))
                {
                    //show a validation error above box saying the game already exists, or if game name is other than /w text
                    Debug.Log("This game already exists!");
                    //edit text field
                    errorText.color = new Color(115, 0, 0);
                    errorText.text = "A game with the same name already exists!";
                    createButton.gameObject.SetActive(false);
                }
                else if (conform.IsMatch(value) && value.Length < 20)
                {
                    errorText.color = Color.blue;
                    errorText.text = "All good.";
                    createButton.gameObject.SetActive(true);
                    nameInput = nameSetter.text;
                }
                else
                {
                    //show error
                    Debug.LogError("name must be a valid folder name and less than 20 characters!");
                    //edit text field
                    errorText.color = new Color(115,0,0);
                    errorText.text = "Game name must be alphanumeric.";
                    createButton.gameObject.SetActive(false);
                }
            });
        });
    }
    private void SetPaths(string dir) 
    {
        dir_path += dir;
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
            //we should copy an existing JSON to playerData instead of creating empties or we'll need a whole section of create logic in game manager
            File.Copy($"{copy_dir}/save.JSON", savePath);
            Debug.Assert(File.Exists(savePath));
            File.Copy($"{copy_dir}/globals.JSON", gameSavePath);
            Debug.Assert(File.Exists(gameSavePath));
            LoadGame();
        }
    }
}
