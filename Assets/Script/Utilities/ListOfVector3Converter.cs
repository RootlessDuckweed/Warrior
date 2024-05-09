using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class ListOfVector3Converter : JsonConverter<List<Vector3>>
{
    public override void WriteJson(JsonWriter writer, List<Vector3> value, JsonSerializer serializer)
    {
        var array = new JArray();
        foreach (Vector3 vector in value)
        {
            var obj = new JObject();
            obj.Add("x", vector.x);
            obj.Add("y", vector.y);
            obj.Add("z", vector.z);
            array.Add(obj);
        }
        array.WriteTo(writer);
    }

    public override List<Vector3> ReadJson(JsonReader reader, Type objectType, List<Vector3> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JArray array = JArray.Load(reader);
        var list = new List<Vector3>();
        foreach (JObject obj in array)
        {
            float x = (float)obj["x"];
            float y = (float)obj["y"];
            float z = (float)obj["z"];
            list.Add(new Vector3(x, y, z));
        }
        return list;
    }

    public override bool CanRead => true;

    public override bool CanWrite => true;
}