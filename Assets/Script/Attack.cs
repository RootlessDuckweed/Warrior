using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;

    /// <summary>
    /// 进入我们的攻击单位 比如剑 的碰撞体范围就通知对方执行扣血操作
    /// </summary>
    /// <param name="other"> 进入我们攻击碰撞体范围的敌方 </param>
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
        print(other.gameObject.name);
    }
}
