using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
#nullable enable
public class ExitManager : MonoBehaviour
{
    bool isCompleted = false;
    private static string currentScene;
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
                    var startScene = (string)gameManager.GetGameInfo("Scene");
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
        //I don't know if the player shows on the prev scene, but maybe that doesn't matter
        //we can just set it's own position on awake tbh, rem, we still need to have player status somewhere persistent
        var posFloats = JsonConvert.DeserializeObject<float[]>(gameManager.GetGameInfo("Position").ToString());
        //need to auto-deserialize these field when getting the info, this is ridiculous
        var pos = new Vector3(posFloats[0], posFloats[1], posFloats[2]);
        GameObject player = Instantiate((GameObject)Resources.Load("Player"), pos, Quaternion.identity);
        player.name = "Player";
        Debug.Log("Does player exist?");
    }
}
