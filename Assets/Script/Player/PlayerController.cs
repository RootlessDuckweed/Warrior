using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //����ĸ������
    private PlayerInput input; //���������
    public Vector2 inputDirection; //���������ƶ��ķ���
    public PlayerAnimaton playerAnimaton; //�����PlayerAnimation�ű����
    public PhysicsCheck physicsCheck; //����������ű����

    [Header("�¼�")]
    public AttackSO attackCriticalData;
    [Header("�������� 0~1")]
    public float criticalRate; //��������
    [Header("����ʱ������ı���")]
    public float critTimeScale;
    [Header("��������ʱ�������ʱ��")]
    public float critDuration;
    public bool isCritical; //�Ƿ񱩻�

    public float hurtForce;
    public float moveSpeed;
    public float currentFace;
    public float jumpForce;
    public float slideForce;
    public float dashForce;
    public float slideTimeDuration;
    public float slideCounter;
    public bool isSlideCold;  //�Ƿ��ڻ�����ȴ״̬
    public float dashTimeDuration;
    public float dashTimeCounter;
    public bool isDashCold;
    public bool isAttack;
    public bool isHurt;
    public bool isDead;
    public bool isDash;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimaton = GetComponent<PlayerAnimaton>();
        physicsCheck = GetComponent<PhysicsCheck>();
        
        input = new PlayerInput();
        input.GamePlay.Jump.started += Jump;
        input.GamePlay.Slide.performed += Slide;
        input.GamePlay.Attack.started += PlayerAttack;
        input.GamePlay.Dash.performed += PlayerDash;
    }

    private void PlayerDash(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isDashCold) return;
        isDash = true;
        rb.AddForce(Vector2.right*dashForce*currentFace,ForceMode2D.Impulse);
        isDashCold = true;
        dashTimeCounter = dashTimeDuration;
        
    }

    private void PlayerAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerAnimaton.TriggerAttack();
        isAttack = true;
    }

    private void OnEnable()
    {
        input.GamePlay.Enable();
    }
    private void OnDisable()
    {
        input.GamePlay.Disable();
    }
    private void Update()
    {
        SlideTimeCounter();
        DashTimeCounter();
        inputDirection = input.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(!isHurt&&!isDash)
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
    //������ȴ��ʱ
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

    void DashTimeCounter()
    {
        if (!isDashCold) return;
        else
        {
            dashTimeCounter -= Time.deltaTime;
            if (dashTimeCounter <= 0f)
            {
                isDashCold = false;
            }
        }
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, transform.position.y).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        playerAnimaton.TriggerHurt();
    }

    public void PlayerDead()
    {
        isDead = true;
        input.GamePlay.Disable();
        gameObject.tag = "Untagged";
        gameObject.layer = 3;
        rb.mass = 100f;
    }

    // ������Attack������Ϸ���� �Ӵ��ж�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            float randomRate = UnityEngine.Random.value;
            if (randomRate < criticalRate || randomRate >= 1f)
            {
                isCritical = true;
                attackCriticalData.SetCritical(isCritical);
                
            }
            else
            {
                isCritical = false;
                attackCriticalData.SetCritical(isCritical);
            }
        }
    
    }



    public void PerformCriticalAttack()
    {
        if (!attackCriticalData.GetCritical()) return;
        // ��ͣ��Ϸʱ��
        Time.timeScale = critTimeScale;

        // �ڱ���Ч��������ָ�ʱ�������
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        yield return new WaitForSecondsRealtime(critDuration);
        print("ResumeTime");
        Time.timeScale = 1f;
        isCritical = false;
        attackCriticalData.SetCritical(isCritical);
    }
}
