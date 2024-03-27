using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
// ����SO��
[CreateAssetMenu(menuName = "Prop/PropSO")]
public class PropSO : ScriptableObject,IProp
{
    public string propName; //����
    public string description; //����
    public Image Image; //��ʾ��ͼƬ
    public UnityEvent usePropEvent; //ʹ���˵���֮�����ǻᴥ��ʲô�¼�
    public void UseProp()
    {
        usePropEvent?.Invoke();
    }

}
