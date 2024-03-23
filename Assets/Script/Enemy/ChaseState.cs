using Cainos.PixelArtPlatformer_VillageProps;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseState:BaseState
{
    public ChaseState() 
    { }

    public override void LogicUpdate()
    {

    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.SetBool("isChase",true);
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isChase", false);
    }
    public override void PhysicUpdate()
    {
        if (!currentEnemy.attackerTransform.GetComponent<PlayerController>().isDead)
        {
            if(currentEnemy.moveable)
            {
                Chase();
                if (currentEnemy.InAttackRange())
                {
                    currentEnemy.SwitchState(State.ATTACK);
                }
            }
        }
        else
        {
            currentEnemy.SwitchState(State.PATROL);
        }

    }
    //Bocchi:追击敌人逻辑
    public void Chase()
    {
        //Bocchi:通过物理检测碰撞体来改变朝向
        Collider2D facingCollider = Physics2D.OverlapCircle((Vector2)currentEnemy.transform.position + currentEnemy.chaseRadiusOffset, currentEnemy.chaseRadius, currentEnemy.playerLayerMask);
        if (facingCollider != null)
        {
            if (facingCollider.gameObject.transform.position.x - currentEnemy.transform.position.x > 0f)
            {
                currentEnemy.currentFace = 1;
            }
            else
            {
                currentEnemy.currentFace = -1;
            }

        }
        currentEnemy.transform.localScale = new Vector3(currentEnemy.currentFace, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        currentEnemy.rb.velocity = new Vector2(currentEnemy.currentFace * Time.deltaTime * currentEnemy.chaseSpeed, 0);
    }
}