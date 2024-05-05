using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //����ĸ������
    [HideInInspector]public PlayerInput input; //���������
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

    [Header("���Ӱ�Ӳ���")]
    public float dashShadowDuraTime;
    private float dashShadowTimeCounter;

    public float hurtForce;
    public float moveSpeed;
    public float currentFace;
    public float jumpForce;
    public float slideForce;
    public float dashForce;
    public float slideTimeDuration;
    public float slideCounter;
    public bool isSlideCold;  //�Ƿ��ڻ�����ȴ״̬
    public bool isSlide;
    public float dashTimeDuration;
    public float dashTimeCounter;
    public bool isDashCold;
    public bool isAttack;
    public bool isHurt;
    public bool isDead;
    public bool isDash;
    public bool isClimb;    //Bocchi:�Ƿ�������״̬
    public bool isFalling;  //Bocchi:����Ƿ��ڵ���״̬
    private float orginalGravity;   //Bocchi:�����ʼ���������ٶ�

    public GameObject critical;
    private void Awake()
    {
        isClimb = false;
        rb = GetComponent<Rigidbody2D>();
        orginalGravity = rb.gravityScale;
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
        if (isDashCold||isAttack) return;
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
        PlayerDashShadow();
        inputDirection = input.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(!isHurt&&!isDash&&!isSlide)
        {
            Move();
            Climb();
        }

    }

    private void Move()
    {
        if (physicsCheck.isGround)
        {
            isFalling = false;
        }
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
        if(physicsCheck.isGround||isClimb|| physicsCheck.isPlayerDead)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            if (isClimb) 
            { 
                isFalling = true;
                isClimb = false;            
            }
            rb.gravityScale = orginalGravity; 
        }

    }
    private void Slide(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isSlideCold) return;
        if (physicsCheck.isGround)
        {
            isSlide = true;
            isSlideCold = true;
            playerAnimaton.TriggerSlide();
            rb.AddForce(transform.right * currentFace*slideForce, ForceMode2D.Impulse);
            slideCounter = slideTimeDuration;
        }
        
    }

    private void Climb()
    {
        if (isClimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed*inputDirection.y*Time.deltaTime);
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

    void PlayerDashShadow()
    {
        if (isDash)
        {
            dashShadowTimeCounter -= Time.deltaTime;
            if (dashShadowTimeCounter <= 0f)
            {
                ShadowPool.Instance.Get();
                dashShadowTimeCounter = dashShadowDuraTime;
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
        gameObject.tag = "PlayerDead";
        gameObject.layer = 8;
        rb.mass = 2f;
        UIManager.Instance.OpenPanel("DeadPanel");
    }

    // ������Attack������Ϸ���� �Ӵ��ж� �Ƿ�����˱���
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")&&isAttack)
        {
            float randomRate = UnityEngine.Random.value;
            if (randomRate < criticalRate || randomRate >= 1f)
            {
                isCritical = true;
                attackCriticalData.SetCritical(isCritical);
                critical.SetActive(true); //������������Ķ���
            }
            else
            {
                isCritical = false;
                attackCriticalData.SetCritical(isCritical);
            }
        }
    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ladder"))
        {
            if (!isFalling)
            {
                rb.gravityScale = 0;
                isClimb = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            rb.gravityScale = orginalGravity;
            isClimb = false;
            isFalling = false;
        }
    }

    #region Unity Animation Event
    //���ŵ�һ�ι���
    public void PlayAttackFX_1()
    {
        AudioManager.Instance.PlayFX(AudioPathGlobals.Attack_1,0.2f);
    }
    
    //���ŵڶ��ι���
    public void PlayAttackFX_2()
    {
        AudioManager.Instance.PlayFX(AudioPathGlobals.Attack_2, 0.2f);
    }
    #endregion
}
