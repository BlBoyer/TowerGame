using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//abstract class for items that are saved as game data instead of playerdata
public abstract record GameItem
{
    public abstract string Name { get; protected set; }
    public abstract string Description { get; protected set; }
}
/**Keys**/
/*we've given the max amount of keyparts as 4, I did this as properties in order to check if the keypart was the same as the property*/
public abstract record Key : GameItem
{
    //prefab should have a name/path right and a Vector3 transfrom to instantiate?
    //public abstract Dictionary<string, Vector2> Prefabs { get; protected set; }
    public abstract List<KeyValuePair<Vector3, bool>> KeyFabs { get; protected set; }
}
//In our master key builder, we add keyParts to inventory
public record VerticalSliceKey : Key
{
    public override string Name { get; protected set; } = "VerticalSliceKey";
    public override string Description { get; protected set; } = "A Key to open Vertcal Slice Door.";
    //get whether the prefab should be active or not and use the active sef to build the gameobject list
    public override List<KeyValuePair<Vector3, bool>> KeyFabs { get; protected set; } = new List<KeyValuePair<Vector3, bool>>()
    {
        new KeyValuePair<Vector3, bool>(new Vector3(-2,0,0), true),
        new KeyValuePair<Vector3, bool>(new Vector3(-4,1,0), true)
    };
}
