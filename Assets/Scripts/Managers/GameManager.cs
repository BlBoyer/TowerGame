using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#nullable enable
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance 
    {
        get 
        {
            if (_instance == null) 
            {
                GameObject manager = new GameObject("GameManager");
                manager.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    [System.NonSerialized] public static List<string> _scenes = new List<string>()
    {
        "begin",
        "VerticalSlice",
        "level2"
    };
    private static string _currentScene = _scenes[0];
    //prefabs
    public GameObject playerPrefab;
    void Awake()
    {
        _instance = this;
    }
    public void setScene(string name) 
    {
        if (_scenes.Contains(name))
        {
            _currentScene = _scenes[_scenes.IndexOf(name)];
        }
        else 
        {
            Debug.Log("Scene doesn't exist");
        }
    }
    public void changeScene(string? exitingScene) 
    {
        SceneManager.LoadScene(_currentScene);
        if (exitingScene != null) 
        {
            SceneManager.UnloadSceneAsync(exitingScene);
        }
    }
    //put scene logic here, start points etc, or make a child scene management object with the info there, and pass it the value of the current scene from there
    //called from exitScript instance
    private void initScene2() 
    {
        //init player
        //public static Object Instantiate(playerPrefab, Transform parent, bool instantiateInWorldSpace);
    }
}
