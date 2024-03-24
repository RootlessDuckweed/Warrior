using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ǿɻ�����ǩ�� 
public class Sign : MonoBehaviour
{
    PlayerInput input;
    IInteractable targetItem;
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
        targetItem?.TriggerAction();
        print("Confirm");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            //TODO: ��ʾ�ɻ�����ͼ��ָʾ
            targetItem = collision.gameObject.GetComponent<IInteractable>();
            print("Interactable");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //TODO: �رտɻ�����ͼ��ָʾ
        targetItem = null;
    }
}
