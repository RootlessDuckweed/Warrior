using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using UnityEngine;


public static class SaveInteractableObserver 
{
    private static readonly Dictionary<string,IInteractable> saveObservers = new(); //�۲����б�
    private static Dictionary<string,string> loadFromJson; // ��json�ļ���ȡ������
    
    //��ӹ۲���
    public static  void AddObserver(string GUID,IInteractable obj)
    {
        saveObservers.Add(GUID, obj);
    }
    
    //�Ƴ��۲���
    public static void RemoveObserver(string GUID) 
    {
        saveObservers.Remove(GUID); 
    }

    //��json�ļ��ж�ȡ�ɻ������������
    public static void LoadInteractableObj()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/saveInteractableJson.json"); //��ȡjson�ļ�
        //Debug.Log(json);    
        loadFromJson=JsonConvert.DeserializeObject<Dictionary<string, string>>(json); //�����л�
        foreach (var item in loadFromJson)
        {
            if (saveObservers.ContainsKey(item.Key)) //����ڵ�ǰ������ע��Ĺ۲����� �������Ǳ���ĵ�keyֵ
            {
                //��ô��ǰ�Ĺ۲��߾�ִ�з����л�����(���뱣���json���� ���з�����) ���ж�ȡ
                saveObservers[item.Key].DeserializeFromJsonData(loadFromJson[item.Key]);
            }
        }
        
    }

    //����ɻ��������json���ݵ�json�ļ���
    public static void SaveInteractableToJson()
    {
        //Ԥ����Ҫ��������� ��һ��string�ǿɻ��������GUID �ڶ���string�ǿɻ��������json����
        Dictionary<string, string> save = new Dictionary<string, string>(); 
        foreach (var item in saveObservers)
        {
            save[item.Key]=item.Value.GetNeedToCovertJsonData(); //�����Ҫ��������ݵ�Ԥ�����ֵ�save��
            //Debug.Log(item.Key);
        }

        var saveJson = JsonConvert.SerializeObject(save); //������save�ֵ����л�
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        File.WriteAllText(Application.streamingAssetsPath + "/saveInteractableJson.json", saveJson); //�����ļ�
    }
}
