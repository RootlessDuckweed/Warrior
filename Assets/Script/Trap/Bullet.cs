using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb; // 自身刚体组件
    public float speed;  // 发射速度
    [HideInInspector]
    public Vector2 dir; // 发射方向
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity= dir *speed* Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "ReflectBox":
                //TODO: 转换方向 
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
            
            //FIXME: 这里有问题，写完其他代码 再来修复
            case "Player":
                //TODO: 转换方向
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
                //TODO: 转换方向
                if (rb.velocity.y != 0f)
                {
                    dir = new Vector2(dir.x, -dir.y);
                }
                else
                {
                    dir = -dir;
                }
                break;

            case "Ground":
                Destroy(this.gameObject);
                break;

            default:
                break;
        }
       
    }


}
