using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public void TriggerAction()
    {
        SceneLoaderManager.Instance.SaveScenePoint(transform.position);
    }
}
