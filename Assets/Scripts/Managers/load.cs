using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
