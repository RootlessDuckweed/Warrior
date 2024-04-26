using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public InventorySO inventorySO;
    public PropListSO propListSO;
    public GameObject slotGrid;
    public Slot slotPrefab;
    string saveFolder;

    // Start is called before the first frame update
    void Start()
    {
        saveFolder = Application.streamingAssetsPath;
    }
    public void AddProp(PropSO propSO,int count)
    {
        inventorySO.AddProp(propSO,count);
    }

    public void RemoveProp(PropSO propSO) 
    { 
        inventorySO.RemoveProp(propSO);
    }
    
    public void CreateProp(GameObject slotGrid)
    {
        Instance.slotGrid = slotGrid;
        //实例化道具在背包UI界面的显示图
        foreach (var item in inventorySO.propsPakage)
        {
            Slot newProp = Instantiate(Instance.slotPrefab,Instance.slotGrid.transform.position,Quaternion.identity);
            newProp.gameObject.transform.SetParent(Instance.slotGrid.transform);
            //Bocchi:强制缩放比为1:1:1
            newProp.gameObject.transform.localScale = Vector3.one;
            newProp.propSO = Instance.propListSO.GetPropSO(item.Key);
            //newProp.propSO.usePropEvent.AddListener(delegate{ RemoveProp(newProp.propSO); RefreshProp(); });
            newProp.slotImage.sprite = newProp.propSO.Image;
            newProp.gameObject.transform.localScale = new(1, 1, 1);
            newProp.slotNum.text = item.Value.ToString();
        }

    }

    //显示道具具体描述
    public static void DisplayDescription(PropSO newPropSO)
    {
        UIManager.Instance.OpenPanel("SlotDescriptionPanel");
        SlotDescriptionPanel slotDescriptionPanel = UIManager.Instance.panelDict["SlotDescriptionPanel"].gameObject.GetComponent<SlotDescriptionPanel>();
        slotDescriptionPanel.propSO = newPropSO;
        slotDescriptionPanel.titleText.text = newPropSO.propName;
        slotDescriptionPanel.descriptionText.text = newPropSO.description;
    }
    //刷新背包
    public void RefreshProp()
    {
        for (int i=0;i<Instance.slotGrid.transform.childCount;i++)
        {
            if (Instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(Instance.slotGrid.transform.GetChild(i).gameObject); ;
        }
        CreateProp(Instance.slotGrid);
    }

    //Bocchi:保存背包数据
    public void SaveInventoryData()
    {
        var resultPath = saveFolder + "/inventoryData.json";
        var jsonData = JsonConvert.SerializeObject(inventorySO.propsPakage);
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        File.WriteAllText(resultPath, jsonData);
    }

    //Bocchi:加载背包数据
    public void LoadInventoryData()
    {
        var resultPath = saveFolder + "/inventoryData.json";
        if (!File.Exists(resultPath))
        {
            return;
        }
        var stringData=File.ReadAllText(resultPath);
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, int>>(stringData);
        inventorySO.propsPakage = jsonData;
    }

    public void ClearInventoryData()
    {
        var resultPath = saveFolder + "/inventoryData.json";
        inventorySO.propsPakage.Clear();
        File.Delete(resultPath);
    }
}
