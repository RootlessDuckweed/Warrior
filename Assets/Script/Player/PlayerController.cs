using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //自身的刚体组件
    private PlayerInput input; //输入控制器
    public Vector2 inputDirection; //输入人物移动的方向
    public PlayerAnimaton playerAnimaton; //自身的PlayerAnimation脚本组件
    public PhysicsCheck physicsCheck; //自身物理检测脚本组件
  
    public float moveSpeed;
    public float currentFace;
    public float jumpForce;
    public float slideForce;
    public float slideTimeDuration;
    public float slideCounter;
    public bool isSlideCold;  //是否在滑铲冷却状态
    public bool isAttack;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimaton = GetComponent<PlayerAnimaton>();
        physicsCheck = GetComponent<PhysicsCheck>();
        
        input = new PlayerInput();
        input.GamePlay.Jump.started += Jump;
        input.GamePlay.Slide.performed += Slide;
        input.GamePlay.Attack.started += PlayerAttack;
    }

    private void PlayerAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerAnimaton.TriggerAttack();
        isAttack = true;
    }

    private void OnEnable()
    {
        input.Enable();

        
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void Update()
    {
        SlideTimeCounter();
        inputDirection = input.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //转向
        currentFace = transform.localScale.x;
        if (inputDirection.x > 0) currentFace = 1;
        if (inputDirection.x < 0) currentFace = -1;
        transform.localScale = new Vector3(currentFace, transform.localScale.y, transform.localScale.z);
        //移动
        rb.velocity = new Vector2(moveSpeed * inputDirection.x* Time.deltaTime, rb.velocity.y);
        if (isAttack)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(physicsCheck.isGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Slide(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isSlideCold) return;
        if (physicsCheck.isGround)
        {
            playerAnimaton.TriggerSlide();
            moveSpeed *= 1.5f;
            slideCounter = slideTimeDuration;
            isSlideCold = true;

        }
        
    }
    void SlideTimeCounter()
    {
        if (!isSlideCold) return;
        else
        {
            slideCounter -= Time.deltaTime;
            if(slideCounter <= 0f){
                isSlideCold = false;
            }
        }
    }

   
    
}
