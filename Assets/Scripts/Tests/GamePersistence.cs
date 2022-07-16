using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GameManager._gameName);
        //looks like we're not getting a referencce to the game manager
        //GameManager.instance.AddData("testData", new List<string>(){ "one", "two", "three" });
        var data = GameObject.FindWithTag("Manager").GetComponent<GameManager>().GetGameInfo("testData");
        var expectedList = (List<string>)data;
        Debug.Log(expectedList[0]);
        
    }

}
