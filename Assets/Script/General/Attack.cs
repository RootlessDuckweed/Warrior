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
    [Header("暴击几率 0~1")]
    public float criticalRate; //暴击几率
    [Header("暴击时间变慢的比例")]
    public float critTimeScale;
    [Header("暴击暴击时间变慢的时长")]
    public float critDuration;
    public bool isCritical; //是否暴击
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
        // 暂停游戏时间
        Time.timeScale = critTimeScale;

        // 在暴击效果结束后恢复时间的流逝
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        yield return new WaitForSecondsRealtime(critDuration);
        print("ResumeTime");
        Time.timeScale = 1f;
    }
}
