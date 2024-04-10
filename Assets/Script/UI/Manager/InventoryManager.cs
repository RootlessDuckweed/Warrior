using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public InventorySO inventorySO;
    public PropListSO propListSO;
    public GameObject slotGrid;
    public Slot slotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
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
        //ʵ���������ڱ���UI�������ʾͼ
        foreach (var item in inventorySO.propsPakage)
        {
            Slot newProp = Instantiate(Instance.slotPrefab,Instance.slotGrid.transform.position,Quaternion.identity);
            newProp.gameObject.transform.SetParent(Instance.slotGrid.transform);
            //Bocchi:ǿ�����ű�Ϊ1:1:1
            newProp.gameObject.transform.localScale = Vector3.one;
            newProp.propSO = Instance.propListSO.GetPropSO(item.Key);
            //newProp.propSO.usePropEvent.AddListener(delegate{ RemoveProp(newProp.propSO); RefreshProp(); });
            newProp.slotImage.sprite = newProp.propSO.Image;
            newProp.slotNum.text = item.Value.ToString();
        }

    }

    //��ʾ���߾�������
    public static void DisplayDescription(PropSO newPropSO)
    {
        UIManager.Instance.OpenPanel("SlotDescriptionPanel");
        SlotDescriptionPanel slotDescriptionPanel = UIManager.Instance.panelDict["SlotDescriptionPanel"].gameObject.GetComponent<SlotDescriptionPanel>();
        slotDescriptionPanel.propSO = newPropSO;
        slotDescriptionPanel.titleText.text = newPropSO.propName;
        slotDescriptionPanel.descriptionText.text = newPropSO.description;
    }
    //ˢ�±���
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
}
