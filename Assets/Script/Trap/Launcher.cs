using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Bullet bulletPrefab; //子弹预制体
    public Transform LaunchPoint; //发射点位
    public bool isStart; // 是否开始射击
    public float launchTime; //发射间隔
    public float launchCounter; //计数器
    public bool isLaunchCold; //发射冷却
    public Vector2 dir; // 发射方向
    private void Update()
    {
        if (isStart)
        {
            LaunchTimeCounter();
        }
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
                var bullet = Instantiate(bulletPrefab, LaunchPoint.position, Quaternion.identity);
                bullet.dir = dir;
            }
        }
    }

    // 监听开关的启动 来启动此函数
    public void SetLauncher(bool isLaunch)
    {
        isStart=isLaunch;
    }
}
