using Cinemachine;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using UnityEngine;

public class PlayerCameraController : Singleton<PlayerCameraController>
{
    [Header("监听")]
    public LoadedSceneEventSO OnLoadedScene;
    public PlayerRespawnEventSO OnPlayerRespawn;

    public CinemachineConfiner2D confiner;
    private CinemachineVirtualCamera playerCamera;
    
    public GameObject backGround;
    public float BGforwardPercent;
    public float BGUpPercent;
    private Vector3 lastCameraPos;

    private bool canBGFollow;

    private static Vector3 bg_Pos;
    private static bool isNeedRead_bg_Pos;
    private void Awake()
    {
        base.Awake();
        playerCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner2D>();
    }
    private void Start()
    {
        //lastCameraPos = Camera.main.transform.position;
    }
    private void OnEnable()
    {
        OnLoadedScene.OnLoadedSceneEvent.AddListener(LookAtPlayer);
        OnLoadedScene.OnLoadedSceneEvent.AddListener(GetNewBound);
        OnLoadedScene.OnLoadedSceneEvent.AddListener(GetNewBackGround);
        //OnPlayerRespawn.OnPlayerDeadEvent.AddListener(LookAtPlayer);
    }

    private void Update()
    {
        if (canBGFollow)
        {
            //摄像机稳定后再做跟随
            float deltaX = Camera.main.transform.position.x - lastCameraPos.x;
            float deltaY = Camera.main.transform.position.y - lastCameraPos.y;
            if (backGround != null)
            {
                backGround.transform.position = backGround.transform.position + new Vector3(deltaX*BGforwardPercent, deltaY * BGUpPercent);
                bg_Pos = backGround.transform.position;
            }
                        
            lastCameraPos = Camera.main.transform.position;
        }
        
    }

    private void OnDisable()
    {
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(LookAtPlayer);
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(GetNewBound);
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(GetNewBackGround);
        //OnPlayerRespawn.OnPlayerDeadEvent.RemoveListener(LookAtPlayer);
    }
    public void LookAtPlayer()
    {
         playerCamera.Follow =  playerCamera.LookAt = GameObject.FindWithTag("LookPoint").transform;
    }

    //获取场景的边界 创建Bound
    private void GetNewBound()
    {
        canBGFollow = false;
        var bound = GameObject.FindGameObjectWithTag("Bound"); //寻找Bound标签对象
        if (bound != null )
        {
            confiner.m_BoundingShape2D = bound.GetComponent<Collider2D>();
            confiner.InvalidateCache();//清空上个场景的边界等缓存
        }   
    }

    private void GetNewBackGround()
    {
        var bg = GameObject.FindGameObjectWithTag("BackGround");
        if (bg != null)
        {
            backGround = bg;
            if (isNeedRead_bg_Pos) //如果需要读取保存的背景点位
            {
                backGround.transform.position = bg_Pos;
                isNeedRead_bg_Pos = false;
            }
            else //不需要的话，就用背景的初始坐标初始化bg_Pos
            {
                bg_Pos = backGround.transform.position;
            }

        }
        StartCoroutine(WaitForCameraToStabilize()); // 等待摄像机稳定
    }

    // 等待摄像机稳定
    IEnumerator WaitForCameraToStabilize()
    {
        yield return new WaitForSeconds(1);
        // 摄像机稳定后的操作
        canBGFollow = true;
        lastCameraPos = Camera.main.transform.position;
    }
  // 保存当前场景的背景点位
    public static void SaveBackGroundPoisition()
    {
        string saveJson = JsonUtility.ToJson(bg_Pos);
        //print(saveJson);
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        File.WriteAllText(Application.streamingAssetsPath + "/saveLastGame_Bg_Pos_Json.json", saveJson);
    }
   //读取保存的当前场景点位
    public static void ReadBackGroundPoisition()
    {
        if(File.Exists(Application.streamingAssetsPath + "/saveLastGame_Bg_Pos_Json.json"))
        {
            string readData = File.ReadAllText(Application.streamingAssetsPath + "/saveLastGame_Bg_Pos_Json.json");
            Vector3 pos = JsonUtility.FromJson<Vector3>(readData);
            bg_Pos = pos;
            isNeedRead_bg_Pos = true;
        }
    }
    
}
