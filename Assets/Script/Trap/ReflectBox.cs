using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBox : MonoBehaviour
{
    public bool isLeftReflect; //是否为左反射箱 否则为右反射箱 
    public bool isSwitchRotat; // 是否为开关控制旋转
    private float rotatAngle; //开关的关闭将会改变角度的多少
    [SerializeField] private float changeZ; //初始Z的欧拉角
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rotatAngle = 90f;
    }

    // 不能用开关控制旋转的反射箱 若发生了90度 的变化
    private void FixedUpdate()
    {
        
        if (!isSwitchRotat)
        {
            if (rb.velocity.magnitude <= 0.1f)
            {
                if (transform.localRotation.eulerAngles.z - changeZ >= 88f || transform.localRotation.eulerAngles.z - changeZ<=-88f)
                {
                    //isLeftReflect = !isLeftReflect;
                    changeZ = transform.localRotation.eulerAngles.z - changeZ;
                    changeZ = Mathf.Floor(Mathf.Abs(changeZ));
                    float tmp = changeZ % 90;
                    int cnt;
                    if (tmp >= 45f)
                    {
                        cnt = (int)Mathf.Ceil(changeZ / 90);
                    }
                    else
                    {
                        cnt = (int)Mathf.Floor(changeZ / 90);
                    }
                    if(cnt%2!=0)
                    {
                        isLeftReflect = !isLeftReflect;
                    }
                    changeZ = transform.localRotation.eulerAngles.z;
                }   
            }
            //如果当前的Z欧拉角和初始角的增量超过90改变
            
        }
    }

    // 开关控制左反射箱的变化的订阅者
    public void ChangeLeftReflect()
    {
        if (isSwitchRotat)
        {
            isLeftReflect = !isLeftReflect;
            if (isLeftReflect)
                transform.DORotate(new Vector3(0, 0, 0), 1);
            else
                transform.DORotate(new Vector3(0, 0, rotatAngle), 1);
        }
       
    }
    // 开关控制右反射箱的变化的订阅者
    public void ChangeRightReflect()
    {
        if (isSwitchRotat)
        {
            isLeftReflect = !isLeftReflect;
            if (isLeftReflect)
                transform.DORotate(new Vector3(0, 0, rotatAngle), 1);
            else
                transform.DORotate(new Vector3(0, 0, 0), 1);
        }
    }
}
