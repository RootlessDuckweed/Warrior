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
        //Bocchi:调用保存背包数据的方法
        InventoryManager.Instance.SaveInventoryData();
    }
}
