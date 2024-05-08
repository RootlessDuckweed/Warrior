
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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
    [HideInInspector]public bool isHurt;
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
    [Header("����ʹ�ü��ܵĸ���")]
    public float skillRate;//Bocchi:����ʹ�ü��ܵĸ���
    public List<Pair<PropSO, int>> itemList;    //Bocchi:���˵������Ʒ

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
            currentState.LogicUpdate();
            AttackTimeCounter();
        }
    }

    private void FixedUpdate()
    {   
        if(!isDead && !isHurt)
        {
        //Bocchi:������û����������û����ʱ�����ƶ�����
           if (!FoundPlayer())
           {
               if (currentState!=patrolState)
               {
                   SwitchState(State.PATROL);
               }
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

    public bool FoundPlayer()
    {
        //Ѱ�����
        return Physics2D.OverlapCircle((Vector2)transform.position + chaseRadiusOffset, chaseRadius, playerLayerMask);
    }

    public bool InAttackRange()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + chaseRadiusOffset, stoppingDistance, playerLayerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+chaseRadiusOffset,chaseRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position+chaseRadiusOffset,stoppingDistance);
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
        moveable = false;
        Vector2 dir = new Vector2(transform.position.x-attacker.position.x,attacker.position.y).normalized;
        rb.AddForce(new Vector2(hurtForce, 0)* dir);
        anim.SetTrigger("Hurt");
    }

    public void EnemyDead()
    {
        isDead = true;
        moveable = false;
        anim.SetTrigger("Death");
        AddItemToInventory();
    }

    public void AddItemToInventory()
    {
        if (itemList.Count > 0)
        {
            foreach (var item in itemList)
            {
                InventoryManager.Instance.AddProp(item.key, item.value);
            }

            UIManager.Instance.OpenPanel("ItemDescriptionPanel");
            UIManager.Instance.panelDict["ItemDescriptionPanel"].GetComponent<ItemDescriptonPanel>().GeneratePanel(itemList);
        }
    }
}
