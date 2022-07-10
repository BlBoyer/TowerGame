using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    //get keys for unlocking this door
    public List<GameObject> keyParts;

    //get Master key for this dungeon
    public GameObject masterKey;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Assert(keyParts.SequenceEqual(masterKey.GetComponent<KeyBuilder>().keys));
        //check if the level's master key contains both keys, they will get added by the player picking them up, so the scene should contain the master key as a prop 
        if (collision.gameObject.CompareTag("Player") && keyParts.SequenceEqual(masterKey.GetComponent<KeyBuilder>().keys))
        {
            Debug.Log("we've collided with the exit door and our keys match up");
            GameManager.instance.setScene("level2");
            GameManager.instance.changeScene();
        }
    }
}
