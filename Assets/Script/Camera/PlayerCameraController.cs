using Cinemachine;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using UnityEngine;

public class PlayerCameraController : Singleton<PlayerCameraController>
{
    [Header("����")]
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
            //������ȶ�����������
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

    //��ȡ�����ı߽� ����Bound
    private void GetNewBound()
    {
        canBGFollow = false;
        var bound = GameObject.FindGameObjectWithTag("Bound"); //Ѱ��Bound��ǩ����
        if (bound != null )
        {
            confiner.m_BoundingShape2D = bound.GetComponent<Collider2D>();
            confiner.InvalidateCache();//����ϸ������ı߽�Ȼ���
        }   
    }

    private void GetNewBackGround()
    {
        var bg = GameObject.FindGameObjectWithTag("BackGround");
        if (bg != null)
        {
            backGround = bg;
            if (isNeedRead_bg_Pos) //�����Ҫ��ȡ����ı�����λ
            {
                backGround.transform.position = bg_Pos;
                isNeedRead_bg_Pos = false;
            }
            else //����Ҫ�Ļ������ñ����ĳ�ʼ�����ʼ��bg_Pos
            {
                bg_Pos = backGround.transform.position;
            }

        }
        StartCoroutine(WaitForCameraToStabilize()); // �ȴ�������ȶ�
    }

    // �ȴ�������ȶ�
    IEnumerator WaitForCameraToStabilize()
    {
        yield return new WaitForSeconds(1);
        // ������ȶ���Ĳ���
        canBGFollow = true;
        lastCameraPos = Camera.main.transform.position;
    }
  // ���浱ǰ�����ı�����λ
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
   //��ȡ����ĵ�ǰ������λ
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
