using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : InteractableObject
{
    //key shouldn't be added to inventory, bc they are game data, not player data
    public GameObject masterKey;

    protected override void onInteract()
    {
        //set inactive
        this.gameObject.SetActive(false);
        //build key
        masterKey.GetComponent<KeyBuilder>().addKey(gameObject);
    }
}
