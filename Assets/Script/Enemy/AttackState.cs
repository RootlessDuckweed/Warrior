using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : BaseState
{
    public override void LogicUpdate()
    {
        if (currentEnemy.canAttack)
        {
            currentEnemy.anim.SetTrigger("Attack");
            currentEnemy.canAttack = false;
            currentEnemy.moveable = false;
        }
        currentEnemy.AttackTimeCounter();
        Debug.Log(currentEnemy.canAttack);
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //currentEnemy.anim.SetTrigger("Attack");
    }

    public override void OnExit()
    {
        currentEnemy.ResetAttackTimeCounter();
        currentEnemy.anim.ResetTrigger("Attack");
    }

    public override void PhysicUpdate()
    {
        currentEnemy.rb.velocity = Vector2.zero;
        if (!currentEnemy.isDead && !currentEnemy.isHurt && currentEnemy.FoundPlayer() 
            && Vector2.Distance((Vector2)currentEnemy.transform.position, currentEnemy.attackerTransform.position) > currentEnemy.stoppingDistance)
        {
            currentEnemy.SwitchState(State.CHASE);
        }
    }
}