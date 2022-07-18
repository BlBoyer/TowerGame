using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    // Test gameData in new scene
    void Start()
    {
        var data = GameObject.FindWithTag("Manager").GetComponent<GameManager>().GetGameInfo("init");
        /*var expectedList = (List<string>)data;
        Debug.Log(expectedList[0]);*/
        Debug.Log(data);
        
    }

}
