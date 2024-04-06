using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // 预制件缓存字典
    private Dictionary<string, GameObject> prefabDict;
    //UI界面的Canvas坐标
    private Transform canvas_Root;
    // 已打开界面的缓存字典
    public Dictionary<string, BasePanel> panelDict;

    private PlayerInput inputActions;

    // Start is called before the first frame update
    protected override void Awake()
    {
        //初始化字典集
        base.Awake();
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
        canvas_Root = GameObject.Find("Canvas").transform;
        inputActions = new PlayerInput();
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += OnPause;
        inputActions.UI.OpenInventory.performed += OnOpenInventory;
    }

    //TODO:暂时放在这里,之后单独分离到专门的控制类中
    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OpenPanel("PausePanel");
    }

    //打开背包
    private void OnOpenInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OpenPanel("InventoryPanel");
    }

    //打开界面
    public void OpenPanel(string panelName)
    {
        
        BasePanel panel = null;
        //判断该界面是否被打开
        if(panelDict.TryGetValue(panelName,out panel))
        {
            Debug.Log("界面已打开"+panelName);
            return;
        }
        // 使用缓存预制件,如果没有该缓存件则通过Resources从路径中获取
        GameObject panelPrefab = null;
        if(!prefabDict.TryGetValue(panelName, out panelPrefab))
        {
            string realPath = "Prefab/Panel/" + panelName;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDict.Add(panelName, panelPrefab);
        }
        //打开界面
        GameObject panelObject = Instantiate(panelPrefab, canvas_Root, false);
        panel=panelObject.GetComponent<BasePanel>();
        panelDict.Add(panelName, panel);
        panel.OpenPanel(panelName);
    }

    //关闭界面
    public void ClosePanel(string panelName)
    {
        BasePanel panel = null;
        if(!panelDict.TryGetValue(panelName,out panel))
        {
            Debug.Log("界面未打开" + panelName);
            return;
        }
        panel.ClosePanel();
    }
}
