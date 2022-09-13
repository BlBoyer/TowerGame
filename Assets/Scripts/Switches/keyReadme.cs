/**how the key system works**/
//WE SHOULD JUST HAVE THEY KEYS AND BUILD THEM AS WE PICK UP THE KEY OBJECTS AND SAVE THE WITH THEIR PROPERTIES BEING SET THEN BUILD THEM IN THE MASTER KEY SCRIPT
//NO KEY PARTS NEEDED,MASTER KEY OBJ HOLDS KEY PREFABS, we don't need to say we have the master key, we could just have a boolean saying it's built
//// Instantiate at position (0, 0, 0) and zero rotation.
//Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
///
/*
OR do we just instantiate all keys, but set the ones to inactive and add them to the object list?
 */
/*
 * 1. we build the key prefabs from game data, and pass it the master key object of the door it goes to to build the master key
 * 2. if the key has been built no key prefabs will be instantiated, and the master key will be in saved data
 * 3. in the exit script, if the master key list is built, the door unlocks, and also saves the master key data
 * 4. this method will run the unlock animation by have the prefab list made up for the door
 * 5. if the master key variable is saved, the animaiton no longer runs
 */