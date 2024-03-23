using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : BaseState
{
    public override void LogicUpdate()
    {
        //Bocchi:攻击玩家的动画的逻辑
        if (!currentEnemy.attackerTransform.GetComponent<PlayerController>().isDead)
        {
            //Bocchi:攻击时转向
            if (currentEnemy.attackerTransform.position.x - currentEnemy.transform.position.x > 0f)
            {
                currentEnemy.currentFace = 1;
            }
            else
            {
                currentEnemy.currentFace = -1;
            }
            if (currentEnemy.canAttack)
            {
                currentEnemy.anim.SetTrigger("Attack");
                currentEnemy.canAttack = false;
                currentEnemy.moveable = false;
            }            
        }
        else
        {
            currentEnemy.SwitchState(State.PATROL);
        }
        //Debug.Log(currentEnemy.canAttack);
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //currentEnemy.anim.SetTrigger("Attack");
    }

    public override void OnExit()
    {
        //currentEnemy.anim.ResetTrigger("Attack");
    }

    public override void PhysicUpdate()
    {
        currentEnemy.rb.velocity = Vector2.zero;
        if (!currentEnemy.isDead && !currentEnemy.isHurt && currentEnemy.FoundPlayer() 
            && !currentEnemy.InAttackRange())
        {
            currentEnemy.SwitchState(State.CHASE);
        }
    }
}