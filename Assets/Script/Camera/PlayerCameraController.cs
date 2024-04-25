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
    private void Awake()
    {
        playerCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        OnLoadedScene.OnLoadedSceneEvent.AddListener(LookAtPlayer);
        OnLoadedScene.OnLoadedSceneEvent.AddListener(GetNewBound);
        OnPlayerRespawn.OnPlayerDeadEvent.AddListener(LookAtPlayer);
    }

    private void OnDisable()
    {
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(LookAtPlayer);
        OnLoadedScene.OnLoadedSceneEvent.RemoveListener(GetNewBound);
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
}
