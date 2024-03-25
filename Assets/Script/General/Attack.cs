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

    [Header("�¼�������������")]
    public AttackSO attackData;
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
        if (attackData == null) return;
        if (attackData.GetCritical())
        {
            extraDamage = 0f;
            extraDamage = damage * criticalAddtionScale;
        }
        else
        {
            extraDamage = 0f;
        }
    }

}
