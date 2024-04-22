using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public bool RepeatInteraction()
    {
        return false;
    }

    public void TriggerAction()
    {
        SceneLoaderManager.Instance.SaveScenePoint(transform.position);
        //Bocchi:���ñ��汳�����ݵķ���
        InventoryManager.Instance.SaveInventoryData();
    }
}
