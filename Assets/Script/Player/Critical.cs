using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

// ���������ű�
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
        // ��ͣ��Ϸʱ��
        Time.timeScale = controller.critTimeScale;

        // �ڱ���Ч��������ָ�ʱ�������
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
