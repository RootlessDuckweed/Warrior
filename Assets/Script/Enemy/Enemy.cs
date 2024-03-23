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
    [HideInInspector]public bool moveable;       //Bocchi:�жϵ����Ƿ��ڿ��ƶ�״̬
    [HideInInspector] public bool canAttack;     //Bocchi;�жϵ����Ƿ��ڿɹ���״̬
    public float chaseRadius;//Bocchi:�����ҵķ�Χ
    public float stoppingDistance;//Bocchi:����ҵ�ֹͣ�ƶ��ľ���
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
        //Bocchi:������û����������û����ʱ�����ƶ�����
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
    ///Bocchi:ר�ż��㹥������ȴ
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

    //Bocchi:��������ʱ���е���Ϊ
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
