using JetBrains.Annotations;
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
    public LayerMask playerLayerMask;   //¼ì²âÍæ¼ÒµÄ²ã¼¶
    public float normalSpeed;
    public float chaseSpeed;
    public float attackTime;
    private float attackTimeCounter;
    [HideInInspector]public bool isHurt;
    [HideInInspector]public bool isDead;
    public float hurtForce;
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState attackState; //Bocchi:Ìí¼ÓµÐÈË¹¥»÷×´Ì¬
    protected BaseState hurtState;//Bocchi:Ìí¼ÓµÐÈËÊÜÉË×´Ì¬
    protected BaseState deathState;//Bocchi:Ìí¼ÓµÐÈËËÀÍö×´Ì¬
    public Vector2 chaseRadiusOffset;//Bocchi:×·»÷·¶Î§Æ«ÒÆÁ¿
    [HideInInspector]public bool moveable;       //Bocchi:ÅÐ¶ÏµÐÈËÊÇ·ñ´¦ÓÚ¿ÉÒÆ¶¯×´Ì¬
    [HideInInspector] public bool canAttack;     //Bocchi;ÅÐ¶ÏµÐÈËÊÇ·ñ´¦ÓÚ¿É¹¥»÷×´Ì¬
    public float chaseRadius;//Bocchi:¼ì²âÍæ¼ÒµÄ·¶Î§
    public float stoppingDistance;//Bocchi:ÓëÍæ¼ÒµÄÍ£Ö¹ÒÆ¶¯µÄ¾àÀë

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrolState = new PatrolState();
        chaseState = new ChaseState();
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
            AttackTimeCounter();
        }
    }

    private void FixedUpdate()
    {   
        if(!isDead && !isHurt)
        {
        //Bocchi:ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ã»ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ã»ï¿½ï¿½ï¿½ï¿½Ê±ï¿½ï¿½ï¿½ï¿½ï¿½Æ¶ï¿½ï¿½ï¿½ï¿½ï¿½
           if (!FoundPlayer() && moveable)
           {
               if (!anim.GetBool("isWalk"))
               {
                   SwitchState(State.PATROL);
               }
               Move();
           }
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
                chaseState.OnEnter(this);
                break;
            default: 
                break;
        }
    }

    /// <summary>
    ///Bocchi:×¨ï¿½Å¼ï¿½ï¿½ã¹¥ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½È´
    /// </summary>
    public void AttackTimeCounter()
    {
        if(!canAttack)
        {
            if(attackTimeCounter<=0f)
             {
                 attackTimeCounter = attackTime;
                 canAttack = true;
             }
             else
             {
                 attackTimeCounter -= Time.deltaTime;
             }
        }
        
    }

    public void Move()
    {
        transform.localScale = new Vector3(currentFace,transform.localScale.y,transform.localScale.z);
        rb.velocity = new Vector2(currentFace*Time.deltaTime*normalSpeed,0);
    }

    public bool FoundPlayer()
    {
        //Ñ°ÕÒÍæ¼Ò
        return Physics2D.OverlapCircle((Vector2)transform.position + chaseRadiusOffset, chaseRadius, playerLayerMask);
    }

    public bool InAttackRange()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + chaseRadiusOffset, stoppingDistance, playerLayerMask);
    }

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

    //Bocchi:µÐÈËÊÜÉËÊ±½øÐÐµÄÐÐÎª
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        Vector2 dir = new Vector2(transform.position.x-attacker.position.x,attacker.position.y).normalized;
        rb.AddForce(new Vector2(hurtForce, 0)* dir);
    }

    public void EnemyDead()
    {
        isDead = true;
        SwitchState(State.DEATH);
    }
}
