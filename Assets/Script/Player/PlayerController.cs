using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //自身的刚体组件
    private PlayerInput input; //输入控制器
    public Vector2 inputDirtion; //输入人物移动的方向
    public float moveSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInput();
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
        inputDirtion = input.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(moveSpeed * inputDirtion.x, rb.velocity.y);
    }
}
