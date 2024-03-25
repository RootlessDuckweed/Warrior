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
    public AttackSO attackData;

    private void OnTriggerStay2D(Collider2D other)
    {

        if (attackData!=null)
        {
            if(attackData.GetCritical())
            extraDamage = 0;
            extraDamage = damage * criticalAddtionScale;
        }
        else
        {
            extraDamage = 0;
        }
        other.GetComponent<Character>()?.TakeDamage(this);
    }

  
}
