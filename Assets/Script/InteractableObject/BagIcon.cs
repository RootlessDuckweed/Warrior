using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagIcon : MonoBehaviour
{
    [Header("¼àÌý")]
    public LoadedSceneEventSO OnLoadedScene;
    public StartToLoadSceneEventSO OnStartToLoadScene;
    public bool isOpened;
    private void Awake()
    {
        OnLoadedScene.OnLoadedSceneEvent.AddListener(SetGameObjectActive);
        OnStartToLoadScene.OnStartToLoadSceneEvent.AddListener(SetGameObjectInactive);
    }
    private void OnDestroy()
    {
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(SetGameObjectActive);
        OnStartToLoadScene.OnStartToLoadSceneEvent.RemoveListener(SetGameObjectInactive);
    }
    void SetGameObjectActive()
    {
        gameObject.SetActive(true);
    }
    void SetGameObjectInactive()
    {
        gameObject.SetActive(false);
    }
    public void OpenBag()
    { 
        if (!isOpened)
        {
            UIManager.Instance.OpenPanel("InventoryPanel");
        }
        else
        {
            UIManager.Instance.ClosePanel("InventoryPanel");
            Time.timeScale = 1;
        }
        isOpened = !isOpened;
    }
}
