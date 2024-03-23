using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;

    /// <summary>
    /// �������ǵĹ�����λ ���罣 ����ײ�巶Χ��֪ͨ�Է�ִ�п�Ѫ����
    /// </summary>
    /// <param name="other"> �������ǹ�����ײ�巶Χ�ĵз� </param>
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
        print(other.gameObject.name);
    }
}
