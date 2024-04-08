using System;
using System.Collections.Generic;
using UnityEngine;

//将道具的数据存储在列表中
[CreateAssetMenu(fileName ="PropListSO",menuName ="Prop/PropListSO")]
[Serializable]
public class PropListSO : ScriptableObject
{
    public List<PropSO> propList = new List<PropSO>();
    public PropSO GetPropSO(string propName)
    {
        return propList.Find(x => x.propName == propName);
    }
}
