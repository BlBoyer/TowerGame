using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#nullable enable
public class ExitManager : MonoBehaviour
{
    private static ExitManager _instance;
    public static ExitManager instance 
    {
        get 
        {
            if (_instance == null) 
            {
                GameObject manager = new GameObject("ExitManager");
                manager.AddComponent<ExitManager>();
            }
            return _instance;
        }
    }
    //list game scenes here
    [System.NonSerialized] public static List<string> _scenes = new List<string>()
    {
        "begin",
        "VerticalSlice",
        "level2"
    };
    //we do need a reference to the current scene to save, it could be a method or an instance variable like this
    private static string _currentScene = _scenes[0];
    //prefabs
    public GameObject playerPrefab;
    void Awake()
    {
        _instance = this;
        //persist the manager through scene changes
        DontDestroyOnLoad(_instance.gameObject);
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
}
