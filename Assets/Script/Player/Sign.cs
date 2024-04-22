using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 主角可互动标签类 
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
        input.GamePlay.Confirm.started += OnConfirmAction;
    }

    private void OnDisable()
    {
        input.Disable();
        input.GamePlay.Confirm.started -= OnConfirmAction;
    }

    // 按下确认按钮 触发可互动对象的 互动逻辑
    private void OnConfirmAction(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            targetItem?.TriggerAction();
            print("Confirm");
            canPress = false;
            if(!targetItem.RepeatInteraction())
                target.tag = "Untagged";
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
        }
            
         //TODO: 显示可互动的图标指示
         
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //TODO: 关闭可互动的图标指示
       
    }
}
