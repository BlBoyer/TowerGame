using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#nullable enable
public class ExitManager : MonoBehaviour
{
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
    private void Awake()
    {
        //persist the manager through scene changes, it works, but for some reason I log a warning in the console
        DontDestroyOnLoad(gameObject);
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
        //this may need to be a task to make sure it happens first
        SceneManager.LoadSceneAsync(_currentScene);
        if (exitingScene != null && SceneManager.GetActiveScene().name == _currentScene) 
        {
            SceneManager.UnloadSceneAsync(exitingScene);
        }
    }
}
