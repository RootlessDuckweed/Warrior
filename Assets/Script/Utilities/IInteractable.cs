using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RootlessDuckweed:�ɻ����� �ӿ�
public interface IInteractable 
{
    bool RepeatInteraction(); //�Ƿ���ظ����������� bool
    void TriggerAction(); //�����߼�

    void DeserializeFromJsonData(string json) { } // ��json�ļ��з����б������Ϣ ��ִ����ز���

    string GetNeedToCovertJsonData() { return null; } // ��ȡ��Ҫ�����json���ݴӱ�����
}
