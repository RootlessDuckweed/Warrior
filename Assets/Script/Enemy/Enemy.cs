using UnityEditor.XR;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]public Animator anim;
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public Character character;
    [HideInInspector]public PhysicsCheck check;
    [HideInInspector]public Transform attackerTransform;
    [HideInInspector]public float currentFace;
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
    [HideInInspector]public bool moveable;       //Bocchi:判断敌人是否处于可移动状态
    [HideInInspector] public bool canAttack;     //Bocchi;判断敌人是否处于可攻击状态
    public float chaseRadius;//Bocchi:检测玩家的范围
    public float stoppingDistance;//Bocchi:与玩家的停止移动的距离
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
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {   
        if(!isDead && !isHurt)
        {
        //Bocchi:当敌人没有死亡或者没受伤时调用移动函数
           if (!FoundPlayer() && moveable)
           {
               if (!anim.GetBool("isWalk"))
               {
                   SwitchState(State.PATROL);
               }
               Move();
           }
           else if (!isDead && !isHurt && FoundPlayer() && Vector2.Distance(transform.position,attackerTransform.position)>stoppingDistance && moveable)
           {
               if(!anim.GetBool("isChase"))
               {
                   SwitchState(State.CHASE);
               }
               Chase();
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
    ///Bocchi:专门计算攻击的冷却
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
        if (Vector3.Distance(transform.position, attackerTransform.position) < chaseRadius)
        {
            return true;
        }
        return false;
    }
    public void Chase()
    {
        if (Vector2.Distance((Vector2)check.transform.position + check.checkPointOffset_RightWall, (Vector2)attackerTransform.position)
            < Vector2.Distance((Vector2)check.transform.position + check.checkPointOffset_LeftWall, (Vector2)attackerTransform.position))
        {
            currentFace = 1;
        }
        else
        {
            currentFace = -1;
        }
        transform.localScale = new Vector3(currentFace, transform.localScale.y, transform.localScale.z);
        rb.velocity = new Vector2(currentFace * Time.deltaTime * chaseSpeed, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position,chaseRadius);
    }
}
