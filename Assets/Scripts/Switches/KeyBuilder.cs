using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
//the master key script, this should acutally create the Key instance to save in the game data
//so, we give it  a key type, and make sure the key part matches that, or else we don't add it to the gameobject keys list, and we don't build this key with it
//this way we can build different keys at different times
//we can use the same build script and just build the master key based off game data
//need to figure out instantiating key prefabs based on whether we have those parts or not

public class KeyBuilder : MonoBehaviour
{
    //dependency
    private GameManager manager;
    //make the key from this string
    public string keyType;
    //we're going to select the prefab to instantiate using the vectors provided by the Key type
    public GameObject keyPrefab;
    //the door the prefabs will be used for, set the list for it on creation
    public GameObject exitDoor;
    //the list that we build from setting the keys inactive
    public List<GameObject> keys;
    private List<KeyValuePair<float[], bool>> keyData;
    public bool isBuilt = false;
    private static readonly JsonSerializerSettings _options = new() { NullValueHandling = NullValueHandling.Ignore };
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        //Get key from game data, if it exists
        var rawData = manager.GetGameInfo(keyType);
        if (rawData != null)
        {
            keyData = JsonConvert.DeserializeObject<List<KeyValuePair<float[], bool>>>(rawData.ToString(), _options);
        }
        //if the key doesn't exist in data create it and instantiate prefab keys
        Debug.Log("KeyData is " + (keyData));
        if (keyData == null) 
        {
            var thisKeyT = Type.GetType(keyType);
            //get data from the class, it's a property so we shouldn't need to instantiate this class********
            var newKey = (Key)Activator.CreateInstance(thisKeyT);
            Debug.Assert(newKey is Key, "The key subclass doesn't exist!");
            //instantiate new prefabs, since they don't exist
            foreach (var prefab in newKey.KeyFabs)
            {
                //intantiate prefab
                GameObject thisKeyPrefab = Instantiate(keyPrefab, prefab.Key, Quaternion.identity);
                thisKeyPrefab.GetComponent<KeyItem>().masterKey = gameObject;
                thisKeyPrefab.name = $"{keyType}{newKey.KeyFabs.IndexOf(prefab)}";
                //add prefab to door key list
                exitDoor.GetComponent<ExitScript>().keyParts.Add(thisKeyPrefab);
                //set the prefabs active status to the boolean
                keyPrefab.SetActive(prefab.Value);
                //if value is false add to gameobject list
                if (!prefab.Value)
                {
                    this.addKey(thisKeyPrefab);
                }
            }
            //create game data for new key
            var prefList = new List<KeyValuePair<float[], bool>>();
            //get key info and add to game data
            foreach (KeyValuePair<Vector3, bool> prefab in newKey.KeyFabs) 
            {
                //add to list
                float[] vectors = new float[3];
                vectors[0] = prefab.Key.x;
                vectors[1] = prefab.Key.y;
                vectors[2] = prefab.Key.z;
                prefList.Add(new(vectors, prefab.Value));
            }
            //create static data
            keyData = prefList;
            //create save data
            manager.AddData(newKey.Name, prefList);
        }
        else
        {
            //foreach prefab info in keyData, instantiate
            foreach (var prefab in keyData)
            {
                //make Vector3 here for cleanliness
                var vector = new Vector3(prefab.Key[0], prefab.Key[1], prefab.Key[2]);
                //set the masterkey field for this key part
                GameObject thisKeyPrefab = Instantiate(keyPrefab, vector, Quaternion.identity);
                thisKeyPrefab.GetComponent<KeyItem>().masterKey = gameObject;
                thisKeyPrefab.name = $"{keyType}{keyData.IndexOf(prefab)}";
                //add prefab to door key list
                exitDoor.GetComponent<ExitScript>().keyParts.Add(thisKeyPrefab);
                //set the prefabs active status to the boolean
                thisKeyPrefab.SetActive(prefab.Value);
                //if value is false add to gameobject list
                Debug.Assert(!prefab.Value, "prefab's value is true so it is being instantiated.");
                if (!prefab.Value) 
                {
                    //we can't call addKey here bc it changes te keyData values, we just need to add it to the list bc it's here already in keyData
                    keys.Add(thisKeyPrefab);
                }
            }
            //hopefully this runs after all the addKey methods are completed, rem, this is only at start if key exists
            if (keyData.All(pairs => !pairs.Value))
            {
                //we're going to glitch on this if we save before opening the door and want to do ceremonial door opening.
                isBuilt = true;
            }
        }
    }

    public void addKey(GameObject keyPart) 
    {
        Debug.Log($"Adding keypart {keyPart.name} to list.");
        //this adds the gameobject to the list
        keys.Add(keyPart);
        float[] vectors = new float[3] 
        { 
            keyPart.transform.position.x,
            keyPart.transform.position.y,
            keyPart.transform.position.z
        };
        var newPair = new KeyValuePair<float[], bool>(vectors, false);
        //need to get the currect list and replace one of the booleans at this keyPart transform position
        int i = keyData.IndexOf(keyData.Single(part => part.Key.SequenceEqual(vectors)));
        /*key data that is saveable changed to false value*/
        keyData[i] = newPair;
        manager.ReplaceData(keyType, keyData);
    }

}
