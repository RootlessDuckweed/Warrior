using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class DeadPanel : BasePanel
{
    Button RetryBtn;
    public GameObject PlayerPrefab;
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
            Instantiate(player).transform.position = SceneLoaderManager.Instance.saveSceneSO.positionToGo;
        }
        UIManager.Instance.ClosePanel("DeadPanel");
    }

}
