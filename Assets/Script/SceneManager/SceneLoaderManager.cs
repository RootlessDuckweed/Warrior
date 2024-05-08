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
    
    // �µ���Ϸ
    public void NewGame()
    {
        //TODO:Fade in
        nextSceneSO = FristSceneSO;
        startToLoadSceneEvent.RaisedEvent();
        StartToLoad(nextSceneSO);
        UIManager.Instance.ClosePanel("MenuPanel");
    }
   

    // �˵��ļ���
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

    //��ʼ ��ȡ��һ��
    private void LoadNextScene(GameSceneSO next)
    {
        var handle =  next.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        handle.Completed += OnLoadCompleted;
        
    }

    //������ɺ�
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
            //��ȡ�Ѿ�����Ŀɻ��������״̬
            SaveInteractableObserver.LoadInteractableObj();
            //��ȡ�Ѿ�����ı���
            PlayerCameraController.ReadBackGroundPoisition();
            isLoadSavePoint = false;
        }

        loadedSceneEvent.RaisedEvent(); //����ɳ�������֮����¼�
        SaveScenePoint(playerTrans.position); //�ٱ��泡����λ
        }
       
    }

    // ������һ�ص��߼� ����ж�ص�ǰ����
    IEnumerator LoadScene(float dura,bool isFadeIn)
    {
        FadeEvent.RaisedEvent(fadeDuration, isFadeIn);
        yield return new WaitForSecondsRealtime(fadeDuration);

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
        // ���浱ǰ�����ı���λ��
        PlayerCameraController.SaveBackGroundPoisition();

    }

    private string ReadSaveScenePoint()
    {
        return File.ReadAllText(Application.streamingAssetsPath + "/saveSceneJson.json");
    }
   
}
