using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using UnityEngine;


public static class SaveInteractableObserver 
{
    private static readonly Dictionary<string,IInteractable> saveObservers = new(); //观察者列表
    private static Dictionary<string,string> loadFromJson; // 从json文件读取的数据
    
    //添加观察者
    public static  void AddObserver(string GUID,IInteractable obj)
    {
        saveObservers.Add(GUID, obj);
    }
    
    //移除观察者
    public static void RemoveObserver(string GUID) 
    {
        saveObservers.Remove(GUID); 
    }

    //从json文件中读取可互动对象的数据
    public static void LoadInteractableObj()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/saveInteractableJson.json"); //读取json文件
        //Debug.Log(json);    
        loadFromJson=JsonConvert.DeserializeObject<Dictionary<string, string>>(json); //反序列化
        foreach (var item in loadFromJson)
        {
            if (saveObservers.ContainsKey(item.Key)) //如果在当前场景中注册的观察者中 存在我们保存的的key值
            {
                //那么当前的观察者就执行反序列化操作(传入保存的json数据 进行反序列) 进行读取
                saveObservers[item.Key].DeserializeFromJsonData(loadFromJson[item.Key]);
            }
        }
        
    }

    //保存可互动对象的json数据到json文件中
    public static void SaveInteractableToJson()
    {
        //预备需要保存的数据 第一个string是可互动对象的GUID 第二个string是可互动对象的json数据
        Dictionary<string, string> save = new Dictionary<string, string>(); 
        foreach (var item in saveObservers)
        {
            save[item.Key]=item.Value.GetNeedToCovertJsonData(); //添加需要保存的数据到预备的字典save中
            //Debug.Log(item.Key);
        }

        var saveJson = JsonConvert.SerializeObject(save); //将整个save字典序列化
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        File.WriteAllText(Application.streamingAssetsPath + "/saveInteractableJson.json", saveJson); //保存文件
    }
}
