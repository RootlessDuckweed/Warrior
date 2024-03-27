using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
// 道具SO类
[CreateAssetMenu(menuName = "Prop/PropSO")]
public class PropSO : ScriptableObject,IProp
{
    public string propName; //名字
    public string description; //描述
    public Image Image; //显示的图片
    public UnityEvent usePropEvent; //使用了道具之后，我们会触发什么事件
    public void UseProp()
    {
        usePropEvent?.Invoke();
    }

}
