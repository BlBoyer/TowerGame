using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//when the keys objects are in the master key builder, then we check against the master key object list, for animations/first-time open door
//when the door is unlocked the first time, we add the master key to game data, and then check for that in our inventory for opening doors
public class ExitScript : MonoBehaviour
{
    //get keys for unlocking this door, I like using the gameobjects because we can see what keys we're adding in thhe editor
    //Make sure that all keys are attached to the corresponding exitDoor if assertion fails in console log
    public List<GameObject> keyParts;

    //get Master key for this dungeon, use the scripts key type to check if in inventory
    public GameObject masterKey;
    private ExitManager manager;
    private void Start()
    {
        manager = GameObject.FindWithTag("ExitController").GetComponent<ExitManager>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        List<GameObject> comparedKeys = keyParts.Intersect(masterKey.GetComponent<KeyBuilder>().keys).ToList();
        Debug.Assert(comparedKeys.SequenceEqual(keyParts), $"List didn't match, returned type: {comparedKeys.GetType()}");
        //if gameobjects are in list and game data doesn't have door key
        if (collision.gameObject.CompareTag("Player") && comparedKeys.SequenceEqual(keyParts) && !masterKey.GetComponent<KeyBuilder>().isBuilt)
        {
            Debug.Log("we've collided with the exit door and our keys match up");
            //run animations
            manager.SetScene("level2");
            manager.ChangeScene("VerticalSlice");
            //we don't run through this again
            masterKey.GetComponent<KeyBuilder>().isBuilt = true;
        }
        else if (masterKey.GetComponent<KeyBuilder>().isBuilt)
        {
            manager.SetScene("level2");
            manager.ChangeScene("VerticalSlice");
        }
    }
}
