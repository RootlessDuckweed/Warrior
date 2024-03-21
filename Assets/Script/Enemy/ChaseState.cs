using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
public class ChaseState:BaseState
{
    public ChaseState() 
    { }

    public override void LogicUpdate()
    {
        //Bocchi:攻击玩家的动画的逻辑
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
        
    }
}