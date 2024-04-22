using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBox : MonoBehaviour
{
    public bool isLeftReflect; //�Ƿ�Ϊ������ ����Ϊ�ҷ����� 
    public bool isSwitchRotat; // �Ƿ�Ϊ���ؿ�����ת
    private float rotatAngle; //���صĹرս���ı�ǶȵĶ���
    private float changeZ; //��ʼZ��ŷ����
    
    private void Awake()
    {
        rotatAngle = 90f;
        changeZ = transform.rotation.eulerAngles.z;
    }

    // �����ÿ��ؿ�����ת�ķ����� ��������90�� �ı仯
    private void FixedUpdate()
    {
        if (!isSwitchRotat)
        {
            //�����ǰ��Zŷ���Ǻͳ�ʼ�ǵ�����Ϊ 90 �� -90 �ı�
            if (transform.rotation.eulerAngles.z - changeZ >= 90f || transform.rotation.eulerAngles.z - changeZ<=-90f)
            {
                isLeftReflect = !isLeftReflect;
                changeZ = transform.rotation.eulerAngles.z;
            }   
        }
    }

    // ���ؿ���������ı仯�Ķ�����
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
    // ���ؿ����ҷ�����ı仯�Ķ�����
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
