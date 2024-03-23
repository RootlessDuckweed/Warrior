using System;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]public Animator anim;
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public Character character;
    [HideInInspector]public PhysicsCheck check;
    [HideInInspector]public Transform attackerTransform;
    [HideInInspector]public float currentFace;
    public LayerMask playerLayerMask;   //�����ҵĲ㼶
    public float normalSpeed;
    public float chaseSpeed;
    public float attackTime;
    private float attackTimeCounter;
    public bool isHurt;
    [HideInInspector]public bool isDead;
    public float hurtForce;
    private BaseState currentState;
    public BaseState CurrentState { get { return currentState; } } //Bocchi:��õ��˵�ǰ״̬(ֻ��)
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState attackState; //Bocchi:��ӵ��˹���״̬
    protected BaseState hurtState;//Bocchi:��ӵ�������״̬
    protected BaseState deathState;//Bocchi:��ӵ�������״̬
    public Vector2 chaseRadiusOffset;//Bocchi:׷����Χƫ����
    [HideInInspector]public bool moveable;       //Bocchi:�жϵ����Ƿ��ڿ��ƶ�״̬
    [HideInInspector] public bool canAttack;     //Bocchi;�жϵ����Ƿ��ڿɹ���״̬
    public float chaseRadius;//Bocchi:�����ҵķ�Χ
    public float stoppingDistance;//Bocchi:����ҵ�ֹͣ�ƶ��ľ���

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrolState = new PatrolState();
        chaseState = new ChaseState();
        attackState = new AttackState();
        deathState = new DeathState();
        hurtState = new HurtState();
    }
    // Start is called before the first frame update
    void Start()
    {
        attackerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        check = GetComponent<PhysicsCheck>();
        currentFace = transform.localScale.x;
        attackTimeCounter = attackTime;
        isHurt = false;
        isDead = false;
        moveable = true;
        canAttack = true;
    }

    void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }
    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            if (isHurt)
            {
                SwitchState(State.HURT);
            }
            currentState.LogicUpdate();
        }

    }

    private void FixedUpdate()
    {   
        if(!isDead && !isHurt)
        {
        //Bocchi:������û����������û����ʱ�����ƶ�����
           if (!FoundPlayer())
           {
               if (!anim.GetBool("isWalk"))
               {
                   SwitchState(State.PATROL);
               }
               //Move();
           }
          /* else if (!isDead && !isHurt && FoundPlayer() && Vector2.Distance(transform.position,attackerTransform.position)>stoppingDistance)
           {
               if(!anim.GetBool("isChase"))
               {
                   SwitchState(State.CHASE);
               }
               //Chase();
           }*/
           currentState.PhysicUpdate();
        }
    }



    public void SwitchState(State state)
    {
        switch (state)
        {
            case State.PATROL:
                currentState.OnExit();
                currentState = patrolState; 
                patrolState.OnEnter(this);
                break;
            case State.CHASE:
                currentState.OnExit();
                currentState = chaseState;
                currentState.OnEnter(this);
                break;
            case State.ATTACK:
                currentState.OnExit();
                currentState = attackState;
                currentState.OnEnter(this);
                break;
            case State.DEATH:
                currentState.OnExit();
                currentState = deathState;
                currentState.OnEnter(this);
                break;
            case State.HURT:
                currentState.OnExit();
                currentState = hurtState;
                currentState.OnEnter(this);
                break;
            default: 
                break;
        }
    }

    /// <summary>
    ///Bocchi:ר�ż��㹥������ȴ
    /// </summary>
    public void AttackTimeCounter()
    {
        if(!canAttack)
        {
            if(attackTimeCounter<=0f)
             {
                 ResetAttackTimeCounter();
             }
             else
             {
                 attackTimeCounter -= Time.deltaTime;
             }
        }
        
    }

    /// <summary>
    ///Bocchi:���ü��㹥������ȴ
    /// </summary>
    public void ResetAttackTimeCounter()
    {
        attackTimeCounter = attackTime;
        canAttack=true;
    }

    /*    public void Move()
        {
            transform.localScale = new Vector3(currentFace,transform.localScale.y,transform.localScale.z);
            rb.velocity = new Vector2(currentFace*Time.deltaTime*normalSpeed,0);
        }
    */
    public bool FoundPlayer()
    {
        if (Vector2.Distance((Vector2)transform.position+chaseRadiusOffset, attackerTransform.position) < chaseRadius)
        {
            return true;
        }
        return false;
    }
    /*
    public void Chase()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + chaseRadiusOffset, stoppingDistance, playerLayerMask);
    }
    */


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+chaseRadiusOffset,chaseRadius);
    }

    public void DestroyEnemy()
    {
        if(!gameObject.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }

    public void GetHurt()
    {
        isHurt = true;
    }

    public void EnemyDead()
    {
        isDead = true;
        SwitchState(State.DEATH);
    }
}
