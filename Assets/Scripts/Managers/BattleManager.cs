using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager _instance;
    public static BattleManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = new GameObject("BattleManager");
                manager.AddComponent<BattleManager>();
            }
            return _instance;
        }
    }
    private string _nextScene;
    void Start()
    {
        //get player object
        //get enemy object
    }
    private void FixedUpdate()
    {
        //move characters
        //play animations
    }
    // Update is called once per frame
    void Update()
    {
        //process inputs
        //calculate outcome
        //assign animations
    }
    private void ProcessInputs() 
    {
        //take variables from ui
        //list possible animations
    }
    private void Calculate() 
    {
        //if (battleisover){_nextScene = "VerticalSlice", this.End()}
    }
    private void End() 
    {
        GameManager.instance.setScene(_nextScene);
        GameManager.instance.changeScene();
    }
}
