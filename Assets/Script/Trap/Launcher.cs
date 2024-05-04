using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Launcher : MonoBehaviour
{
    public Bullet bulletPrefab; //�ӵ�Ԥ����
    public Transform LaunchPoint; //�����λ
    public bool isStart; // �Ƿ�ʼ���
    public float launchTime; //������
    public float launchCounter; //������
    public bool isLaunchCold; //������ȴ
    public Vector2 dir; // ���䷽��
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

    private Bullet CreateFunc() // ����Ϊ�� �� ����Ĭ�϶�����������
    {
        var bulletObj = Instantiate(bulletPrefab,LaunchPoint.position,Quaternion.identity,transform);
        
        bulletObj.bulletPool = bulletPool;
        bulletObj.gameObject.SetActive(false);
        bulletObj.dir = dir;
        return bulletObj;
    }

    private void ActionOnGet(Bullet bullet) // ��ȥ���Ӷ������
    {
        bullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Bullet bullet)  //���س��ӵ���
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestory(Bullet bullet) //�������Ӵ�С���޷����س���ʱ����
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

    // �������ص����� �������˺���
    public void SetLauncher(bool isLaunch)
    {
        isStart=isLaunch;
        anim.SetBool("isLaunch", isLaunch);
    }
    
}
