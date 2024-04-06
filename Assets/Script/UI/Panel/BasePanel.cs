using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public string panelName;   //��øý��������
    public bool isOpened;       //�ж��Ƿ�Ϊ��״̬
    protected virtual void Awake()
    {
        panelName = gameObject.name;
        isOpened = false;
    }
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void OpenPanel(string name)//��UI����
    {
        this.name = name;
        SetActive(true);
    }

    public virtual void ClosePanel()//�ر�UI����
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
