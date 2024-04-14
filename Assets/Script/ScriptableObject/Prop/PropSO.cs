using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
// ����SO��
[CreateAssetMenu(menuName = "Prop/PropSO")]
[Serializable]
public class PropSO : ScriptableObject,IProp
{
    public string propName; //����
    public string description; //����
    public Sprite Image; //��ʾ��ͼƬ
    public UnityEvent usePropEvent; //ʹ���˵���֮�����ǻᴥ��ʲô�¼�
    public bool useable;
    public void UseProp()
    {
        usePropEvent?.Invoke();
    }

}
