using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ǿɻ�����ǩ�� 
public class Sign : MonoBehaviour
{
    PlayerInput input;
    IInteractable targetItem;
    bool canPress;
    GameObject target;
    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.GamePlay.Confirm.performed += OnConfirmAction;
    }

    private void OnDisable()
    {
        input.Disable();
        input.GamePlay.Confirm.performed -= OnConfirmAction;
    }

    // ����ȷ�ϰ�ť �����ɻ�������� �����߼�
    private void OnConfirmAction(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            targetItem?.TriggerAction();
            print("Confirm");
            canPress = false;
            target.tag = "Untagged";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.gameObject.GetComponent<IInteractable>();
            target = collision.gameObject;
        }
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
         //TODO: ��ʾ�ɻ�����ͼ��ָʾ
         
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //TODO: �رտɻ�����ͼ��ָʾ
        targetItem = null;
    }
}
