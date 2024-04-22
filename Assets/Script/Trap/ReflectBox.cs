using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBox : MonoBehaviour
{
    public bool isLeftReflect; //是否为左反射箱 否则为右反射箱 
    public bool isSwitchRotat; // 是否为开关控制旋转
    private float rotatAngle; //开关的关闭将会改变角度的多少
    private float changeZ; //初始Z的欧拉角
    
    private void Awake()
    {
        rotatAngle = 90f;
        changeZ = transform.rotation.eulerAngles.z;
    }

    // 不能用开关控制旋转的反射箱 若发生了90度 的变化
    private void FixedUpdate()
    {
        if (!isSwitchRotat)
        {
            //如果当前的Z欧拉角和初始角的增量为 90 或 -90 改变
            if (transform.rotation.eulerAngles.z - changeZ >= 90f || transform.rotation.eulerAngles.z - changeZ<=-90f)
            {
                isLeftReflect = !isLeftReflect;
                changeZ = transform.rotation.eulerAngles.z;
            }   
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
