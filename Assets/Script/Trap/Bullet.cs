using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb; // ����������
    public float speed;  // �����ٶ�
    [HideInInspector]
    public Vector2 dir; // ���䷽��

    public ObjectPool<Bullet> bulletPool;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        rb.velocity= dir *speed* Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, 0, Time.deltaTime*500 + transform.localEulerAngles.z);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "ReflectBox":
                //TODO: ת������ 
                bool isLeftReflect = other.gameObject.GetComponent<ReflectBox>().isLeftReflect;
                if (isLeftReflect)
                {
                    if (dir.x > 0f && dir.y == 0f)
                    {
                        dir = new Vector2(0, 1);
                    }
                    else if(dir.x < 0f && dir.y == 0f)
                    {
                        dir = new Vector2(0, -1);
                    }

                    else if(dir.x == 0f && dir.y > 0f)
                    {
                        dir = new Vector2(1, 0);
                    }
                    else if(dir.x == 0f && dir.y < 0f)
                    {
                        dir = new Vector2(-1, 0);
                    }


                }
                else
                {
                    if (dir.x > 0f && dir.y == 0f)
                    {
                        dir = new Vector2(0, -1);
                    }
                    else if(dir.x < 0f && dir.y == 0f)
                    {
                        dir = new Vector2(0, 1);
                    }
                    else if (dir.x == 0f && dir.y > 0f)
                    {
                        dir = new Vector2(-1, 0);
                    }
                    else if (dir.x == 0f && dir.y < 0f)
                    {
                        dir = new Vector2(1, 0);
                    }
                }
                break;
            
            //FIXME: ���������⣬д���������� �����޸�
            case "Player":
                //TODO: ת������
                if (rb.velocity.y != 0f)
                {
                    dir = new Vector2(dir.x, -dir.y);
                }
                else
                {
                    dir = -dir;
                }
                break;

            case "PlayerDead":
                //TODO: ת������
                if (rb.velocity.y < 0f)
                {
                    dir = new Vector2(dir.x, 1);
                }
                else if(rb.velocity.y > 0f)
                {
                    dir = new Vector2(dir.x, -1);
                }
                else
                {
                    dir =  new Vector2(-dir.x, 0);
                }
                break;

            case "Ground":
                bulletPool?.Release(this);
                break;

            default:
                break;
        }
       
    }


}
