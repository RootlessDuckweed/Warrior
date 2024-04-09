using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Bullet bulletPrefab; //�ӵ�Ԥ����
    public Transform LaunchPoint; //�����λ
    public bool isStart; // �Ƿ�ʼ���
    public float launchTime; //������
    public float launchCounter; //������
    public bool isLaunchCold; //������ȴ
    public Vector2 dir; // ���䷽��
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

    // �������ص����� �������˺���
    public void SetLauncher(bool isLaunch)
    {
        isStart=isLaunch;
    }
}
