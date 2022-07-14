using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    //get keys for unlocking this door
    //Make sure that all keys are attached to the corresponding exitDoor if assertion fails in console log
    public List<GameObject> keyParts;

    //get Master key for this dungeon
    public GameObject masterKey;
    //add the Exit Manager through UI
    //public GameObject ExitManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        List<GameObject> comparedKeys = keyParts.Intersect(masterKey.GetComponent<KeyBuilder>().keys).ToList();
        Debug.Assert(comparedKeys.SequenceEqual(keyParts), $"List didn't match, returned type: {comparedKeys.GetType()}");
        //check if the level's master key contains both keys, they will get added by the player picking them up, so the scene should contain the master key as a prop 
        if (collision.gameObject.CompareTag("Player") && comparedKeys.SequenceEqual(keyParts))
        {
            Debug.Log("we've collided with the exit door and our keys match up");

            //logic for saving keys

            //we could use the UI for this object for less referencing
            var manager = GameObject.FindWithTag("ExitController").GetComponent<ExitManager>();
            manager.setScene("level2");
            manager.changeScene("VerticalSlice");
        }
    }
}
