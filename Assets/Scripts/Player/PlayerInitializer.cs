using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject gameMenuPref;
    public GameObject invMenuPref;
    public GameObject playerStatPref;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //initialize game menu listener
        GameObject gameMenu = Instantiate(gameMenuPref, new Vector3(0,0,0), Quaternion.identity);
        gameMenu.name = "GameMenu";
        //initialize inventory menu listener
        GameObject invMenu = Instantiate(invMenuPref, new Vector3(0, 0, 0), Quaternion.identity);
        invMenu.name = "InvMenu";
        Debug.Assert(invMenu != null);
        //initilaize player status manager
    }
}
