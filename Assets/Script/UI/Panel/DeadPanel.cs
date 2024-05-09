using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class DeadPanel : BasePanel
{
    Button RetryBtn;
    public GameObject PlayerPrefab;
    [Header("Event")]
    public PlayerRespawnEventSO playerRespawnEvent;
    //public UnityAction OnRespawnEvent;
    protected override void Awake()
    {
        RetryBtn = transform.Find("RetryButton").GetComponent<Button>();
        RetryBtn.onClick.AddListener(StartToRetry);
    }
    void StartToRetry()
    {
        StartCoroutine(GeneratePlayer());
    }

    IEnumerator GeneratePlayer()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Player");
        yield return handle;

        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject player = handle.Result;
            if (SceneLoaderManager.Instance.saveSceneSO != null)
                Instantiate(player).transform.position = SceneLoaderManager.Instance.saveSceneSO.positionToGo;
            else
                Instantiate(player);
            playerRespawnEvent.RaisedEvent();
            //PlayerCameraController.Instance.LookAtPlayer();
        }
        UIManager.Instance.ClosePanel("DeadPanel");
    }
   
    void LookAtPlayer()
    {
        PlayerCameraController.Instance.LookAtPlayer();
    }

}
