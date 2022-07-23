using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        //Get key from game data, if it exists
        keyData = (List<KeyValuePair<float[], bool>>)manager.GetGameInfo(keyType);
        //if the key doesn't exist in data create it and instantiate prefab keys
        Debug.Log(keyData == null); //works
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
                Debug.Log($"new key Item placed at {prefab.Key}");
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
            //deserailize saved data and create prefabs from that (for active values)
            var keyParts = JsonConvert.DeserializeObject<List<KeyValuePair<float[], bool>>>(keyData.ToString());
            //foreach prefab in key, instantiate
            foreach (var prefab in keyParts)
            {
                //make Vector3 here for cleanliness
                var vector = new Vector3(prefab.Key[0], prefab.Key[1], prefab.Key[2]);
                //set the masterkey field for this key part
                GameObject thisKeyPrefab = Instantiate(keyPrefab, vector, Quaternion.identity);
                thisKeyPrefab.GetComponent<KeyItem>().masterKey = gameObject;
                thisKeyPrefab.name = $"{keyType}{keyParts.IndexOf(prefab)}";
                //add prefab to door key list
                exitDoor.GetComponent<ExitScript>().keyParts.Add(thisKeyPrefab);
                //set the prefabs active status to the boolean
                keyPrefab.SetActive(prefab.Value);
                Debug.Log($"new key Item placed at {prefab.Key}");
                //if value is false add to gameobject list
                if (!prefab.Value) 
                {
                    this.addKey(thisKeyPrefab);
                }
            }
        }
    }

    public void addKey(GameObject keyPart) 
    {
        //this adds the gameobject to the list
        keys.Add(keyPart);
        //we need to use the ActiveStatus of this key and set the boolean of our save data, may from the item script
        float[] vectors = new float[3] 
        { 
            keyPart.transform.position.x,
            keyPart.transform.position.y,
            keyPart.transform.position.z
        };
        var newPair = new KeyValuePair<float[], bool>(vectors, false);
        //need to get the currect list and replace one of the booleans at this keyPart transform position
        int i = keyData.IndexOf(keyData.Single(part => part.Key.SequenceEqual(vectors)));
        //key data that is saveable changed
        keyData[i] = newPair;
        manager.ReplaceData(keyType, keyData);
    }

}
