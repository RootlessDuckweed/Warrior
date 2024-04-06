using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // Ԥ�Ƽ������ֵ�
    private Dictionary<string, GameObject> prefabDict;
    //UI�����Canvas����
    private Transform canvas_Root;
    // �Ѵ򿪽���Ļ����ֵ�
    public Dictionary<string, BasePanel> panelDict;

    private PlayerInput inputActions;

    // Start is called before the first frame update
    protected override void Awake()
    {
        //��ʼ���ֵ伯
        base.Awake();
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
        canvas_Root = GameObject.Find("Canvas").transform;
        inputActions = new PlayerInput();
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += OnPause;
        inputActions.UI.OpenInventory.performed += OnOpenInventory;
    }

    //TODO:��ʱ��������,֮�󵥶����뵽ר�ŵĿ�������
    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OpenPanel("PausePanel");
    }

    //�򿪱���
    private void OnOpenInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OpenPanel("InventoryPanel");
    }

    //�򿪽���
    public void OpenPanel(string panelName)
    {
        
        BasePanel panel = null;
        //�жϸý����Ƿ񱻴�
        if(panelDict.TryGetValue(panelName,out panel))
        {
            Debug.Log("�����Ѵ�"+panelName);
            return;
        }
        // ʹ�û���Ԥ�Ƽ�,���û�иû������ͨ��Resources��·���л�ȡ
        GameObject panelPrefab = null;
        if(!prefabDict.TryGetValue(panelName, out panelPrefab))
        {
            string realPath = "Prefab/Panel/" + panelName;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDict.Add(panelName, panelPrefab);
        }
        //�򿪽���
        GameObject panelObject = Instantiate(panelPrefab, canvas_Root, false);
        panel=panelObject.GetComponent<BasePanel>();
        panelDict.Add(panelName, panel);
        panel.OpenPanel(panelName);
    }

    //�رս���
    public void ClosePanel(string panelName)
    {
        BasePanel panel = null;
        if(!panelDict.TryGetValue(panelName,out panel))
        {
            Debug.Log("����δ��" + panelName);
            return;
        }
        panel.ClosePanel();
    }
}
