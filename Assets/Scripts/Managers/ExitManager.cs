using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
#nullable enable
public class ExitManager : MonoBehaviour
{
    //list game scenes here
    [System.NonSerialized] public static List<string> scenes = new List<string>()
    {
        "MainMenu",
        "VerticalSlice",
        "level2"
    };
    //we do need a reference to the current scene to save, it could be a method or an instance variable like this
    private static string currentScene = scenes[0];
    //prefabs
    public GameObject playerPrefab;
    private GameManager gameManager;
    private InventoryManager invManager;
    private void Awake()
    {
        //persist the manager through scene changes, it works, but for some reason I log a warning in the console
        DontDestroyOnLoad(gameObject);
        //wait for game manager to load data and load correct scene
    }
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        invManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        bool isCompleted = false;
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
        if (SceneManager.GetActiveScene().name == "MainMenu" && currentScene == "VerticalSlice")
        {
            Debug.Log("Changing scene in update");
            ChangeScene("MainMenu");
        }
    }
    public void SetScene(string name) 
    {
        if (scenes.Contains(name))
        {
            currentScene = scenes[scenes.IndexOf(name)];
        }
        else 
        {
            Debug.LogError("Scene doesn't exist");
        }
    }
    public void ChangeScene(string exitingScene) 
    {

        Debug.Log(currentScene);
        SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Single);
        //Debug.Log(SceneManager.GetActiveScene());
        //if (exitingScene != null)// && SceneManager.GetActiveScene().name == currentScene) 
        //{
        //    SceneManager.UnloadSceneAsync(exitingScene);
        //}
    }
}
