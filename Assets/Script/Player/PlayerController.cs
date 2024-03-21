using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //自身的刚体组件
    private PlayerInput input; //输入控制器
    public Vector2 inputDirection; //输入人物移动的方向
    public float moveSpeed;
    public float currentFace;
    public float jumpForce;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInput();
        input.GamePlay.Jump.started += Jump;
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
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

}
