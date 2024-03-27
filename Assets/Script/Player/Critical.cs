using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

// 发生暴击脚本
public class Critical : MonoBehaviour
{
    public PlayerController controller;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PerformCriticalAttack()
    {
        // 暂停游戏时间
        Time.timeScale = controller.critTimeScale;

        // 在暴击效果结束后恢复时间的流逝
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        yield return new WaitForSecondsRealtime(controller.critDuration);
        print("ResumeTime");
        Time.timeScale = 1f;
        controller.isCritical = false;
        controller. attackCriticalData.SetCritical(controller.isCritical);
        gameObject.SetActive(false);
    }
}
