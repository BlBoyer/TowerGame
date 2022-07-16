using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PlayerItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(PlayerItem).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        //we need our string here from the object name
        Type subType = Type.GetType((string)obj["name"]);
        var item = (PlayerItem)Activator.CreateInstance(subType);
        serializer.Populate(obj.CreateReader(), item);
        return item;
    }

    public override bool CanWrite
    {
        get { return false; }
    }
    public override void WriteJson(JsonWriter writer,
        object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
