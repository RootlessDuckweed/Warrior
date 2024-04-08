using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����
[CreateAssetMenu(menuName = "Inventory/InventorySO")]
public class InventorySO : ScriptableObject
{
    public Dictionary<string,PropStack> propsPakage=new Dictionary<string, PropStack>();
    public void AddProp(PropSO propSO,int count)
    {
        if(propsPakage.ContainsKey(propSO.name))
        {
            //�����汳���б������иõ���,��ֱ�����ֵ伯��Ϊ���߶�Ӧ��PropStack��������
            propsPakage[propSO.name].propNum += count;
        }
        //����ֵ伯��û�����ʵ�ʴ洢�ĵ�������,�����ֵ伯�м���
        else
        {
            PropStack propStack = new PropStack();
            propStack.prop = propSO;
            propStack.propNum = count;
            propsPakage.Add(propSO.name, propStack);

        }
    }
    //�Ƴ�һ�������ڵĵ���
    public void RemoveProp(PropSO propSO) {
        //����иõ�����õ����ڱ����ڵ���������,����ֱ�ӷ���
        if(propsPakage.ContainsKey(propSO.name))
        {
            propsPakage[propSO.name].propNum--;
        }
        else
        {
            return;
        }
        //���ñ����еĸ��������������Ϊ0ʱ,���õ����Ƴ�
        if (propsPakage[propSO.name].propNum==0)
        {
            propsPakage.Remove(propSO.name);
        }
    }
}
