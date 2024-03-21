using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //����ĸ������
    private PlayerInput input; //���������
    public Vector2 inputDirection; //���������ƶ��ķ���
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
        //ת��
        currentFace = transform.localScale.x;
        if (inputDirection.x > 0) currentFace = 1;
        if (inputDirection.x < 0) currentFace = -1;
        transform.localScale = new Vector3(currentFace, transform.localScale.y, transform.localScale.z);
        //�ƶ�
        rb.velocity = new Vector2(moveSpeed * inputDirection.x* Time.deltaTime, rb.velocity.y);
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

}
