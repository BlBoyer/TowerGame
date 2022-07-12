using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : InteractableObject
{
    public GameObject masterKey;

    protected override void onInteract()
    {
        //set inactive
        this.gameObject.SetActive(false);
        //build key
        masterKey.GetComponent<KeyBuilder>().addKey(gameObject);
    }
}
