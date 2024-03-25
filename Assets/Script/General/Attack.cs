using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    [HideInInspector]
    public float extraDamage; //额外的伤害
    public float criticalAddtionScale; //暴击伤害加成比例

    [Header("事件传输来的数据")]
    public AttackSO attackData;
    /// <summary>
    /// 进入我们的攻击单位 比如剑 的碰撞体范围就通知对方执行扣血操作
    /// </summary>
    /// <param name="other"> 进入我们攻击碰撞体范围的敌方 </param>
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
