using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    [SerializeField]private GameSceneSO currentSceneSO;
    public GameSceneSO saveSceneSO;
    private Transform playerTrans;
    private bool isLoadSavePoint;

    [Header("Event")]
    public StartToLoadSceneEventSO startToLoadSceneEvent;
    public LoadedSceneEventSO loadedSceneEvent;
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
    
    // 新的游戏
    public void NewGame()
    {
        //TODO:Fade in
        nextSceneSO = FristSceneSO;
        startToLoadSceneEvent.RaisedEvent();
        StartToLoad(nextSceneSO);
        UIManager.Instance.ClosePanel("MenuPanel");
    }
   

    // 菜单的继续
    public void ContinueGame()
    {
        if (!File.Exists(Application.streamingAssetsPath + "/saveSceneJson.json"))
            return;
        if (currentSceneSO == null)
        {
            startToLoadSceneEvent.RaisedEvent();
            isLoadSavePoint = true;
            var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
            JsonUtility.FromJsonOverwrite(ReadSaveScenePoint(), newScene);
            nextSceneSO = newScene;
            StartToLoad(nextSceneSO);
        } 
        UIManager.Instance.ClosePanel("MenuPanel");
    }

    public void ReLoadThisScene()
    {
        //Time.timeScale = 1;
        if(currentSceneSO!=null)
            StartToLoad(currentSceneSO);
    }

    //开始 读取下一关
    private void LoadNextScene(GameSceneSO next)
    {
        var handle =  next.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        handle.Completed += OnLoadCompleted;
        
    }

    //加载完成后
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Result.Scene.name != "GameOver")
        {
        SceneManager.SetActiveScene(obj.Result.Scene);
        FadeEvent.RaisedEvent(fadeDuration, false);
        currentSceneSO = nextSceneSO;
        playerTrans = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        if (isLoadSavePoint)
        {
            playerTrans.position = currentSceneSO.positionToGo;
            //读取已经保存的可互动对象的状态
            SaveInteractableObserver.LoadInteractableObj();
            //读取已经保存的背景
            PlayerCameraController.ReadBackGroundPoisition();
            isLoadSavePoint = false;
        }

        loadedSceneEvent.RaisedEvent(); //先完成场景加载之后的事件
        SaveScenePoint(playerTrans.position); //再保存场景点位
        }
       
    }

    // 加载下一关的逻辑 包括卸载当前场景
    IEnumerator LoadScene(float dura,bool isFadeIn)
    {
        FadeEvent.RaisedEvent(fadeDuration, isFadeIn);
        yield return new WaitForSecondsRealtime(fadeDuration);

        if (currentSceneSO != null)
        {
            yield return  currentSceneSO.sceneReference.UnLoadScene(); //卸载当前关卡
        }
        LoadNextScene(nextSceneSO); //开始加载下一关
        
    }

    // 开始传送下一关
    public void StartToLoad(GameSceneSO next)
    {
        nextSceneSO = next;
        StartCoroutine(LoadScene(fadeDuration,true));
    }
   
    public void SaveScenePoint(Vector3 position)
    {
        saveSceneSO = currentSceneSO;
        saveSceneSO.positionToGo = position;
        
        string saveJson = JsonUtility.ToJson(saveSceneSO);
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        File.WriteAllText(Application.streamingAssetsPath+"/saveSceneJson.json",saveJson);
        // 保存当前场景的背景位置
        PlayerCameraController.SaveBackGroundPoisition();

    }

    private string ReadSaveScenePoint()
    {
        return File.ReadAllText(Application.streamingAssetsPath + "/saveSceneJson.json");
    }
   
}
