using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����
[Serializable]
[CreateAssetMenu(menuName = "Inventory/InventorySO")]
public class InventorySO : ScriptableObject
{
    public Dictionary<string,int> propsPakage=new Dictionary<string, int>();
    //��¼���ǵ�һ�α�������ȡ����Ʒ
    public List<string> propsGot;
    public bool GetPropSO(string propName)
    {
        return propsGot.Contains(propName);
    }
    public void AddProp(PropSO propSO,int count)
    {
        if(propsPakage.ContainsKey(propSO.propName))
        {
            //�����汳���б������иõ���,��ֱ�����ֵ伯��Ϊ���߶�Ӧ��PropStack��������
            propsPakage[propSO.propName] += count;
        }
        //����ֵ伯��û�����ʵ�ʴ洢�ĵ�������,�����ֵ伯�м���
        else
        {
            propsPakage.Add(propSO.propName,count);
            if(!propsGot.Contains(propSO.propName))
            {
                propsGot.Add(propSO.propName);
            }
        }
    }
    //�Ƴ�һ�������ڵĵ���
    public void RemoveProp(PropSO propSO) {
        //����иõ�����õ����ڱ����ڵ���������,����ֱ�ӷ���
        if(propsPakage.ContainsKey(propSO.propName))
        {
            propsPakage[propSO.propName]--;
        }
        else
        {
            return;
        }
        //���ñ����еĸ��������������Ϊ0ʱ,���õ����Ƴ�
        if (propsPakage[propSO.propName] ==0)
        {
            propsPakage.Remove(propSO.propName);
        }
    }
}
