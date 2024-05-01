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
    GameObject showSign;
    private void Awake()
    {
        input = new PlayerInput();
        showSign = transform.GetChild(0).gameObject;
    }
    private void OnEnable()
    {
        input.Enable();
        input.GamePlay.Confirm.started += OnConfirmAction;
    }

    private void OnDisable()
    {
        input.Disable();
        input.GamePlay.Confirm.started -= OnConfirmAction;
    }

    // ����ȷ�ϰ�ť �����ɻ�������� �����߼�
    private void OnConfirmAction(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            targetItem?.TriggerAction();
            print("Confirm");
            canPress = false;
            if (!targetItem.RepeatInteraction())
            {
                target.tag = "Untagged";
                showSign.SetActive(false);
            }
                
            targetItem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.gameObject.GetComponent<IInteractable>();
            target = collision.gameObject;
            showSign.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //TODO: �رտɻ�����ͼ��ָʾ
        canPress = false;
        showSign.SetActive(false);
    }
}
