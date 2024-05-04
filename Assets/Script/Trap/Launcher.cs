using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Launcher : MonoBehaviour
{
    public Bullet bulletPrefab; //子弹预制体
    public Transform LaunchPoint; //发射点位
    public bool isStart; // 是否开始射击
    public float launchTime; //发射间隔
    public float launchCounter; //计数器
    public bool isLaunchCold; //发射冷却
    public Vector2 dir; // 发射方向
    public Animator anim;

    [HideInInspector]
    public ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(CreateFunc,ActionOnGet,ActionOnRelease,ActionOnDestory,false,10,1000);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isStart)
        {
            LaunchTimeCounter();
        }
    }

    private Bullet CreateFunc() // 池子为空 或 超出默认对象数量调用
    {
        var bulletObj = Instantiate(bulletPrefab,LaunchPoint.position,Quaternion.identity,transform);
        
        bulletObj.bulletPool = bulletPool;
        bulletObj.gameObject.SetActive(false);
        bulletObj.dir = dir;
        return bulletObj;
    }

    private void ActionOnGet(Bullet bullet) // 拿去池子对象调用
    {
        bullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Bullet bullet)  //返回池子调用
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestory(Bullet bullet) //超出池子大小，无法返回池子时调用
    {
        Destroy(bullet);
    }

    private void LaunchTimeCounter()
    {
        if (!isLaunchCold)
        {
            launchCounter -= Time.deltaTime;
            if (launchCounter <= 0f)
            {
                isLaunchCold = false;
                launchCounter = launchTime;
                var bulletObject = bulletPool.Get();
                bulletObject.transform.position=LaunchPoint.transform.position;
                bulletObject.dir = dir;
            }
        }
    }

    // 监听开关的启动 来启动此函数
    public void SetLauncher(bool isLaunch)
    {
        isStart=isLaunch;
        anim.SetBool("isLaunch", isLaunch);
    }
    
}
