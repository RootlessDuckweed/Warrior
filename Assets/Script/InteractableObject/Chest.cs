using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RootlessDuckweed�������� 
public class Chest : MonoBehaviour,IInteractable //ʵ��IInterractable �ɻ����ӿ�
{
    Animator animator; //�������������
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void TriggerAction() //ʵ�ֽӿڴ��������߼�
    {
        animator.SetBool("isOpen", true);
        gameObject.tag = "Untagged";
    }
}
