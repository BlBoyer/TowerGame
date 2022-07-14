using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SelectGame();
        GameObject manager = new GameObject("GameManager") { tag = "Manager" };
        manager.AddComponent<GameManager>();
        //GameManager._gameName = "myGame2";
        //Debug.Log(GameManager._gameName);
        manager.GetComponent<GameManager>().AddData("testData", new List<string>() { "one", "two", "three" });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SelectGame()
    {
        GameObject manager = new GameObject("GameManager");
        manager.AddComponent<GameManager>();
        //GameManager._gameName = "myGame2";
    }
}