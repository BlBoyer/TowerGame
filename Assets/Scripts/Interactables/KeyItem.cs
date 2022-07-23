using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : InteractableObject
{
    //key shouldn't be added to inventory save, bc they are game data, not player data
    public GameObject masterKey;
    protected override void onInteract()
    {
        //set inactive
        this.gameObject.SetActive(false);
        masterKey.GetComponent<KeyBuilder>().addKey(gameObject);
    }
}
