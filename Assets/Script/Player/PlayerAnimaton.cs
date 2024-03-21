using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimaton : MonoBehaviour
{
    public PlayerController playerController;
    public PhysicsCheck physicsCheck;
    public Animator anim;
    private void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        SetAnimation();
    }
    void SetAnimation()
    {
        anim.SetFloat("speed", Mathf.Abs(playerController.rb.velocity.x));
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetFloat("velocityY", playerController.rb.velocity.y);
    }

    /// <summary>
    /// 触发动画状态机的滑动参数
    /// </summary>
    public void TriggerSlide()
    {
        anim.SetTrigger("slide");
    }

    public void TriggerAttack()
    {
        anim.SetTrigger("attack");
    }
}
