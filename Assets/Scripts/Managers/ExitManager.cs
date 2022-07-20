using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
#nullable enable
public class ExitManager : MonoBehaviour
{
    //list game scenes here
    /*[System.NonSerialized] public static List<string> scenes = new List<string>()
    {
        "MainMenu",
        "VerticalSlice",
        "level2"
    };*/
    bool isCompleted = false;
    private static string currentScene;
    public GameObject playerPrefab;
    private GameManager gameManager;
    private InventoryManager invManager;
    List<string> scenes = new();
    private void Awake()
    {
        //persist the manager through scene changes, it works, but for some reason I log a warning in the console
        DontDestroyOnLoad(gameObject);
        scenes = EditorBuildSettings.scenes.Select(scene => Path.GetFileNameWithoutExtension(scene.path)).ToList<string>();
    }
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        invManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        Task.Run(() => 
        {
            while (isCompleted == false) 
            {
                Debug.Log("exit controller task running");
                if (invManager.completed) 
                {
                    //get scene data
                    Debug.Log("getting scene");
                    var startScene = (string)gameManager.GetGameInfo("scene");
                    //I might put this outside the loop
                    SetScene(startScene);
                    //ChangeScene("MainMenu");
                    isCompleted = true;
                    //break;
                }
            }
        });
    }
    void Update() 
    {
        if (isCompleted = true && SceneManager.GetActiveScene().name == "MainMenu" && currentScene == "VerticalSlice")
        {
            Debug.Log("Changing scene in update");
            ChangeScene("MainMenu");
        }
    }
    public void SetScene(string name) 
    {
        if (scenes.Contains(name))
        {
            currentScene = name;
        }
        else 
        {
            Debug.LogError("Scene doesn't exist");
        }
    }
    public void ChangeScene(string exitingScene) 
    {

        Debug.Log($"changing scene to: {currentScene}");
        //I changed from Async, to stop multiple scene changes on update
        SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
    }
}
