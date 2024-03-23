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
        //Bocchi:������ҵĶ������߼�
        /*
        if(Vector2.Distance(currentEnemy.transform.position, currentEnemy.attackerTransform.position) <= currentEnemy.stoppingDistance)
        {
            currentEnemy.moveable = false;
            currentEnemy.rb.velocity = Vector2.zero;
            if (currentEnemy.canAttack)
            {
                currentEnemy.anim.SetTrigger("Attack");
                currentEnemy.canAttack = false;
            }
            else
            {
                currentEnemy.anim.ResetTrigger("Attack");
            }
            Debug.Log(currentEnemy.canAttack);
        }
        else if(Vector2.Distance(currentEnemy.transform.position, currentEnemy.attackerTransform.position) > currentEnemy.stoppingDistance
            &&currentEnemy.anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f)

        {
            currentEnemy.moveable = true;
        }
        currentEnemy.AttackTimeCounter();
        */

        
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
                if (Vector2.Distance((Vector2)currentEnemy.transform.position, currentEnemy.attackerTransform.position) <= currentEnemy.stoppingDistance)
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
    //Bocchi:׷�������߼�
    public void Chase()
    {
        if (currentEnemy.attackerTransform.position.x-currentEnemy.transform.position.x>0f)
        {
            currentEnemy.currentFace = 1;
        }
        else
        {
            currentEnemy.currentFace = -1;
        }
        currentEnemy.transform.localScale = new Vector3(currentEnemy.currentFace, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        currentEnemy.rb.velocity = new Vector2(currentEnemy.currentFace * Time.deltaTime * currentEnemy.chaseSpeed, 0);
    }
}