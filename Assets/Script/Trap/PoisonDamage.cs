using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    private Vector2 deadPoint;
    public Attack attack;
    private void Awake()
    {
        deadPoint = transform.GetChild(0).position;
        attack = GetComponent<Attack>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
       if(other.gameObject.GetComponent<PlayerController>()!=null&&!other.gameObject.GetComponent<PlayerController>().isDead) 
           other.gameObject.GetComponent<Character>()?.TakeDamage(attack);
       if (other.gameObject.CompareTag("PlayerDead"))
       {
           Vector3 playerPos = other.transform.position;
           other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
           other.gameObject.transform.position= new Vector3(playerPos.x,deadPoint.y,0);
       }        
    }
}
