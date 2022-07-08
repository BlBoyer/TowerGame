using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void changeScene() 
    {
        SceneManager.LoadScene(_currentScene);
    }
}
