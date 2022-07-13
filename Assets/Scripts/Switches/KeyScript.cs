using UnityEngine;

/*
 * we will make a pickupable script that does this for any object that we can pick up and have the text whatever we want.
 * then we will make a notification variable on the pickup script, and call the keyBuilder add method on pickup here, maybe with an override method or this
 */

public class KeyScript : MonoBehaviour
{
    public GameObject masterKey;
    private void Update()
    {
        if (GetComponent<KeyItem>().dirty)
        {
            masterKey.GetComponent<KeyBuilder>().addKey(gameObject);
        }
    }
}
