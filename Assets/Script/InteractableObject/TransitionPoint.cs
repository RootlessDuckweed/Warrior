using System.Collections;
using System.Collections.Generic;
using Script.Observer;
using UnityEngine;

public class TransitionPoint : MonoBehaviour, IInteractable
{
    public GameSceneSO SceneToGo;
    private Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public bool RepeatInteraction()
    {
        return false;
    }

    public void TriggerAction()
    {
        anim.SetBool("isConvey", true);
        InventoryManager.Instance.ClearInventory();
        SavePlayerDeadObserver.RemoveAll();
    }

    #region Unity Animation Event
    public void TriggerTransition()
    {
        SceneLoaderManager.Instance.StartToLoad(SceneToGo);
    }
    #endregion
}
