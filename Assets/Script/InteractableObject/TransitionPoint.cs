using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour, IInteractable
{
    public GameSceneSO SceneToGo;
    public void TriggerAction()
    {
        SceneLoaderManager.Instance.StartToLoad(SceneToGo);
    }
}
