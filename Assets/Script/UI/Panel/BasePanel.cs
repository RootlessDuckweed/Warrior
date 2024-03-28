using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public string panelName;   //获得该界面的名字
    public bool isOpened;       //判断是否为打开状态
    protected virtual void Awake()
    {
        panelName = gameObject.name;
        isOpened = false;
    }
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void OpenPanel(string name)//打开UI界面
    {
        this.name = name;
        SetActive(true);
    }

    public virtual void ClosePanel()//关闭UI界面
    {
        isOpened = false;
        SetActive(false);
        if (UIManager.Instance.panelDict.ContainsKey(name))
        {
            UIManager.Instance.panelDict.Remove(name);
        }
        Destroy(gameObject);
    }
}
