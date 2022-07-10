using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBuilder : MonoBehaviour
{

    //we want to attach this class to a masterkey, and create the child objects that the key needs to be complete

    //we're going to place the key pieces in the scene, and add the to this keys list, we will also add the keys to the exit door, and make sure both Lists are complete
    public List<GameObject> keys;

    //make an addKey method that we can call or put the script in game manager
    public void addKey(GameObject keyPart) 
    {
        keys.Add(keyPart);
    }

}
