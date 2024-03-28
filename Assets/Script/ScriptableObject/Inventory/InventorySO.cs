using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背包
[CreateAssetMenu(menuName = "Inventory/InventorySO")]
public class InventorySO : ScriptableObject
{
    public Dictionary<string,PropStack> propsPakage=new Dictionary<string, PropStack>();
    public void AddProp(PropSO propSO,int count)
    {
        if(propsPakage.ContainsKey(propSO.name))
        {
            //若里面背包中本来就有该道具,则直接在字典集中为道具对应的PropStack增加数量
            propsPakage[propSO.name].propNum += count;
        }
        //如果字典集中没有这个实际存储的道具内容,就在字典集中加入
        else
        {
            PropStack propStack = new PropStack();
            propStack.prop = propSO;
            propStack.propNum = count;
            propsPakage.Add(propSO.name, propStack);

        }
    }
    //移除一个背包内的道具
    public void RemoveProp(PropSO propSO) {
        //如果有该道具则该道具在背包内的数量减少,否则直接返回
        if(propsPakage.ContainsKey(propSO.name))
        {
            propsPakage[propSO.name].propNum--;
        }
        else
        {
            return;
        }
        //当该背包中的该类道具数量减少为0时,将该道具移除
        if (propsPakage[propSO.name].propNum==0)
        {
            propsPakage.Remove(propSO.name);
        }
    }
}
