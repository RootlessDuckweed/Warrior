using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背包
[Serializable]
[CreateAssetMenu(menuName = "Inventory/InventorySO")]
public class InventorySO : ScriptableObject
{
    public Dictionary<string,int> propsPakage=new Dictionary<string, int>();
    //记录不是第一次被背包获取的物品
    public List<string> propsGot;
    public bool GetPropSO(string propName)
    {
        return propsGot.Contains(propName);
    }
    public void AddProp(PropSO propSO,int count)
    {
        if(propsPakage.ContainsKey(propSO.propName))
        {
            //若里面背包中本来就有该道具,则直接在字典集中为道具对应的PropStack增加数量
            propsPakage[propSO.propName] += count;
        }
        //如果字典集中没有这个实际存储的道具内容,就在字典集中加入
        else
        {
            propsPakage.Add(propSO.propName,count);
            if(!propsGot.Contains(propSO.propName))
            {
                propsGot.Add(propSO.propName);
            }
        }
    }
    //移除一个背包内的道具
    public void RemoveProp(PropSO propSO) {
        //如果有该道具则该道具在背包内的数量减少,否则直接返回
        if(propsPakage.ContainsKey(propSO.propName))
        {
            propsPakage[propSO.propName]--;
        }
        else
        {
            return;
        }
        //当该背包中的该类道具数量减少为0时,将该道具移除
        if (propsPakage[propSO.propName] ==0)
        {
            propsPakage.Remove(propSO.propName);
        }
    }
}
