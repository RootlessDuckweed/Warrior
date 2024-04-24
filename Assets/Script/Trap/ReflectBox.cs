using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBox : MonoBehaviour
{
    public bool isLeftReflect; //�Ƿ�Ϊ������ ����Ϊ�ҷ����� 
    public bool isSwitchRotat; // �Ƿ�Ϊ���ؿ�����ת
    private float rotatAngle; //���صĹرս���ı�ǶȵĶ���
    [SerializeField] private float changeZ; //��ʼZ��ŷ����
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rotatAngle = 90f;
    }

    // �����ÿ��ؿ�����ת�ķ����� ��������90�� �ı仯
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
            //�����ǰ��Zŷ���Ǻͳ�ʼ�ǵ���������90�ı�
            
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
