using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class SceneLoaderManager : Singleton<SceneLoaderManager>
{
    public GameSceneSO FristSceneSO;
    public GameSceneSO nextSceneSO;
    private GameSceneSO currentSceneSO;

    [Header("Event")]
    public FadeSO FadeEvent;
    public float fadeDuration;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UIManager.Instance.OpenPanel("MenuPanel");
    }

    // �µ���Ϸ
    public void NewGame()
    {
        //TODO:Fade in
        nextSceneSO = FristSceneSO;
        StartToLoad(FristSceneSO);
        UIManager.Instance.ClosePanel("MenuPanel");   
    }

    //��ʼ ��ȡ��һ��
    private void LoadNextScene(GameSceneSO next)
    {
        var handle =  next.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        handle.Completed += OnLoadCompleted;
        
    }

    //������ɺ�
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        SceneManager.SetActiveScene(obj.Result.Scene);
        FadeEvent.RaisedEvent(fadeDuration, false);
        currentSceneSO = nextSceneSO;
    }

    // ������һ�ص��߼� ����ж�ص�ǰ����
     IEnumerator LoadScene(float dura,bool isFadeIn)
    {
        FadeEvent.RaisedEvent(fadeDuration, isFadeIn);
        yield return new WaitForSeconds(dura);
        if (currentSceneSO != null)
        {
          yield return  currentSceneSO.sceneReference.UnLoadScene(); //ж�ص�ǰ�ؿ�
        }
        LoadNextScene(nextSceneSO); //��ʼ������һ��
        
    }

    // ��ʼ������һ��
    public void StartToLoad(GameSceneSO next)
    {
        nextSceneSO = next;
        StartCoroutine(LoadScene(fadeDuration,true));
    }
   
}
