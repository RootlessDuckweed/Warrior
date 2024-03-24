using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    [HideInInspector]
    public float extraDamage; //������˺�
    public float criticalAddtionScale; //�����˺��ӳɱ���
    [Header("�������� 0~1")]
    public float criticalRate; //��������
    [Header("����ʱ������ı���")]
    public float critTimeScale;
    [Header("��������ʱ�������ʱ��")]
    public float critDuration;
    public bool isCritical; //�Ƿ񱩻�
    /// <summary>
    /// �������ǵĹ�����λ ���罣 ����ײ�巶Χ��֪ͨ�Է�ִ�п�Ѫ����
    /// </summary>
    /// <param name="other"> �������ǹ�����ײ�巶Χ�ĵз� </param>
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Character>()||criticalRate == 0f) return;
        float randomRate = Random.value;
        if (randomRate < criticalRate || randomRate >= 1f)
        {
            isCritical = true;
            PerformCriticalAttack();
            extraDamage = damage * criticalAddtionScale;
        }
        else
        {
            extraDamage = 0;
        }
    }

    public void PerformCriticalAttack()
    {    
        // ��ͣ��Ϸʱ��
        Time.timeScale = critTimeScale;

        // �ڱ���Ч��������ָ�ʱ�������
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        yield return new WaitForSecondsRealtime(critDuration);
        print("ResumeTime");
        Time.timeScale = 1f;
    }
}
