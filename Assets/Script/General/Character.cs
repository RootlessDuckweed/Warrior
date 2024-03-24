using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float invulnerableDuration; //�����޵е�ʱ�����
    public float invulnerableCounter; //�����޵����� ����
    public bool invulnerable;

    public UnityEvent<Character> OnHealthChange; //Ѫ�������仯֪ͨ������
    public UnityEvent<Transform> OnTakenDamage; // ����ʱ�� ֪ͨ������
    public UnityEvent OnDead; // ������ʱ��֪ͨ������

    //RootlessDuckweed:�޸Ľ�ɫ����ʱ �Զ���ֵѪ��
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //��������޵�ʱ�䣬��ô�ͼ�ʱ�޵е�ʱ�� 
        InvulnerableConter();
    }

    /// <summary>
    /// �����뵽һЩ��ײ��ʱ��������߼�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    /// <summary>
    /// ��ɫ�ܵ��˺� �߼�
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
            //ִ������
            OnTakenDamage?.Invoke(attacker.transform); //֪ͨ�����ߣ�������
        }
        else
        { 
            //�������� ֪ͨ���Ľ�ɫ�����Ķ�����
            if (currentHealth > 0) OnDead?.Invoke();
            currentHealth = 0f;


        }
        OnHealthChange?.Invoke(this);//ִ��Ѫ���仯���� ֪ͨ������
    }

    /// <summary>
    /// ��������֮���޵�һ��ʱ��
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
    /// ����������޵�ʱ�䣬��ô�ͼ�ʱ�޵е�ʱ�� ���޵�ʱ��һ���������޵�
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

