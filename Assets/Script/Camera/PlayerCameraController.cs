using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
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
    private void Awake()
    {
        playerCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner2D>();
    }
    private void Start()
    {
        lastCameraPos = transform.position;
    }
    private void OnEnable()
    {
        OnLoadedScene.OnLoadedSceneEvent.AddListener(LookAtPlayer);
        OnLoadedScene.OnLoadedSceneEvent.AddListener(GetNewBound);
        OnLoadedScene.OnLoadedSceneEvent.AddListener(GetNewBackGround);
        OnPlayerRespawn.OnPlayerDeadEvent.AddListener(LookAtPlayer);
    }

    private void Update()
    {
        float deltaX = transform.position.x - lastCameraPos.x;
        float deltaY = transform.position.y - lastCameraPos.y;
        if (backGround!=null)
            backGround.transform.position = backGround.transform.position + new Vector3(deltaX*BGforwardPercent, deltaY * BGUpPercent);
        lastCameraPos = transform.position;
    }

    private void OnDisable()
    {
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(LookAtPlayer);
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(GetNewBound);
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(GetNewBackGround);
        OnPlayerRespawn.OnPlayerDeadEvent.RemoveListener(LookAtPlayer);
    }
    private void LookAtPlayer()
    {
         playerCamera.Follow =  playerCamera.LookAt = GameObject.FindWithTag("LookPoint").transform;
    }

    //获取场景的边界 创建Bound
    private void GetNewBound()
    {
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
        }
    }
}
