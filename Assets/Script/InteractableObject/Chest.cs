using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RootlessDuckweed：宝箱类 
public class Chest : MonoBehaviour,IInteractable //实现IInterractable 可互动接口
{
    Animator animator; //自身动画播放组件
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void TriggerAction() //实现接口触发互动逻辑
    {
        animator.SetBool("isOpen", true);
        gameObject.tag = "Untagged";
    }
}
