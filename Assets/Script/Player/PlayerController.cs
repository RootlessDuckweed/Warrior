using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //自身的刚体组件
    [HideInInspector]public PlayerInput input; //输入控制器
    public Vector2 inputDirection; //输入人物移动的方向
    public PlayerAnimaton playerAnimaton; //自身的PlayerAnimation脚本组件
    public PhysicsCheck physicsCheck; //自身物理检测脚本组件

    [Header("事件")]
    public AttackSO attackCriticalData;
    [Header("暴击几率 0~1")]
    public float criticalRate; //暴击几率
    [Header("暴击时间变慢的比例")]
    public float critTimeScale;
    [Header("暴击暴击时间变慢的时长")]
    public float critDuration;
    public bool isCritical; //是否暴击

    [Header("冲刺影子参数")]
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
    public bool isSlideCold;  //是否在滑铲冷却状态
    public bool isSlide;
    public float dashTimeDuration;
    public float dashTimeCounter;
    public bool isDashCold;
    public bool isAttack;
    public bool isHurt;
    public bool isDead;
    public bool isDash;

    public GameObject critical;
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
        if(physicsCheck.isGround|| physicsCheck.isPlayerDead)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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
    //滑铲冷却计时
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

    // 子物体Attack攻击游戏对象 接触判断 是否造成了暴击
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")&&isAttack)
        {
            float randomRate = UnityEngine.Random.value;
            if (randomRate < criticalRate || randomRate >= 1f)
            {
                isCritical = true;
                attackCriticalData.SetCritical(isCritical);
                critical.SetActive(true); //激活产生暴击的对象
            }
            else
            {
                isCritical = false;
                attackCriticalData.SetCritical(isCritical);
            }
        }
    
    }

    #region Unity Animation Event
    //播放第一段攻击
    public void PlayAttackFX_1()
    {
        AudioManager.Instance.PlayFX(AudioPathGlobals.Attack_1,0.2f);
    }
    
    //播放第二段攻击
    public void PlayAttackFX_2()
    {
        AudioManager.Instance.PlayFX(AudioPathGlobals.Attack_2, 0.2f);
    }
    #endregion
}
