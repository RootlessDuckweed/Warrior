using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float invulnerableDuration; //受伤无敌的时长间隔
    public float invulnerableCounter; //受伤无敌秒数 计数
    public bool invulnerable;

    public UnityEvent<Character> OnHealthChange; //血量发生变化通知订阅者
    public UnityEvent<Transform> OnTakenDamage; // 受伤时候 通知订阅者
    public UnityEvent OnDead; // 死亡的时候通知订阅者

    //RootlessDuckweed:修改角色创建时 自动赋值血量
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //如果触发无敌时间，那么就计时无敌的时间 
        InvulnerableConter();
    }

    /// <summary>
    /// 当进入到一些碰撞体时处理相关逻辑
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    /// <summary>
    /// 角色受到伤害 逻辑
    /// </summary>
    /// <param name="attacker"></param>
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }
        if (currentHealth - (attacker.damage + attacker.extraDamage)> 0f)
        {
            currentHealth -= (attacker.damage+attacker.extraDamage);
            TriggerInvulnerable();
            //执行受伤
            OnTakenDamage?.Invoke(attacker.transform); //通知订阅者，受伤了
        }
        else
        { 
            //触发死亡 通知订阅角色死亡的订阅者
            if (currentHealth > 0) OnDead?.Invoke();
            currentHealth = 0f;


        }
        OnHealthChange?.Invoke(this);//执行血量变化操作 通知订阅者
    }

    /// <summary>
    /// 触发受伤之后无敌一段时间
    /// </summary>
    void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

    /// <summary>
    /// 如果触发了无敌时间，那么就计时无敌的时间 ，无敌时间一过就重置无敌
    /// </summary>
    void InvulnerableConter()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0f)
            {
                invulnerable = false;
            }
        }
    }

}

