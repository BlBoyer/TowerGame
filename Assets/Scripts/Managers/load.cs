using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is unnecessary, we will use the game menu to load the fist scene, and when loading a savedGame we will load the saved scene.

public class load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instantiate<GameManager>(GameManager.instance);
        ExitManager.instance.setScene("begin");
        ExitManager.instance.changeScene(null);
    }
}
