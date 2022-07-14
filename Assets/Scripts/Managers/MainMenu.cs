using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SelectGame();
        //instantiate managers, or add them to the scenes, IDC!
        GameObject manager = new GameObject("GameManager") { tag = "Manager" };
        manager.AddComponent<GameManager>();
        //GameManager._gameName = "myGame2";
        //Debug.Log(GameManager._gameName);
        manager.GetComponent<GameManager>().AddData("testData", new List<string>() { "one", "two", "three" });
        GameObject exitManager = new GameObject("ExitManager") { tag = "ExitController" };
        exitManager.AddComponent<ExitManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*void SelectGame() //the function we will run on interacting with the menu
    {
        GameObject manager = new GameObject("GameManager");
        manager.AddComponent<GameManager>();
        //GameManager._gameName = "myGame2";
    }*/
}