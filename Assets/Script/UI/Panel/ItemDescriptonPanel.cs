using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptonPanel : BasePanel
{
    Button confirmButton;
    public GameObject itemGrid;
    public SlotItem itemPrefab;
    private void Start()
    {
        confirmButton = transform.Find("ConfirmButton").GetComponent<Button>();
        confirmButton.onClick.AddListener(OnConfirm);
    }
    //Bocchi:显示获取物品的列表
    public void GeneratePanel(List<Pair<PropSO,int>> itemList)
    {
        if(itemList.Count != 0) 
        {
            foreach (var item in itemList)
            {
                SlotItem newItem = Instantiate(itemPrefab,itemGrid.transform.position,Quaternion.identity);
                newItem.gameObject.transform.SetParent(itemGrid.transform);
                newItem.slotImage.sprite = item.key.Image;
                newItem.slotNum.text = item.value.ToString();
                newItem.slotName.text = item.key.propName;
            }
        }
    }

    public void ClearPanel()
    {
        for(int i=0;i<itemGrid.transform.childCount;i++) 
        {
            Destroy(itemGrid.transform.GetChild(i).gameObject);
        }
    }

    void OnConfirm()
    {
        ClearPanel();
        UIManager.Instance.ClosePanel(gameObject.name);
    }

    private void OnDisable()
    {
        confirmButton.onClick.RemoveAllListeners();
    }
}
